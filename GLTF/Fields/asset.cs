
using System.Text.RegularExpressions;

namespace GLTF;

public class asset
{
    public string? generator { get; set; }
    public string version { get; set; } = "";// required pattern ^[0-9]+\\.[0-9]+$"
    public string? copyright { get; set; }
    public string? minVersion { get; set; } = null;// can be null or required pattern ^[0-9]+\\.[0-9]+$"

    public bool CheckRequirements()
    {
        return Regex.IsMatch(version, "^[0-9]+\\.[0-9]+$") 
        && ((minVersion != null && Regex.IsMatch(minVersion, "^[0-9]+\\.[0-9]+$"))
        || (minVersion == null)) ;
    }
}