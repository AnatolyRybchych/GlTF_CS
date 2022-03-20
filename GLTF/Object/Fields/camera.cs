
namespace GLTF.Object.Fieslds;

public class camera
{
    private camera(string name, perspective? perspective, orthographic? orthographic)
    {
        this.name = name;
        if(perspective != null)
        {
            IsPerspective = true;
            this.perspective = perspective; 
        }
        else
        {
            IsPerspective = false;
            this.perspective = new perspective(-1, 1, 1, 1, false);
        }


        if(orthographic != null)
        {
            IsOrthographic = true;
            this.orthographic = orthographic;
        }
        else
        {
            IsOrthographic = false;
            this.orthographic = new orthographic(-1, 1, 1, -1); 
        }
    }

    public camera(string name, perspective perspective)
        :this(name, perspective, null)
    {
    }

    public camera(string name, orthographic orthographic)
        :this(name, null, orthographic)
    {
    }

    public string name { get; private set; }
    public bool IsPerspective { get; private set; }
    public bool IsOrthographic { get; private set; }
    public perspective perspective { get; private set; }
    public orthographic orthographic { get; private set; }
 
}

public class perspective
{
    public perspective(double znear, double? zfar, double yfov, double aspectRatio, bool infiniteProjection)
    {
        if(infiniteProjection || zfar == null)
        {
            this.infiniteProjectionMatrix = true;
            zfar = 99999;
        }
        else
        {
            infiniteProjectionMatrix = false;
            this.zfar = zfar.Value;
        }

        this.znear = znear;
        this.yfov = yfov;
        this.aspectRatio = aspectRatio;
    }
    public bool infiniteProjectionMatrix { get; private set; }
    public double aspectRatio { get; private set; }//if null the aspect ratio of the rendering viewport **MUST** be used.
    public double yfov { get; private set; }// required not null
    public double zfar { get; private set; }//if null client implementations **SHOULD** use infinite projection matrix."
    public double znear { get; private set; }//required not null
}
public class orthographic
{
    public orthographic(double xmag, double ymag, double zfar, double znear)
    {
        this.zfar = zfar;
        this.znear = znear;
        this.ymag = ymag;
        this.xmag = xmag;
    }
    public double xmag { get; private set; }
    public double ymag { get; private set; }
    public double zfar { get; private set; }
    public double znear { get; private set; }
}