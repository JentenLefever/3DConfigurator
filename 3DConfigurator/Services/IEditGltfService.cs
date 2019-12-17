using _3DConfigurator.Models;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Threading.Tasks;

namespace _3DConfigurator.Services
{
    public interface IEditGltfService
    {
        public GltfModel PopulateGltfModel(string objectAdres);
        public IEnumerable<Bitmap> CreateTextures(GltfModel gltfModel);


        public SharpGLTF.Schema2.Image ReadImage(string imagePath, GltfModel gltfModel, string imageName);

        public void SaveCurrentGltf(GltfModel gltfmodel, string saveAdres);


        public void AddUploadedImageToSelectedTexture(SharpGLTF.Schema2.Texture texture, IFormFile NewTexture);
    }
}
