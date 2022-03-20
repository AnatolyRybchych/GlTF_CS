
namespace GLTF.Object.Fieslds;

public class image
{
    public image(string name, string mimeType, bufferView? bufferView, string? uri)
    {
        this.name = name;
        this.mimeType = mimeType;
        this.uri = uri;
        this.bufferView = bufferView;

        if(uri == null)
        {
            if(bufferView == null) throw new ArgumentException($"image '{name}' should have buffer view or uri");
        }
        else
        {
            if(mimeType == null) throw new ArgumentException($"image '{name}' should have mime type");
        }
    }
    

    public string name { get; private set; }
    public string? uri { get; private set; }
    public string mimeType { get; private set; }
    public bufferView? bufferView { get; private set; }

}