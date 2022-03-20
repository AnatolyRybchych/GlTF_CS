
using static GLTF.Fields.animation.Fields;

namespace GLTF.Fields;


public class animation
{
    public string name = "";
    public channel[] channels { get; set; } = new channel[0];
    public animation.Fields.sampler[] samplers { get; set; } = new animation.Fields.sampler[0]; 

    public bool CheckRequirements(int accessorsCount, int nodesCount)
    {
        if(samplers.Where(
            s => s.input == null || s.output == null 
            || s.input < 0 || s.input >= accessorsCount 
            || s.output < 0 || s.output >= accessorsCount
            ).Count() != 0) return false;
        
        int samplesCount = samplers.Length;

        if(channels.Where(c => c.sampler == null || c.target == null 
            || c.sampler < 0 || c.sampler > samplesCount
            || c.target.node < 0 || c.target.node > nodesCount 
            || (new string[]{"translation", "rotation", "scale", "weights"}
            .Contains(c.target.path) == false)).Count() != 0) return false;


        return true;
    }

    public static class Fields
    {
        public class sampler
        {
            public int? input { get; set; } = null;//required not null in range of count accesours
            public int? output { get; set; } = null;//required not null in range of count accesours
            public string interpolation { get; set; } = "";//required one of {"LINEAR", "STEP", "CUBICSPLINE"}
        }

        public class channel
        {
            public int? sampler { get; set; } = null;//required not null  index of sampler object
            public target? target {get; set; } = null;//required not null
        }

        public class target
        {
            public int node { get; set; } //node index
            public string path { get; set;} = ""; // one of {"translation", "rotation", "scale", "weights"}
        }
    }
}