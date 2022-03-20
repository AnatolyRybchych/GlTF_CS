# gltf parser
gltf specification v2 parser (maybe abstract wrapper)

## offitial format specification:
https://github.com/KhronosGroup/glTF/tree/main/specification/2.0/schema


## code example

```c#
var gltfText = File.ReadAllText("gltf_file.gltf");
var gltfLayout = JsonSerializer.Deserialize<LayoutGLTF>(gltfText);

ObjectGLTF? gltf = null;

if(gltfLayout == null || !gltfLayout.CheckRequirements())
{
    //!gltfLayout.CheckRequirements() can be unused if u know that file is not corruped
    Console.Error.WriteLine("gltf file has not requires specification v2.0")
}
else
{
    try//can be unused if used uncorrupted file
    {
        ogltfbj = new ObjectGLTF(gltfLayout, 1);
    }
    catch (System.Exception e)//throws exceptions about structure
    {
        Console.Error.WriteLine(e.Message);
    }
}

assert(gltf != null)
```