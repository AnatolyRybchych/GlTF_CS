
namespace GLTF.Object.Fieslds;

public class sampler
{
    public sampler(string name, int magFilter, int minFilter, int wrapS, int wrapT)
    {
        this.name = name;
        this.magFilter = magFilter;
        this.minFilter = minFilter;
        this.wrapS = wrapS;
        this.wrapT = wrapT;
    }
    public string name { get; private set; }
    public int magFilter { get; private set; } 
    public int minFilter { get; private set; } 
    public int wrapS { get; private set; }
    public int wrapT { get; private set; }
}