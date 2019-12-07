using _3DConfigurator.Models;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Threading.Tasks;

namespace _3DConfigurator.Services
{
    public interface IEditGltfService
    {
        public GltfModel PopulateGltfModel(string objectAdres, string saveadres);
        public IEnumerable<Bitmap> CreateTextures(GltfModel gltfModel);

    }
}
