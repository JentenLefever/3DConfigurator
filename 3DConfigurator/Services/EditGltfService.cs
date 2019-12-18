using _3DConfigurator.Models;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;

namespace _3DConfigurator.Services
{
    public class EditGltfService : IEditGltfService
    {

        private readonly IHostingEnvironment _env;

        public EditGltfService(IHostingEnvironment env)
        {
            _env = env;
           ;
        }

        public void UploadGLTFModel(IFormFile NewModel)
        {
            var filePath = Path.Combine(Directory.GetCurrentDirectory(), @"wwwroot\Objects", "Current.glb");
            var filestream = new FileStream(filePath, FileMode.Create);
            NewModel.CopyTo(filestream);
            filestream.Dispose();



        }
        //Create GltfModel
        public GltfModel PopulateGltfModel(string objectAdres)
        {
            //Load Object
            GltfModel gltfModel = new GltfModel();
            gltfModel.gltf = SharpGLTF.Schema2.ModelRoot.Load(objectAdres);
            gltfModel.Name = Path.GetFileName(objectAdres);
            //Object property lists
            List<Meshes> meshesInGltf = new List<Meshes>();
           

            //Populate Models
            foreach (var item in gltfModel.gltf.LogicalMeshes)
            {
                Meshes mesh = new Meshes();
                mesh.Mesh = item;
                meshesInGltf.Add(mesh);
                
            }
gltfModel.MeshesVerzameling = meshesInGltf;

            return gltfModel;
        }

        public IEnumerable<Bitmap> CreateTextures(GltfModel gltfModel)
        {
            List<Bitmap> channelImages = new List<Bitmap>();
            var i = 0;
            foreach (var item in gltfModel.gltf.LogicalImages)
            {
                var img = Bitmap.FromStream(item.OpenImageFile());
                img.Save("wwwwroot/Textures/Texture" + i + ".png", ImageFormat.Png);
                i++;
            }
            return channelImages;
        }

       

        public SharpGLTF.Schema2.Image ReadImage(string imagePath,GltfModel gltfModel,string imageName)
            {
            var image = gltfModel.gltf.CreateImage(imageName);
            
            image.SetSatelliteFile(imagePath);
            return image;
        }

        public void SaveCurrentGltf(GltfModel gltfmodel, string saveAdres)
        {
            gltfmodel.gltf.SaveGLB(saveAdres);
            gltfmodel.gltf.SaveGLTF(Path.Combine(_env.WebRootPath, "Objects", "Current.gltf"));
        }

        public void AddUploadedImageToSelectedTexture(SharpGLTF.Schema2.Texture texture,IFormFile NewTexture)
        {
            var filePath = Path.Combine(Directory.GetCurrentDirectory(), @"wwwroot\Textures", "NewTexture.png");
            var filestream = new FileStream(filePath, FileMode.Create);
            NewTexture.CopyTo(filestream);
            filestream.Dispose();
           
            
                var image = texture.LogicalParent.CreateImage(NewTexture.FileName);
            
            image.SetSatelliteFile(filePath);

            texture.Image = image;
            texture.LogicalParent.SaveGLB(Path.Combine(_env.WebRootPath, "Objects", "Current.glb"));
            texture.LogicalParent.SaveGLTF(Path.Combine(_env.WebRootPath, "Objects", "Current.gltf"));
        }


        public void LoadImageFromTexture(SharpGLTF.Schema2.Texture texture)
        {
            var image = Bitmap.FromStream(texture.Image.OpenImageFile());
            var filePath = Path.Combine(Directory.GetCurrentDirectory(), @"wwwroot\Textures", "SelectedImage.png");
            image.Save(filePath);
        }
    }
}
