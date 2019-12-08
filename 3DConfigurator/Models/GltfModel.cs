using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace _3DConfigurator.Models
{
    public class GltfModel
    {
        public string Name { get; set; }
        public SharpGLTF.Schema2.ModelRoot gltf { get; set; }
        public IEnumerable<Meshes> MeshesVerzameling { get; set; }

       
    }
   
    

}
