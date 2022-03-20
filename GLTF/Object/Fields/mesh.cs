
using System.Text.Json;

namespace GLTF.Object.Fieslds;

public class mesh
{
    public mesh(string name, primitive[] primitives, double[] weights)
    {
        this.name = name;
        this.primitives = primitives;
        this.weights = weights;
    }
    public string name { get; private set; }
    public primitive[] primitives { get; private set; }
    public double[] weights { get; private set; }
}

public class primitive
{
    public primitive(Dictionary<string, int> attributes, int mode, JsonDocument targets, material? material = null, accessor? indices = null)
    {
        this.attributes = attributes;
        this.indices = indices;
        this.mode = mode;
        this.material = material;
        this.targets = targets;
    }
    public Dictionary<string, int> attributes { get; set; }
    public accessor? indices { get; set; }//can be null
    public int mode { get; set; } = 4;//one from range [0, 6] //[GL_POINTS, TRIANGLE_FAN]
    public material? material { get; set;}//can be null
    public JsonDocument targets {get; set;}
}