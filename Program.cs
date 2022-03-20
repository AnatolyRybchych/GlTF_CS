
using System.Text;
using System.Text.Json;
using System.Text.Json.Serialization;
using OpenTK.Windowing.Desktop;
using OpenTK.Graphics.OpenGL;
using static OpenTK.Graphics.OpenGL.GL;

using GLTF;
using GLTF.Layout;
using GLTF.Object;

class Program
{
    public static ObjectGLTF? obj = null;
    public static GameWindow Window;
    public const string robot = "https://raw.githubusercontent.com/KhronosGroup/glTF-Sample-Models/master/2.0/BrainStem/glTF/BrainStem.gltf";
    static void Main(string[] args)
    {
        HttpClient client = new HttpClient();

        string gltfjson = client.GetAsync(robot).Result.Content.ReadAsStringAsync().Result;

        var gltf = JsonSerializer.Deserialize<LayoutGLTF>(gltfjson);   
        if(gltf == null || !gltf.CheckRequirements()) Console.WriteLine("gltf file is corrupted");
        else
        {
            obj = new ObjectGLTF(gltf, 1);
        }

        while(Window.IsExiting == false)
        {
            Window.ProcessEvents();
    
            Render();
        }
    }

    public static void Render()
    {
        ClearColor(1,0,0,1);
        Clear(ClearBufferMask.ColorBufferBit);
    }


    static Program()
    {
        Window = new GameWindow(new GameWindowSettings(){

        },
        new NativeWindowSettings(){
            Size = new OpenTK.Mathematics.Vector2i(800, 600),
        });
    }
}