
namespace GLTF.Object.Fieslds;

public class skin
{
    public string name { get; private set; }
    public accessor? inverseBindMatrices { get; private set; }
    public node? skeleton { get; private set; }
    public node[] joints { get; private set; }

    public skin(string name, node[] joints, node? skeleton, accessor? inverseBindMatrices)
    {
        this.name = name;
        this.inverseBindMatrices = inverseBindMatrices;
        this.skeleton = skeleton;
        this.joints = joints;
    }
    
}