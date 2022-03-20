
namespace GLTF.Object.Fieslds;

public class material
{
    public material(string name, pbrMetallicRoughness? metallicRoughness, normalTextureInfo? normalTexture,
        occlusionTextureInfo? occlusionTexture, textureInfo? emissiveTexture, double[] emissiveFactor,
        alphaMode alphaMode, double alphaCutoff, bool doubleSided)
    {
        this.name = name;
        this.pbrMetallicRoughness = metallicRoughness;
        this.normalTexture = normalTexture;
        this.occlusionTexture = occlusionTexture;
        this.emissiveTexture = emissiveTexture;
        this.emissiveFactor = emissiveFactor;
        this.alphaMode = alphaMode;
        this.alphaCutoff = alphaCutoff;
        this.doubleSided = doubleSided;
    }
    public string name { get; private set; }
    public pbrMetallicRoughness? pbrMetallicRoughness { get; private set; } = null;
    public normalTextureInfo? normalTexture { get; private set; } = null;
    public occlusionTextureInfo? occlusionTexture { get; private set; } = null;
    public textureInfo? emissiveTexture { get; private set; } = null;
    public double[] emissiveFactor { get; private set; } = new double[]{0, 0, 0};
    public alphaMode alphaMode { get; private set; } = alphaMode.OPAQUE;
    public double alphaCutoff { get; private set; } = 0.5;
    public bool doubleSided { get; private set; } = false;
}

public class pbrMetallicRoughness
{
    public pbrMetallicRoughness(double metallicFactor, double roughnessFactor,
    textureInfo? baseColorTexture, textureInfo? metalicRogughnessTexture, double[] baseColorFactor)
    {
        if(baseColorFactor.Length != 4) throw new ArgumentException("baseColorFactor sholud be double[4]");
        this.metallicFactor = metallicFactor;
        this.roughnessFactor = roughnessFactor;
        this.baseColorTexture = baseColorTexture;
        this.metallicRoughnessTexture = metalicRogughnessTexture;
        this.baseColorFactor = baseColorFactor;
    }
    public double[] baseColorFactor { get; private set; }//requires lengh = 4 and each element  in range [0, 1.0]
    public textureInfo? baseColorTexture { get; private set; }
    public double metallicFactor { get; private set; }  = 1.0;//requires value in range [0, 1.0]
    public double roughnessFactor { get; private set; }  = 1.0;//requires value in range [0, 1.0]
    public textureInfo? metallicRoughnessTexture { get; private set; }
}

public class occlusionTextureInfo: textureInfo
{
    public occlusionTextureInfo(texture texture, int texCoord, double strength) : base(texture, texCoord)
    {
        this.strength = strength;
    }

    public double strength { get; private set; } = 1.0;//requires value in range [0, 1.0]
}
public class normalTextureInfo: textureInfo
{
    public normalTextureInfo(texture texture, int texCoord, double scale) : base(texture, texCoord)
    {
        this.scale = scale;
    }

    public double scale { get; private set; } = 1.0;//requires value in range [0, 1.0]
}

public class textureInfo
{
    public textureInfo(texture texture, int texCoord)
    {
        this.texCoord = texCoord;
        this.texture = texture;
    }
    public texture texture { get; private set; } //requires not null
    public int texCoord { get; private set; }
}

public enum alphaMode
{
    OPAQUE,
    MASK,
    BLEND
}