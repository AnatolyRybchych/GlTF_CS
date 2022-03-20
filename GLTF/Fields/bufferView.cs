
namespace GLTF.Fields;

public class bufferView
{
    public string name = "";
    public int? buffer { get; set; } = null;//required not null  index of buffer object 
    public int byteOffset { get; set; } = 0;
    public int byteLength { get; set; } = 0;//required >= 1
    public int? byteStride { get; set; } = null;//can be null or  >= 4 && <= 252
    public int target { get; set; } = 34962; //requires one of {34962, 34963} 

    public bool CheckRequirements(int buffersCount)
    {
        return new int[]{34962, 34963}.Contains(target)
        && buffer != null && byteLength >= 1 &&
        ((byteStride == null) || (byteStride >= 4 && byteStride <= 252))
        &&buffer >= 0 && buffer < buffersCount;
    }
    public static class Fields
    {
        public enum target: int
        {
            ARRAY_BUFFER = 34962,
            ELEMENT_ARRAY_BUFFER = 34963
        }
    }
}