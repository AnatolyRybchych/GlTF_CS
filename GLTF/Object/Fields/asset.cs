
namespace GLTF.Object.Fieslds;


public class asset
{
    public asset(string version, string? generator = null, string? minVersion = null, string? copyright = null)
    {
        this.version = version;
        this.generator = generator ?? "";
        this.minVersion = minVersion ?? "";
        this.copyright = copyright ?? "";
    }

    public string generator { get; private set; }
    public string version { get; private set; }
    public string copyright { get; private set; }
    public string minVersion { get; private set; }
}

