
namespace GLTF.Fields;

public class scene
{
    public string name { get; set; } = "";
    public int[] nodes { get; set; } = new int[0];//requirea each is in range of count gltf nodes

    public bool CheckRequirements(int nodesCount)
    {
        return nodes.Where(n => n < 0 || n >= nodesCount).Count() == 0;
    }
}