using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace _3DConfigurator.Models
{
    public class Meshes
    {
       public SharpGLTF.Schema2.Mesh Mesh { get; set; }
        public Materials Material { get; set; }
        public IEnumerable<Materials> Materials { get; set;}
    }
}
