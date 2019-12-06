using _3DConfigurator.Models;
using System.IO;
using System.Linq;

namespace _3DConfigurator.Services
{
    public class GltfService : IGltfService
    {

        public Gltf currentGltfModel = new Gltf();

        public Gltf GltfInfo(string objectAdres, string saveadres)
        {

            var LoadedModel = SharpGLTF.Schema2.ModelRoot.Load(objectAdres);
            currentGltfModel.Name = Path.GetFileName(objectAdres);
            currentGltfModel.Scenes = LoadedModel.LogicalScenes.ToList();
            currentGltfModel.Materials = LoadedModel.LogicalMaterials.ToList();
            currentGltfModel.Meshes = LoadedModel.LogicalMeshes.ToList();
            LoadedModel.SaveGLB(saveadres);
            return currentGltfModel;
        }

        public Gltf GltfInfo(string objectAdres)
        {

            var LoadedModel = SharpGLTF.Schema2.ModelRoot.Load(objectAdres);
            currentGltfModel.Name = Path.GetFileName(objectAdres);
            currentGltfModel.Scenes = LoadedModel.LogicalScenes.ToList();
            currentGltfModel.Materials = LoadedModel.LogicalMaterials.ToList();
            currentGltfModel.Meshes = LoadedModel.LogicalMeshes.ToList();
            return currentGltfModel;
        }
    }
}
