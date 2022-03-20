
namespace GLTF.Layout.Fields;

public class texture
{
    public string name = "";
    public int? sampler { get; set; } = null; //if not null, requires value in range samplers.lengh //When undefined, a sampler with repeat wrapping and auto filtering **SHOULD** be used."
    public int? source { get; set; } = null; //if not null, requires value in range images.lengh //When undefined, an extension or other mechanism **SHOULD** supply an alternate texture source, otherwise behavior is undefined.

    public bool CheckRequirements(int samplersCount, int imagesCount)
    {
        return (sampler == null || sampler >= 0 && sampler < samplersCount)
        && (source == null || source >= 0 && source < imagesCount);
    }
}