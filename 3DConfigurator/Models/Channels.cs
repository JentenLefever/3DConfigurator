using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace _3DConfigurator.Models
{
    public class Channels
    {

        public SharpGLTF.Schema2.MaterialChannel Channel { get; set; }
        public IEnumerable<Textures> Textures { get; set; }
    }
}
