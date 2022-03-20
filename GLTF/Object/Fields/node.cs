

namespace GLTF.Object.Fieslds;

public class node
{
    private node(string name, camera? camera, List<node> children, int? skinIndex, double[] weights)
    {
        this.name = name;
        this.camera = camera;
        this.children = children;
        this.skinIndex = skinIndex;
        this.weights = weights;
        this.matrix = new double[0];
        this.skin = null;
        translation = new double[0];
        scale = new double[0];
        rotation = new double[0];
    }

    public void SetSkin(List<skin> skins)
    {
        if(skinIndex.HasValue)
        {
            if(skinIndex.Value < 0 || skinIndex.Value >= skins.Count) 
                throw new ArgumentException($"node '{name}' has reference for undefined skin");
            
            this.skin = skins[skinIndex.Value];
        }
    }

    public node(string name, camera? camera, List<node> childrend, int? skinIndex, double[] weights, double[] matrix)
        :this(name, camera, childrend, skinIndex, weights)
    {
        if(matrix.Length != 16) throw new ArgumentException("matrix should be double[16]");
        UseMatrix = true;
        this.matrix = matrix;
        this.scale = new double[] {1,1,1};
        this.translation = new double[] {0,0,0};
        this.rotation = new double[] {0.0, 0.0, 0.0, 1.0};
    }

    public node(string name, camera? camera, List<node> childrend, int? skinIndex, double[] weights, 
    double[] rotation, double[] scale, double[] translation)
        :this(name, camera, childrend, skinIndex, weights)
    {
        if(rotation.Length != 4 ) throw new ArgumentException("rotation sholud be double[4]");
        if(scale.Length != 3 ) throw new ArgumentException("scale sholud be double[3]");
        if(translation.Length != 3 ) throw new ArgumentException("rotation sholud be double[3]");

        UseMatrix = false;
        matrix = new double[] {1.0, 0.0, 0.0, 0.0, 0.0, 1.0, 0.0, 0.0, 0.0, 0.0, 1.0, 0.0, 0.0, 0.0, 0.0, 1.0};
        this.translation = translation;
        this.rotation = rotation;
        this.scale = scale;
    }

    public double[] rotation { get; private set; }
    public double[] scale { get; private set; }
    public double[] translation {get; private set; }
    public int? skinIndex { get; private set; }
    public string name { get; private set; }
    public camera? camera { get; private set; }
    public List<node> children { get; private set; }
    public skin? skin { get; set; }
    public bool UseMatrix { get; private set; }

    //double[16]
    public double[] matrix { get; private set; }

    public double[] weights { get; private set;}
}