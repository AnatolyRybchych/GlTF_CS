
namespace GLTF.Fields;

public class node
{
    public string name = "";
    public int? camera { get; set; } = null;//required not null, camera id
    public int[] children { get; set; } = new int[0];// each element shold be node id
    public int? skin { get; set; } = null; //required not null, skin id
    public double[]? matrix { get; set; } = null;//required lengh = 16, default = new double[16] {1.0, 0.0, 0.0, 0.0, 0.0, 1.0, 0.0, 0.0, 0.0, 0.0, 1.0, 0.0, 0.0, 0.0, 0.0, 1.0};
    public int? mesh { get; set; } = null;//required not null, mesh id
    public double[] rotation { get; set; } = new double[]{0, 0, 0, 1};//required lengh
    public double[] scale { get; set; } = new double[]{1, 1, 1};//required lengh = 3    
    public double[] transplation { get; set; } = new double[]{0, 0, 0};//required lengh = 3
    public double[]? weights { get; set; } = null;//can be null, required lengh = morphObjects.lengh 
}