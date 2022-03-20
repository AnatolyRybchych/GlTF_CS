
namespace GLTF.Fields;

public class node
{
    public string name = "";
    public int? camera { get; set; } = null;//camera id
    public int[] children { get; set; } = new int[0];// each element shold be node id
    public int? skin { get; set; } = null; //required not null, skin id
    public double[]? matrix { get; set; } = null;//required lengh = 16, default = new double[16] {1.0, 0.0, 0.0, 0.0, 0.0, 1.0, 0.0, 0.0, 0.0, 0.0, 1.0, 0.0, 0.0, 0.0, 0.0, 1.0};
    public int? mesh { get; set; } = null;//required not null, mesh id
    public double[] rotation { get; set; } = new double[]{0, 0, 0, 1};//required lengh
    public double[] scale { get; set; } = new double[]{1, 1, 1};//required lengh = 3    
    public double[] transplation { get; set; } = new double[]{0, 0, 0};//required lengh = 3
    public double[]? weights { get; set; } = null;//can be null, required lengh = morphObjects.lengh 

    public bool CheckRequirements(int camersCount, int nodesCount, int skinsCount, int meshesCount)
    {
        return 
        (camera == null || (camera >= 0 && camera < camersCount))
        && (children.Where(c => c < 0 || c >= nodesCount).Count() == 0)
        && (skin == null || ( skin >= 0 && skin < skinsCount))
        && (matrix == null || matrix.Length == 16)
        && (mesh == null || (mesh >= 0 && mesh < meshesCount))
        && rotation.Length == 4 && scale.Length == 3
        && transplation.Length == 3;
    }
}