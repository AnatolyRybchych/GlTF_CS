
namespace GLTF.Object.Fieslds;

public class scene
{
    public string name { get; private set; }
    public node[] nodes { get; private set; }
    public scene(string name, node[] nodes)
    {
        this.name = name;
        this.nodes = nodes;
    }
}