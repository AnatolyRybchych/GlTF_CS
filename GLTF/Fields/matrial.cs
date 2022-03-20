
using static GLTF.Fields.material.Fields;

namespace GLTF.Fields;

public class material
{
    public string name = "";
    public pbrMetallicRoughness pbrMetallicRoughness { get; set; } = new pbrMetallicRoughness();
    public normalTextureInfo normalTexture { get; set; } = new normalTextureInfo();
    public occlusionTextureInfo occlusionTexture { get; set; } = new occlusionTextureInfo();
    public textureInfo emissiveTexture { get; set; } = new textureInfo();
    public double[] emissiveFactor { get; set; } = new double[]{0, 0, 0};//requires lengh = 3, each in range [0, 1.0]
    public string? alphaMode { get; set; } = null;//required one of {"OPAQUE", "MASK", "BLEND"}
    public double alphaCutoff { get; set; } = 0.5;//minimum: 0.0
    public bool doubleSided { get; set; } = false;


    public static class Fields
    {
        public class occlusionTextureInfo: textureInfo
        {
            public double strength { get; set; } = 1.0;//requires value in range [0, 1.0]
        }

        public class normalTextureInfo: textureInfo
        {
            public double scale { get; set; } = 1.0;//requires value in range [0, 1.0]
        }

        public class pbrMetallicRoughness
        {
            public double[] baseColorFactor { get; set; } = new double[]{1,1,1,1};//requires lengh = 4 and each element  in range [0, 1.0]
            public textureInfo baseColorTexture { get; set; }  = new textureInfo();
            public double metallicFactor { get; set; }  = 1.0;//requires value in range [0, 1.0]
            public double roughnessFactor { get; set; }  = 1.0;//requires value in range [0, 1.0]
            public textureInfo metallicRoughnessTexture { get; set; }  = new textureInfo();
        }

        public class textureInfo
        {
            public int? index { get; set; } = null; //requires not null
            public uint texCoord { get; set; } = 0; // to set GL_TEXCOORD_[texCoord]
        }
    }
}