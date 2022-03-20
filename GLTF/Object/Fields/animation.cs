
namespace GLTF.Object.Fieslds;

public class animation
{
    public animation(string name, channel[] channels, animationSampler[] samplers)
    {
        this.name = name;
        this.channels = channels;
        this.samplers = samplers;
    }
    
    public string name;
    public channel[] channels { get; private set; }
    public animationSampler[] samplers { get; private set; } 
}

public class animationSampler
{
    public animationSampler(accessor input, accessor output, interpolation interpolation)
    {
        this.input = input;
        this.output = output;
        this.interpolation = interpolation;
    }
    public accessor input { get; private set; } 
    public accessor output { get; private set; } 
    public interpolation interpolation { get; private set; }//required one of {"LINEAR", "STEP", "CUBICSPLINE"}
}
public enum interpolation
{
    LINEAR,
    STEP,
    CUBICSPLINE
}

public enum animationPath
{
    translation,
    rotation,
    scale,
    weights
}
public class channel
{
    public channel(animationSampler sampler, target target)
    {
        this.sampler = sampler;
        this.target = target;
    }
    public animationSampler sampler { get; private set; }
    public target target { get; private set; }
}
public class target
{
    public target(node node, animationPath path)
    {
        this.node = node;
        this.path = path;
    }
    public node node { get; private set; }
    public animationPath path { get; set; }
}