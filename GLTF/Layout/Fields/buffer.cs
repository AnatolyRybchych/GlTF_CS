
namespace GLTF.Layout.Fields;


public class buffer
{
    public string name = "";
    public string uri { get; set; } = ""; // required path to file of "data:.*" for inline init
    public int byteLength { get; set; }// required >= 1
    
    public bool CheckRequirements()
    {
        return byteLength >= 1;
    }
}