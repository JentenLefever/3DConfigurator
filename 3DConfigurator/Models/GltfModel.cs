using _3DConfigurator.Models.Identity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace _3DConfigurator.Models
{
    public class GltfModel
    {
        public string Name { get; set; }
        
        public SharpGLTF.Schema2.ModelRoot gltf { get; set; }
        [NotMapped]
        public IEnumerable<Meshes> MeshesVerzameling { get; set; }
        public int Id { get; set; }
        public string SaveAdres { get; set; }
    }
   
    

}
