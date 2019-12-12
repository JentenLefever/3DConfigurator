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
            List<Materials> materialsInMesh = new List<Materials>();
            List<Channels> channelInMaterials = new List<Channels>();

            //Populate Models
            foreach (var item in gltfModel.gltf.LogicalMeshes)
            {
                Meshes mesh = new Meshes();
                mesh.Mesh = item;
                meshesInGltf.Add(mesh);
                

                foreach (var prim in item.Primitives)
                {
                    Materials material = new Materials();
                    material.Material = prim.Material;
                    materialsInMesh.Add(material);
                    
                    foreach (var chan in prim.Material.Channels)
                    {
                        Channels channel = new Channels();
                        channel.Channel = chan;
                        channelInMaterials.Add(channel);
                        
                        Textures texture = new Textures();
                        texture.Texture = chan.Texture;
                        //if (chan.Texture.Image !=null)
                        //{
                        //    texture.Texture.Image = chan.Texture.Image;
                        //    texture.Texture.Name = chan.Texture.Name;
                        //}
                    }
                    material.Channels = channelInMaterials;
                }
                mesh.Materials = materialsInMesh;
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

        public Textures AddImageToTexture(Textures texture, string newTexturePath, string name)
        {
            var image = texture.Texture.LogicalParent.CreateImage(name);
            image.SetSatelliteFile(newTexturePath);
            texture.Image = image;
            return texture;
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
        }

        public Materials AddNewMaterialtoGLTFWithTextures(GltfModel gltfmodel, string materialName)
        {
            Materials newmaterial = new Materials();
            newmaterial.Material = gltfmodel.gltf.CreateMaterial(materialName);
            var i = 0;
            foreach (var item in newmaterial.Material.Channels)
            {
                Textures newMaterialTexture = new Textures();
                newMaterialTexture.Image = ReadImage("wwwwroot/Textures/Texture" + i + ".png", gltfmodel,item.Key);
                item.Texture.Image = newMaterialTexture.Image;
                i++;
            }
            return newmaterial;
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
        }


        public void LoadImageFromTexture(SharpGLTF.Schema2.Texture texture)
        {
            var image = Bitmap.FromStream(texture.Image.OpenImageFile());
            var filePath = Path.Combine(Directory.GetCurrentDirectory(), @"wwwroot\Textures", "SelectedImage.png");
            image.Save(filePath);
        }
    }
}
