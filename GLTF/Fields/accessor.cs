

using static GLTF.Fields.accessor.Fields;
using static GLTF.Fields.accessor.Fields.sparse;
using static GLTF.Fields.accessor.Fields.sparse.Fields;

namespace GLTF.Fields;


public class accessor
{
    public string name = "";
    public int? bufferView { get; set; } = null;//required not null  index of bufferView object
    public int byteOffset { get; set; } = 0;//required >= 0
    public int componentType { get; set; } //required one of {5120, 5121, 5122, 5123, 5125, 5126}
    public bool normalized { get; set; } = false;
    public int count { get; set; } //required >= 1
    public string type { get; set; } = ""; //required one from { "SCALAR", "VEC2", "VEC3", "VEC4", "MAT2", "MAT3", "MAT4"}
    public double[] max { get; set; } = new double[0];// values > 0 && values <= 16
    public double[] min { get; set; } = new double[0];// values > 0 && values <= 16
    public sparse? sparse { get; set; }//can be null

    public bool CheckRequirements(int buffersViewCount)
    {
        if(bufferView == null || bufferView >= buffersViewCount || bufferView < 0) return false;
        if(byteOffset < 0) return false;
        if(new int[]{5120, 5121, 5122, 5123, 5125, 5126}.Contains(componentType) == false) return false;
        if(count < 1) return false;
        if(new string[]{"SCALAR", "VEC2", "VEC3", "VEC4", "MAT2", "MAT3", "MAT4"}.Contains(type) == false) return false;
        if(max.Where(num => num < 1 && num > 16).Count() != 0) return false;
        if(min.Where(num => num < 1 && num > 16).Count() != 0) return false;
        if(sparse != null)
        {
            if(sparse.indices == null | sparse.values == null) return false;
            if(sparse.indices?.bufferView == null || sparse.indices.bufferView > buffersViewCount 
                || sparse.indices.bufferView < 0) return false;
            if(sparse.values?.bufferView == null || sparse.values.bufferView > buffersViewCount 
                || sparse.values.bufferView < 0) return false;
            if(new int[]{5121, 5123, 5125}.Contains(sparse.indices.componentType) == false) return false;
        }

        return true;
    }

    public static class Fields
    {
        public class sparse
        {
            public int count { get; set; } // required >= 1;
            public indices? indices { get; set; } = null; //required not null
            public values? values { get; set; } = null;// required not null

            public static class Fields
            {
                public class values
                {
                    public int? bufferView { get; set; } = null;//required not null
                    public int byteOffset { get; set; } = 0;
                }
                public class indices
                {
                    public int? bufferView { get; set; } = null;//required not null
                    public int byteOffset { get; set; } = 0;
                    public int componentType { get; set; }//required one of {5121, 5123, 5125}
                }


                public enum componentType: int
                {
                    BYTE = 5120,
                    UNSIGNED_BYTE = 5121,
                    SHORT = 5122,
                    UNSIGNED_SHORT = 5123,
                    UNSIGNED_INT = 5125,
                    FLOAT = 5126,
                }
            }
        }
    }
}

