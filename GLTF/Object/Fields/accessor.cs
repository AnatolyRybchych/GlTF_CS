
using GLTF.Object.Fieslds;

namespace GLTF.Object;

public class accessor
{
    public accessor(string name, bufferView bufferView, int byteOffset, int componentType, bool normalized, int count,
        type type, double[] max, double[] min, sparse? sparse)
    {
        this.name = name;
        this.bufferView = bufferView;
        this.byteOffset = byteOffset;
        this.componentType = componentType;
        this.normalized = normalized;
        this.count = count;
        this.type = type;
        this.max = max;
        this.min = min;
        this.sparse = sparse;

    }
    public string name { get; private set; }
    public bufferView bufferView { get; private set; }
    public int byteOffset { get; private set; }
    public int componentType { get; private set; }
    public bool normalized { get; private set; }

    public int count { get; private set; } 
    public type type { get; private set; }
    public double[] max { get; private set; } = new double[0];// values > 0 && values <= 16
    public double[] min { get; private set; } = new double[0];// values > 0 && values <= 16
    public sparse? sparse { get; private set; }//can be null

}

public class sparse
{
    public sparse(int count, indices indices, values values)
    {
        this.count = count;
        this.indices = indices;
        this.values = values;
    }
    public int count { get; private set; }
    public indices indices { get; private set; }
    public values values { get; private set; }
}


public class values
{
    public values(bufferView bufferView, int byteOffset)
    {
        this.bufferView = bufferView;
        this.byteOffset = byteOffset;
    }
    public bufferView bufferView { get; private set; }
    public int byteOffset { get; private set; }
}
public class indices
{
    public indices(bufferView bufferView, int byteOffset, int componentType)
    {
        this.bufferView = bufferView;
        this.byteOffset = byteOffset;
        this.componentType = componentType;
    }
    public bufferView bufferView { get; private set; }
    public int byteOffset { get; private set; }
    public int componentType { get; private set; }
}

public enum type
{
    SCALAR,
    VEC2,
    VEC3,
    VEC4,
    MAT2,
    MAT3,
    MAT4
}
