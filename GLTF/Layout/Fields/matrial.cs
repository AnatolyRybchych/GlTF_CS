
using static GLTF.Layout.Fields.material.Fields;

namespace GLTF.Layout.Fields;

public class material
{
    public string name { get; private set; }= "";
    public pbrMetallicRoughness? pbrMetallicRoughness { get; set; } = null;
    public normalTextureInfo? normalTexture { get; set; } = null;
    public occlusionTextureInfo? occlusionTexture { get; set; } = null;
    public textureInfo? emissiveTexture { get; set; } = null;
    public double[] emissiveFactor { get; set; } = new double[]{0, 0, 0};//requires lengh = 3, each in range [0, 1.0]
    public string alphaMode { get; set; } = "OPAQUE";//required one of {"OPAQUE", "MASK", "BLEND"}
    public double alphaCutoff { get; set; } = 0.5;//minimum: 0.0
    public bool doubleSided { get; set; } = false;

    public bool CheckRequirements(int texturesCount)
    {
        return new string[] {"OPAQUE", "MASK", "BLEND"}.Contains(alphaMode)
        && alphaCutoff >= 0 
        && (emissiveTexture == null || emissiveTexture.CheckRequirements(texturesCount))
        && (occlusionTexture == null || occlusionTexture.CheckRequirements(texturesCount))
        && (normalTexture == null || normalTexture.CheckRequirements(texturesCount))
        && (pbrMetallicRoughness?.metallicRoughnessTexture == null 
            || pbrMetallicRoughness.metallicRoughnessTexture.CheckRequirements(texturesCount))
        && (pbrMetallicRoughness?.baseColorTexture == null 
            || pbrMetallicRoughness.baseColorTexture.CheckRequirements(texturesCount))
        && emissiveFactor.Length == 3;
    }


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
            public textureInfo? baseColorTexture { get; set; }  = null;
            public double metallicFactor { get; set; }  = 1.0;//requires value in range [0, 1.0]
            public double roughnessFactor { get; set; }  = 1.0;//requires value in range [0, 1.0]
            public textureInfo? metallicRoughnessTexture { get; set; }  = null;
        }

        public class textureInfo
        {
            public int? index { get; set; } = null; //requires not null
            public int texCoord { get; set; } = 0; // to set GL_TEXCOORD_[texCoord]

            public bool CheckRequirements(int texturesCount)
            {
                return index != null && index >= 0 && index < texturesCount;
            }
        }
    }
}