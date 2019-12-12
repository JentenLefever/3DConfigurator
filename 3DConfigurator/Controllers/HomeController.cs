using _3DConfigurator.Models;
using _3DConfigurator.Services;
using _3DConfigurator.ViewModel;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace _3DConfigurator.Controllers
{
    public class HomeController : Controller
    {
        private readonly IHostingEnvironment _env;
        private readonly ILogger<HomeController> _logger;

        IEnumerable<SharpGLTF.Schema2.Material> materialsObject;

        public HomeController(ILogger<HomeController> logger, IHostingEnvironment env)
        {
            _env = env;
            _logger = logger;
        }

        //Get Index Page
        public IActionResult Index()
        {
            EditGltfService gltfService = new EditGltfService(_env);
            GltfModel gltfModel = new GltfModel();

            //model populaten met ingeladen gltf
            var Currentmodel = gltfService.PopulateGltfModel(Path.Combine(_env.WebRootPath, "Objects", "Current.glb"));

            IndexViewModel indexViewModel = new IndexViewModel()
            {
                GltfModel = Currentmodel
            };


            return View(indexViewModel);
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Index(IndexViewModel indexPostVM)
        {
            EditGltfService editGltfService = new EditGltfService(_env);
            IndexViewModel indexViewModel = new IndexViewModel();

            //uploaden van nieuwe gltf
            if (indexPostVM.GltfUpload != null)
            {
                editGltfService.UploadGLTFModel(indexPostVM.GltfUpload);
                var currentModel = editGltfService.PopulateGltfModel(Path.Combine(_env.WebRootPath, "Objects", "Current.glb"));
                indexViewModel.GltfModel = currentModel;
                return View(indexViewModel);
            }

            var Currentmodel = editGltfService.PopulateGltfModel(Path.Combine(_env.WebRootPath, "Objects", "Current.glb"));

            //Show Materials in Mesh
            if (indexPostVM.SelectedMeshIndex != null && indexPostVM.SelectedMaterialIndex == -1)
            {
                indexViewModel.SelectedMesh = Currentmodel.MeshesVerzameling.ToList()[indexPostVM.SelectedMeshIndex];
                indexViewModel.GltfModel = Currentmodel;
                indexViewModel.SelectedMeshIndex = indexPostVM.SelectedMeshIndex;
                return View(indexViewModel);
            }

            //Show Channels in Material
            if (indexPostVM.SelectedMeshIndex != null && indexPostVM.SelectedMaterialIndex != null && indexPostVM.SelectedChannelIndex == -1)
            {
                indexViewModel.SelectedMaterial = Currentmodel.gltf.LogicalMaterials.ToList()[indexPostVM.SelectedMeshIndex];
                indexViewModel.GltfModel = Currentmodel;
                indexViewModel.SelectedMaterialIndex = indexPostVM.SelectedMaterialIndex;
                return View(indexViewModel);
            }

            //Show/edit Textures
            if (indexPostVM.SelectedChannelIndex != -1)
            {
                List<SharpGLTF.Schema2.MaterialChannel> channellist = new List<SharpGLTF.Schema2.MaterialChannel>();
                foreach (var item in Currentmodel.gltf.LogicalMaterials.ToList()[indexPostVM.SelectedMeshIndex].Channels)
                {
                    channellist.Add(item);
                };
                indexViewModel.SelectedTexture = channellist[Convert.ToInt32(indexPostVM.SelectedChannelIndex)].Texture;
                editGltfService.LoadImageFromTexture(indexViewModel.SelectedTexture);
                indexViewModel.GltfModel = Currentmodel;
                indexViewModel.SelectedMaterialIndex = indexPostVM.SelectedMaterialIndex;
                indexViewModel.SelectedMeshIndex = indexPostVM.SelectedMeshIndex;
                if (indexPostVM.NewtextureUpload != null)
                {
                    editGltfService.AddUploadedImageToSelectedTexture(indexViewModel.SelectedTexture, indexPostVM.NewtextureUpload);
                    return View(indexViewModel);
                }
            }

            return View(indexViewModel);
        }

    }
}
