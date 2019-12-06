using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace _3DConfigurator.ViewModel
{
    public class IndexViewModel
    {
        public string Name { get; set; }

        public SharpGLTF.Schema2.ModelRoot GltfFile { get; set; }
        public IEnumerable<SharpGLTF.Schema2.Scene> Scenes { get; set; }
        public IEnumerable<SharpGLTF.Schema2.Mesh> Meshes { get; set; }
        public IEnumerable<SharpGLTF.Schema2.Material> Materials { get; set; }
        public IEnumerable<SharpGLTF.Schema2.Animation> Animations { get; set; }

        public List<IFormFile> GltfUpload { get; set; }

        public SharpGLTF.Schema2.Material SelectedMaterialfull { get; set; }
        public long? SelectedMaterial { get; set; }

        public SharpGLTF.Schema2.Material[] AllMaterials { get; set; }
    }

}

