using _3DConfigurator.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace _3DConfigurator.Services
{
    public interface IEditGltfService
    {

        public void AddMaterialToGltf(string nameMaterial, SharpGLTF.Schema2.ModelRoot currentgltfModel);
      
        public void AddAnimation(string nameanimation, SharpGLTF.Schema2.ModelRoot currentgltfModel);

    }
}
