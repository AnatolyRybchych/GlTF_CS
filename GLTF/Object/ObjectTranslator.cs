
using System.Text.Json;
using GLTF.Object.Fieslds;

namespace GLTF.Object;

public class ObjectTranslator
{
    private GLTF.Layout.LayoutGLTF gltf;
    public ObjectTranslator(GLTF.Layout.LayoutGLTF gltf)
    {
        this.gltf = gltf;
    }

    public string[] getUsedExtentions()
    {
        return gltf.extensionsUsed;
    }

    public string[] getRequiredExtentions()
    {
        return gltf.extensionsRequired;
    }

    public asset getAsset()
    {
        return new asset(gltf.asset.version, gltf.asset.generator, 
            gltf.asset.minVersion, gltf.asset.copyright);
    }

    public scene getDefaultScene(List<scene> scenes)
    {
        if(gltf.scene < 0 || gltf.scene > scenes.Count)
            throw new ArgumentException($"default scene is undefined");
        return scenes[gltf.scene];;
    }

    public List<sampler> getSamplers()
    {
        return gltf.samplers.Select(s => new sampler(
            s.name, s.magFilter, s.minFilter, s.wrapS, s.wrapT
        )).ToList();
    }

    public List<mesh> getMeshes(List<material> materials, List<accessor> accessors)
    {
        List<mesh> result = new List<mesh>();

        foreach (var mesh in gltf.meshes)
        {
            string name = mesh.name;

            primitive[] primitives = mesh.primitives.Select(p =>{
                material? mat = null;
                accessor? indices = null;

                if(p.material.HasValue){
                    if(p.material.Value < 0 || p.material.Value >= materials.Count) 
                        throw new ArgumentException($"mesh '{name}' has reference to undefined material");
                    mat = materials[p.material.Value];
                }

                if(p.indices.HasValue){
                    if(p.indices.Value < 0 || p.indices.Value >= accessors.Count) 
                        throw new ArgumentException($"mesh '{name}' has reference to undefined indices");

                    indices = accessors[p.indices.Value];
                }

                return new primitive(
                    p.attributes,
                    p.mode,
                    p.targets ?? JsonDocument.Parse("{}"),
                    mat,
                    indices);
            }).ToArray();

            

            result.Add(
                new mesh(name, primitives, mesh.weights == null ? new double[0] : mesh.weights)
            );
        }

        return result;
    }

    public List<material> getMaterials(List<texture> textures)
    {
        List<material> result = new List<material>();
        foreach (var mat in gltf.materials)
        {
            string name = mat.name;

            pbrMetallicRoughness? roughness = null;
            normalTextureInfo? normalTexture = null;
            occlusionTextureInfo? occlusionTexture = null;
            textureInfo? emissiveTexture = null;

            if(mat.pbrMetallicRoughness != null)
            {
                textureInfo? baseColorTexture = null;
                textureInfo? metallicRoughnessTexture = null;

                if(mat.pbrMetallicRoughness.baseColorTexture != null)
                {
                    if(!mat.pbrMetallicRoughness.baseColorTexture.index.HasValue
                        || mat.pbrMetallicRoughness.baseColorTexture.index < 0
                        || mat.pbrMetallicRoughness.baseColorTexture.index >= textures.Count )
                        throw new ArgumentException($"material '{name}' has invalid pbrMetallicRoughness.baseColorTexture");
                    baseColorTexture = new textureInfo(textures[mat.pbrMetallicRoughness.baseColorTexture.index.Value], 
                        mat.pbrMetallicRoughness.baseColorTexture.texCoord);
                }
                if(mat.pbrMetallicRoughness.metallicRoughnessTexture != null)
                {
                    if(!mat.pbrMetallicRoughness.metallicRoughnessTexture.index.HasValue
                        || mat.pbrMetallicRoughness.metallicRoughnessTexture.index < 0
                        || mat.pbrMetallicRoughness.metallicRoughnessTexture.index >= textures.Count)
                        throw new ArgumentException($"material '{name}' has invalid pbrMetallicRoughness.metallicRoughnessTexture");

                    metallicRoughnessTexture = new textureInfo(
                        textures[mat.pbrMetallicRoughness.metallicRoughnessTexture.index.Value],
                        mat.pbrMetallicRoughness.metallicRoughnessTexture.texCoord
                    );
                }

                roughness = new pbrMetallicRoughness(
                    mat.pbrMetallicRoughness.metallicFactor, 
                    mat.pbrMetallicRoughness.roughnessFactor,
                    baseColorTexture, metallicRoughnessTexture,
                    mat.pbrMetallicRoughness.baseColorFactor
                );
            }

            if(mat.normalTexture != null)
            {
                if(!mat.normalTexture.index.HasValue
                || mat.normalTexture.index < 0
                || mat.normalTexture.index >= textures.Count)
                    throw new ArgumentException($"material '{name}' has invalid normalTexture");

                normalTexture = new normalTextureInfo(textures[mat.normalTexture.index.Value], 
                    mat.normalTexture.texCoord, mat.normalTexture.scale);
            }

            if(mat.occlusionTexture != null)
            {
                if(!mat.occlusionTexture.index.HasValue
                || mat.occlusionTexture.index < 0
                || mat.occlusionTexture.index >= textures.Count)
                    throw new ArgumentException($"material '{name}' has invalid occlusionTexture");

                occlusionTexture = new occlusionTextureInfo(textures[mat.occlusionTexture.index.Value],
                    mat.occlusionTexture.texCoord, mat.occlusionTexture.strength);
            }

            if(mat.emissiveTexture != null)
            {
                if(!mat.emissiveTexture.index.HasValue
                || mat.emissiveTexture.index < 0
                || mat.emissiveTexture.index >= textures.Count)
                    throw new ArgumentException($"material '{name}' has invalid emissiveTexture");

                emissiveTexture = new textureInfo(textures[mat.emissiveTexture.index.Value],
                    mat.emissiveTexture.texCoord);
            }

            result.Add(
                new material(
                    name,
                    roughness,
                    normalTexture,
                    occlusionTexture,
                    emissiveTexture,
                    mat.emissiveFactor,
                    Enum.Parse<alphaMode>(mat.alphaMode),
                    mat.alphaCutoff,
                    mat.doubleSided
                )
            );
        }
        return result;
    }

    public List<animation> getAnimations(List<accessor> accessors, List<node> nodes)
    {
        List<animation> result = new List<animation>();
        foreach (var anim in gltf.animations)
        {
            string name = anim.name;

            animationSampler[] samplers = anim.samplers.Select(sampler =>{
                if(!sampler.input.HasValue || !sampler.output.HasValue
                || sampler.input.Value < 0 || sampler.output.Value < 0
                || sampler.input.Value > accessors.Count || sampler.output.Value > accessors.Count)
                    throw new ArgumentException($"animation '{name}' has corrupted sampler");
                return new animationSampler(
                    accessors[sampler.input.Value],
                    accessors[sampler.output.Value],
                    Enum.Parse<interpolation>(sampler.interpolation)
                );
            }).ToArray();

            channel[] channels = anim.channels.Select(ch => {
                if(ch.target == null) throw new ArgumentException($"animation '{name}' has reference to undefined target");
                if(!ch.sampler.HasValue || ch.sampler.Value < 0 || ch.sampler.Value >= samplers.Length) 
                    throw new ArgumentException($"animation '{name}' has reference to undefined sampler");
                if(ch.target.node < 0 || ch.target.node >= nodes.Count)
                    throw new ArgumentException($"animation '{name}' has reference for undefined node");

                return new channel(
                    samplers[ch.sampler.Value],
                    new target(nodes[ch.target.node] , Enum.Parse<animationPath>(ch.target.path))
                );
            }).ToArray();

            result.Add(new animation(name, channels, samplers));
        }
        return result;
    }

    public List<accessor> getAccessors(List<bufferView> bufferViews)
    {
        List<accessor> result = new List<accessor>();
        foreach (var accessor in gltf.accessors)
        {
            string name = accessor.name;
            sparse? sparse = null;

            if(!accessor.bufferView.HasValue 
            || accessor.bufferView.Value < 0 
            || accessor.bufferView.Value >= bufferViews.Count)
                throw new ArgumentException($"accessor '{name}' has reference for undefined bufferView");

            if(accessor.sparse != null)
            {
                if(accessor.sparse.indices == null
                || !accessor.sparse.indices.bufferView.HasValue 
                || accessor.sparse.indices.bufferView.Value < 0 
                || accessor.sparse.indices.bufferView.Value > bufferViews.Count) 
                    throw new ArgumentException($"accessor '{name}' has reference for undefined sparce.indices.bufferView");
                
                if (accessor.sparse.values == null
                || !accessor.sparse.values.bufferView.HasValue 
                || accessor.sparse.values.bufferView.Value < 0 
                || accessor.sparse.values.bufferView.Value > bufferViews.Count)
                    throw new ArgumentException($"accessor '{name}' has reference for undefined sparce.values.bufferView");

                sparse = new sparse(accessor.sparse.count,
                    new indices(
                        bufferViews[accessor.sparse.indices.bufferView.Value],
                        accessor.sparse.indices.byteOffset,
                        accessor.sparse.indices.componentType
                    ),
                    new values(
                        bufferViews[accessor.sparse.values.bufferView.Value],
                        accessor.sparse.values.byteOffset)
                );
            }
            result.Add(
                new accessor(
                    name, bufferViews[accessor.bufferView.Value],
                    accessor.byteOffset, accessor.componentType,
                    accessor.normalized, accessor.count,
                    Enum.Parse<type>(accessor.type),
                    accessor.max, accessor.min,sparse
                )
            );
        }
        return result;
    }

    //required nodes, accessors
    public List<skin> getSkins(List<accessor> accessors, List<node> nodes)
    {
        List<skin> result = new List<skin>();
        foreach (var skin in gltf.skins)
        {
            string name = skin.name;
            if(skin.inverseBindMatrices.HasValue 
            && ( skin.inverseBindMatrices.Value < 0 
                || skin.inverseBindMatrices.Value > accessors.Count) )
                    throw new ArgumentException($"skin '{name}' reference for undefined inverseBindMatrices");
            if(skin.skeleton.HasValue 
            && ( skin.skeleton.Value < 0 
                || skin.skeleton.Value > nodes.Count) )
                    throw new ArgumentException($"skin '{name}' reference for undefined skeleton");

            result.Add(
                    new skin(
                    name,  
                    skin.joints.Select(index =>{
                        if(index < 0 || index >= nodes.Count) 
                            throw new ArgumentException ($"skin '{name}' has reference for undefined join");
                        return nodes[index];
                    }).ToArray(),
                    skin.skeleton.HasValue ? nodes[skin.skeleton.Value] : null,
                    skin.inverseBindMatrices.HasValue ? accessors[skin.inverseBindMatrices.Value] : null
                )
            );
        }
        return result;
    }

    public void DefineNodeSkins(List<node> nodes, List<skin> skins)
    {
        nodes.ForEach(n => n.SetSkin(skins));
    }

    public List<node> getNodes(List<camera> cameras)
    {
        List<node> result = new List<node>();
        foreach (var node in gltf.nodes)
        {
            string name = node.name;
            camera? camera = null;
            if(node.camera.HasValue)
            {
                if(node.camera.Value < 0 || node.camera.Value >=  cameras.Count())
                    throw new ArgumentException($"node '{name}' has reference for undefined camera");
                camera = cameras[node.camera.Value];
            }

            
            double[] weights;
            if(node.weights == null) weights = new double[0];
            else weights = node.weights;
            
            if(node.matrix == null) result.Add(
                new node(
                    name, 
                    camera, 
                    new List<node>(), 
                    node.skin, 
                    weights,
                    node.rotation,
                    node.scale,
                    node.transplation
                )
            );
            else result.Add(
                new node(
                    name, 
                    camera, 
                    new List<node>(), 
                    node.skin, 
                    weights, 
                    node.matrix)
            );
        }
        return result;
    }

    public List<scene> getScenes(List<node> nodes)
    {
        List<scene> result = new List<scene>();
        foreach (var scene in gltf.scenes)
        {
            string name = scene.name;
            result.Add(
                new scene(name, scene.nodes.Select(node => {
                    if(node < 0 || node > nodes.Count)
                        throw new ArgumentException($"scene '{name}' has reference for undefined node");
                    return nodes[node];
                }).ToArray())
            );
        }
        return result;
    }

    public List<bufferView> getBufferView(List<buffer> buffers)
    {
        List<bufferView> result = new List<bufferView>();
        foreach (var bv in gltf.bufferViews)
        {
            string name = bv.name;
            if(!bv.buffer.HasValue || bv.buffer < 0 || bv.buffer >= buffers.Count()) 
                throw new ArgumentException($"bufferView '{name}' has reference for undefined buffer");

            result.Add(new bufferView(name, buffers[bv.buffer.Value], bv.byteOffset, bv.byteLength, 
                (bv.byteStride.HasValue ? bv.byteStride.Value : 0), bv.target));
        }

        return result;
    }

    public List<texture> getTextures(List<sampler> samplers, List<image> images)
    {
        List<texture> result = new List<texture>();
        foreach (var tex in gltf.textures)
        {
            string name = tex.name;
            
            if(!tex.sampler.HasValue)
                throw new ArgumentException($"texture '{name}' has not sampler");
            if(!tex.source.HasValue)
                throw new ArgumentException($"texture '{name}' has not source");
            if(tex.sampler.Value < 0 || tex.sampler.Value > samplers.Count())
                throw new ArgumentException($"texture '{name}' has reference for undefined sampler");
            if(tex.source.Value < 0 || tex.source.Value > images.Count())
                throw new ArgumentException($"texture '{name}' has reference for undefined source");
            
            result.Add(new texture(name, samplers[tex.sampler.Value], images[tex.source.Value]));
        }

        return result;
    }

    public List<image> getImages(List<bufferView> bufferViews, List<camera> cameras)
    {
        List<image> results = new List<image>();
        foreach (var img in gltf.images)
        {
            string name = img.name;
            if(img.bufferView.HasValue)
            {
                if(img.bufferView < 0 || img.bufferView >= cameras.Count()) 
                    throw new ArgumentException($"image '{name}' has reference on undefined bufferView");
                if(img.mimeType == null) throw new ArgumentException("image '{name}' should contain mime type");
                results.Add(new image(name, img.mimeType, bufferViews[img.bufferView.Value], img.uri));
            }
            else
            {
                results.Add(new image(name, "", null, img.uri));
            }
        }
        return results;
    }

    public List<buffer> getBuffers(List<camera> cameras)
    {
        return gltf.buffers.Select(buf => new buffer(buf.name, buf.uri, buf.byteLength)).ToList();
    }

    public List<camera> getCameras(double alternativeRatio)
    {

        List<camera> result = new List<camera>();
        foreach (var camera in gltf.cameras)
        {
            string name = camera.name;
            if(camera.type == "perspective")
            {
                if(camera.perspective == null) throw new ArgumentException($"camera '{name}' has invalid projection settings");
                if(!camera.perspective.znear.HasValue 
                || !camera.perspective.yfov.HasValue) 
                    throw new ArgumentException($"camera '{name}' has invalid perspective projection params");
                result.Add(
                    new camera(name, new perspective(
                    camera.perspective.znear.Value, 
                    camera.perspective.zfar,
                    camera.perspective.yfov.Value,
                    camera.perspective.aspectRatio.HasValue ? 
                        camera.perspective.aspectRatio.Value : alternativeRatio,
                    !camera.perspective.zfar.HasValue)
                    )
                );
            }
            else if(camera.type == "orthographic")
            {
                if(camera.orthographic == null) throw new ArgumentException($"camera '{name}' has invalid projection settings");
                if(!camera.orthographic.xmag.HasValue 
                || !camera.orthographic.ymag.HasValue 
                || !camera.orthographic.zfar.HasValue 
                || !camera.orthographic.znear.HasValue)
                    throw new ArgumentException($"camera '{name}' has invalid orthographic projection params");
                result.Add(
                    new camera(name, new orthographic(
                    camera.orthographic.xmag.Value, 
                    camera.orthographic.ymag.Value, 
                    camera.orthographic.zfar.Value, 
                    camera.orthographic.znear.Value)
                    )
                );
            }
            else throw new ArgumentException("camera has undefined projection type");
        }

        return result;
    }
}