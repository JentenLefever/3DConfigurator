using _3DConfigurator.Models;
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

        public GltfModel GltfModel { get; set; }
        public IFormFile GltfUpload { get; set; }
        public int  SelectedMeshIndex { get; set; }

        public Meshes SelectedMesh { get; set; }
        public int SelectedMaterialIndex { get; set; }
        
        public string status { get; set; }

        public SharpGLTF.Schema2.Material SelectedMaterial { get; set; }
        public SharpGLTF.Schema2.Texture SelectedTexture { get; set; }
        public int SelectedChannelIndex { get; set; }

        public IFormFile NewtextureUpload { get; set; }

    }

}

