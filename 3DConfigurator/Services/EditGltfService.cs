using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using SharpGLTF.Schema2;

namespace _3DConfigurator.Services
{
    public class EditGltfService : IEditGltfService
    {
        public void AddAnimation(string nameanimation, ModelRoot currentgltfModel)
        {
            currentgltfModel.CreateAnimation(nameanimation);
        }

        public void AddMaterialToGltf(string nameMaterial, ModelRoot currentgltfModel)
        {
            currentgltfModel.CreateMaterial(nameMaterial);
        }

       
    }
}
