
namespace GLTF.Fields;

public class scene
{
    public string name { get; set; } = "";
    public int[] nodes { get; set; } = new int[0];//requirea each is in range of count gltf nodes
}