

namespace GLTF;


public class sampler
{
    public string name = "";
    public int magFilter { get; set; } = 9729;// required one of {9728, 9729}
    public int minFilter { get; set; } = 9729;// required one of {9728, 9729, 9984, 9985, 9986, 9987}
    public int wrapS { get; set; } = 10497;// required one of {33071, 33648, 33648}
    public int wrapT { get; set; } = 10497;// required one of {33071, 33648, 33648}

}