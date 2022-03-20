
using System.Text;
using System.Text.Json;
using System.Text.Json.Serialization;

using GLTF;

class Program
{
    public const string robot = "https://raw.githubusercontent.com/KhronosGroup/glTF-Sample-Models/master/2.0/BrainStem/glTF/BrainStem.gltf";
    static void Main(string[] args)
    {
        HttpClient client = new HttpClient();

        string gltfjson = client.GetAsync(robot).Result.Content.ReadAsStringAsync().Result;

        var gltf = JsonSerializer.Deserialize<GLTF.GLTF>(gltfjson);   

        System.Console.WriteLine(gltf.CheckRequirements());
    }
}