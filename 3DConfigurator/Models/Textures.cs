using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using static System.Net.Mime.MediaTypeNames;

namespace _3DConfigurator.Models
{
    public class Textures
    {
        public SharpGLTF.Schema2.Texture Texture { get; set; }
        public SharpGLTF.Schema2.Image Image { get; set; }
        public string Name { get; set; }
    }
}
