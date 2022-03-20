
namespace GLTF.Layout.Fields;

public class image
{
    public string name = "";
    public string? uri { get; set; } = null; //required not null if bufferView is null, if not null required path to file or "data:.*"
    public string? mimeType { get; set; }//required nut null if bufferView != null
    public int? bufferView { get; set; } = null;//required not null if uri is null 

    public bool CheckRequirements()
    {
        return (bufferView != null && mimeType != null )
        || (uri != null);
    }
}