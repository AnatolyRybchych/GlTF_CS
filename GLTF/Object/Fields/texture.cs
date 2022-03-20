
namespace GLTF.Object.Fieslds;

public class texture
{
    public texture(string name, sampler sampler, image source)
    {
        this.name = name;
        this.sampler = sampler;
        this.source = source;
    }
    public string name { get; private set; }
    public image source { get; private set; }
    public sampler sampler { get; private set; }
}