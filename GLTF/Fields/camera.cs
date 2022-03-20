
using static GLTF.Fields.camera.Fields;

namespace GLTF.Fields;


public class camera
{
    public string name = "";
    public orthographic? orthographic { get; set; }//requires not null if type "orthographic"
    public perspective? perspective { get; set; }// requires not null if type "perspective"
    public string type { get; set; } = "";//required one of {"perspective", "orthographic"}

    public static class Fields
    {
        public class perspective
        {
            public double? aspectRatio { get; set; } = null;//if null the aspect ratio of the rendering viewport **MUST** be used.
            public double? yfov { get; set; } = null;// required not null
            public double? zfar { get; set; } = null;//if null client implementations **SHOULD** use infinite projection matrix."
            public double? znear { get; set; } = null;//required not null
        }

        public class orthographic
        {
            public double? xmag { get; set; } = null;//required not null\
            public double? ymag { get; set; } = null;//required not null
            public double? zfar { get; set; } = null;//required not null
            public double? znear { get; set; } = null;//required not null
        }
    }
}