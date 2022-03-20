
namespace GLTF.Layout.Fields;


public class skin
{
    public string name { get; set; } = "";
    public int? inverseBindMatrices { get; set; } = null;//if not null required value in range accessors.lengh
    public int? skeleton { get; set; } = null;//if not null required value in range nodes.lengh
    public int[] joints { get; set; } = new int[0];//each in range nodes.lengh

    public bool CheckRequirements(int accessorsCount, int nodesCount)
    {
        return (inverseBindMatrices == null || (inverseBindMatrices >= 0 && inverseBindMatrices < accessorsCount))
        && (skeleton == null || (skeleton >= 0 && skeleton < nodesCount))
        && (joints.Where(j => j < 0 || j >= nodesCount).Count() == 0);
    }
} 