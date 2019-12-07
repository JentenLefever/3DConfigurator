using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace _3DConfigurator.Models
{
    public class Materials
    {
        public SharpGLTF.Schema2.Material Material { get; set; }
        public IEnumerable<Channels> Channels { get; set; }
    }
}
