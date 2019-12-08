using _3DConfigurator.Models;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;

namespace _3DConfigurator.Services
{
    public class EditGltfService : IEditGltfService
    {


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
                gltfModel.MeshesVerzameling = meshesInGltf;

                foreach (var prim in item.Primitives)
                {
                    Materials material = new Materials();
                    material.Material = prim.Material;
                    materialsInMesh.Add(material);
                    mesh.Materials = materialsInMesh;
                    foreach (var chan in prim.Material.Channels)
                    {
                        Channels channel = new Channels();
                        channel.Channel = chan;
                        channelInMaterials.Add(channel);
                        material.Channels = channelInMaterials;

                        Textures texture = new Textures();
                        texture.Texture = chan.Texture;
                        //if (chan.Texture.Image !=null)
                        //{
                        //    texture.Texture.Image = chan.Texture.Image;
                        //    texture.Texture.Name = chan.Texture.Name;
                        //}

                    }
                }
            }
            return gltfModel;
        }

        public IEnumerable<Bitmap> CreateTextures(GltfModel gltfModel)
        {
            List<Bitmap> channelImages = new List<Bitmap>();
            var i = 0;
            foreach (var item in gltfModel.gltf.LogicalImages)
            {
                var img = Bitmap.FromStream(item.OpenImageFile());
                img.Save("Texture" + i + ".png", ImageFormat.Png);
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

        public SharpGLTF.Schema2.Image ReadImage(string imagePath)
            {
            var image = texture.Texture.LogicalParent.CreateImage(name);
            image.SetSatelliteFile(newTexturePath);
            

        }

        public void SaveCurrentGltf(GltfModel gltfmodel, string saveAdres)
        {
            gltfmodel.gltf.SaveGLB(saveAdres);
        }

        public Materials newMaterialToMesh(Meshes meshes, string materialName)
        {
            Materials newmaterial = new Materials();
            newmaterial.Material = meshes.Mesh.LogicalParent.CreateMaterial(materialName);
            var index = newmaterial.Material.LogicalIndex;
            foreach (var item in newmaterial.Material.Channels)
            {
                Textures newMaterialTexture = new Textures();
                newMaterialTexture.Image = 
            }
            meshes.Mesh.Primitives[0].Material = newmaterial.Material;
            return newmaterial;
        }


    }
}
