
using GLTF.Fields;

namespace GLTF;


public class GLTF
{
     public string[] extensionsUsed { get; set; } = new string[0];
     public string[] extensionsRequired { get; set; } = new string[0];
     public accessor[] accessors { get; set; } = new accessor[0];
     public animation[] animations {get; set;} = new animation[0];
     public asset asset{ get; set; } = new asset();
     public buffer[] buffers { get; set; } = new buffer[0];
     public bufferView[] bufferViews { get; set; } = new bufferView[0];
     public camera[] cameras { get; set; } = new camera[0];
     public image[] images { get; set; } = new image[0];
     public material[] materials { get; set; } = new material[0];
     public mesh[] meshes { get; set; } = new mesh[0];
     public node[] nodes { get; set; } = new node[0];
     public sampler[] samplers { get; set; } = new sampler[0];
     public int scene { get; set; } //required in range scene.lengh
     public scene[] scenes { get; set; } = new scene[0];//requires scenes[scene] initialized
     public skin[] skins { get; set; } = new skin[0];
     public texture[] textures { get; set; } = new texture[0];

     public bool CheckRequirements()
     {
          foreach (var accessor in accessors)
               if(accessor.CheckRequirements(bufferViews.Length) == false) return false;
          foreach (var animation in animations)
               if(animation.CheckRequirements(accessors.Length, nodes.Length) == false) return false;
          if(asset.CheckRequirements() == false) return false;
          foreach (var buf in buffers)
               if(buf.CheckRequirements() == false) return false;
          foreach (var buf in bufferViews)
               if(buf.CheckRequirements(buffers.Length) == false) return false;
          foreach (var camera in cameras)
               if(camera.CheckRequirements() == false) return false;
          foreach (var image in images)
               if(image.CheckRequirements() == false) return false;
          foreach (var material in materials)
               if(material.CheckRequirements(textures.Length) == false) return false;
          foreach (var mesh in meshes)
               if(mesh.CheckRequirements() == false) return false;
          foreach (var node in nodes)
               if(node.CheckRequirements(cameras.Length, nodes.Length,
               skins.Length, meshes.Length) == false) return false;
          foreach (var sampler in samplers)
               if(sampler.CheckRequirements() == false) return false;
          if(scene < 0 || scene >= scenes.Length) return false;
          foreach (var scen in scenes)
               if(scen.CheckRequirements(nodes.Length) == false) return false;
          foreach (var skin in skins)
               if(skin.CheckRequirements(accessors.Length, nodes.Length) == false) return false;
          foreach (var texture in textures)
               if(texture.CheckRequirements(samplers.Length, images.Length) == false) return false;
          return true;
     }
}