
using System.Text.Json;
using GLTF.Object.Fieslds;

namespace GLTF.Object;

public delegate string Translator(string source);
public class ObjectGLTF
{
    public ObjectGLTF(GLTF.Layout.LayoutGLTF gltf, double alternativeRatio)
    {
        ObjectTranslator translator = new ObjectTranslator(gltf);

        extensionsUsed      = translator.getUsedExtentions();
        extensionsRequired  = translator.getRequiredExtentions();
        asset               = translator.getAsset();

        samplers            = translator.getSamplers();
        cameras             = translator.getCameras(alternativeRatio);
        buffers             = translator.getBuffers(cameras);
        bufferViews         = translator.getBufferView(buffers);
        images              = translator.getImages(bufferViews, cameras);
        textures            = translator.getTextures(samplers, images);
        nodes               = translator.getNodes(cameras);
        accessors           = translator.getAccessors(bufferViews); 
        skins               = translator.getSkins(accessors, nodes);
        scenes              = translator.getScenes(nodes);
        scene               = translator.getDefaultScene(scenes);
        animations          = translator.getAnimations(accessors, nodes);
        materials           = translator.getMaterials(textures);
        meshes              = translator.getMeshes(materials, accessors);

        translator.DefineNodeSkins(nodes, skins);
        
        
    }

    public string[] extensionsUsed { get; private set; }
    public string[] extensionsRequired { get; private set; }
    public scene scene { get; private set; }
    public List<scene> scenes { get; private set;}
    public List<accessor> accessors { get; private set; }
    public List<animation> animations {get; set;}
    public asset asset{ get; private set; }
    public List<buffer> buffers { get; private set; }
    public List<bufferView> bufferViews { get; private set; }
    public List<camera> cameras { get; private set; }
    public List<image> images { get; private set; }
    public List<material> materials { get; private set; }
    public List<mesh> meshes { get; private set; }
    public List<node> nodes { get; private set; }
    public List<sampler> samplers { get; private set; }
    public List<skin> skins { get; private set; }
    public List<texture> textures { get; private set; }
}