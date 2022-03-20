
using System.Text.Json;
using static GLTF.Fields.mesh.Fields;

namespace GLTF.Fields;

public class mesh
{
    public string name { get; set; } = "";
    public primitive[] primitives { get; set; } = new primitive[0];
    public double[]? weights { get; set; } = null;// if not null requires lengh >= 1

    public bool CheckRequirements()
    {
        return (weights == null || weights.Length >= 1);
    }

    public static class Fields
    {
        public class primitive
        {
            public Dictionary<string, int> attributes { get; set; } = new Dictionary<string, int>();
            public int? indices { get; set; } = null;//can be null
            public int mode { get; set; } = 4;//one from range [0, 6] //[GL_POINTS, TRIANGLE_FAN]
            public int? material { get; set;}//can be null
            public JsonDocument? targets {get; set;} = null;//can be null
        }
    }
}