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

        public Textures AddImageToTexture(Textures texture, string newTexturePath, string name);

        public SharpGLTF.Schema2.Image ReadImage(string imagePath, GltfModel gltfModel, string imageName);

        public void SaveCurrentGltf(GltfModel gltfmodel, string saveAdres);

        public Materials AddNewMaterialtoGLTFWithTextures(GltfModel gltfmodel, string materialName);

        public void AddUploadedImageToSelectedTexture(SharpGLTF.Schema2.Texture texture, IFormFile NewTexture);
    }
}
