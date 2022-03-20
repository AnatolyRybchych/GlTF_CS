
namespace GLTF.Fields;


public class skin
{
    public string name { get; set; } = "";
    public int? inverseBindMatrices { get; set; } = null;//if not null required value in range accessors.lengh
    public int? skeleton { get; set; } = null;//if not null required value in range nodes.lengh
    public int[] joints { get; set; } = new int[0];//each in range nodes.lengh
} 