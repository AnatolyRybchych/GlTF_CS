
namespace GLTF.Fields;

public class texture
{
    public string name = "";
    public int? sampler { get; set; } = null; //if not null, requires value in range samplers.lengh
    public int? source { get; set; } = null; //if not null, requires value in range images.lengh
}