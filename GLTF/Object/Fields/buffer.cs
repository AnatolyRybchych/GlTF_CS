
namespace GLTF.Object.Fieslds;

public class buffer
{
    public buffer(string name, string uri, int byteLength)
    {
        this.name = name;
        this.uri = uri;
        this.byteLength = byteLength;
        data = new byte[0];
    }

    public delegate byte[] BufferReadingHandler(string uri);

    public void ReadData(BufferReadingHandler readinghandler)
    {
        this.data = readinghandler(uri);
    }
    public string name { get; private set; }
    public string uri { get; private set; }
    public int byteLength { get; private set; }
    public byte[] data { get; private set; }
}