
namespace GLTF.Object.Fieslds;

public class bufferView
{
    public bufferView(string name, buffer buffer, int byteOffset, int byteLength, int byteStride, int target)
    {
        this.name = name;
        this.buffer = buffer;
        this.byteOffset = byteOffset;
        this.byteLength = byteLength;
        this.byteStride = byteStride;
        this.target = target;
    }
    public string name { get; private set; }
    public buffer buffer { get; private set; } 
    public int byteOffset { get; private set; }
    public int byteLength { get; private set; }
    public int byteStride { get; private set; }
    public int target { get; private set; }
}