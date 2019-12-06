using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using _3DConfigurator.Models;
using _3DConfigurator.Services;
using _3DConfigurator.ViewModel;
using System.IO;
using Microsoft.AspNetCore.Hosting;

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
        
        public IActionResult Index()
        {
            GltfService gltfService = new GltfService();
            Gltf gltf = new Gltf();
            var LoadPath = Path.Combine(_env.WebRootPath, "Objects", "Current.glb");
            gltf = gltfService.GltfInfo(LoadPath);

            IndexViewModel indexViewModel = new IndexViewModel()
            {
                Materials = gltf.Materials,
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

        string fileName;
        // POST: Admin/Projects/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Index(IndexViewModel Uploadgltf)
        {
            if (Uploadgltf.SelectedMaterial == null)
            {
                //Toevoegen van Object in de wwwroot.

                foreach (var item in Uploadgltf.GltfUpload)
                {
                    fileName = Path.GetFileName(item.FileName);
                    var savepath = Path.Combine(_env.WebRootPath, "Objects", fileName);

                    using (var stream = new FileStream(savepath, FileMode.Create))
                    {
                        await item.CopyToAsync(stream);
                    }

                }

                GltfService gltfService = new GltfService();
                Gltf gltf = new Gltf();
                var LoadPath = Path.Combine(_env.WebRootPath, "Objects", "Current.glb");
                gltf = gltfService.GltfInfo("wwwroot/Objects/" + fileName, LoadPath);
                IndexViewModel indexViewModel = new IndexViewModel()
                {
                    GltfFile = SharpGLTF.Schema2.ModelRoot.Load(LoadPath),
                    Name = fileName,
                    Animations = gltf.Animations,
                    Materials = gltf.Materials,
                    Meshes = gltf.Meshes,
                    Scenes = gltf.Scenes

                };


                return View(indexViewModel);
            }

            else
            {
                GltfService gltfService = new GltfService();
                Gltf gltf = new Gltf();
                var LoadPath = Path.Combine(_env.WebRootPath, "Objects", "Current.glb");
                gltf = gltfService.GltfInfo(LoadPath);
                SharpGLTF.Schema2.Material selectedMaterial = gltf.Materials.ToList()[Convert.ToInt32(Uploadgltf.SelectedMaterial)];

                IndexViewModel indexViewModel = new IndexViewModel()
                {
                    GltfFile = SharpGLTF.Schema2.ModelRoot.Load(LoadPath),
                    Name = fileName,
                    Animations = gltf.Animations,
                    Materials = gltf.Materials,
                    Meshes = gltf.Meshes,
                    Scenes = gltf.Scenes,
                    SelectedMaterialfull = selectedMaterial
                };

                LoadPath = Path.Combine(_env.WebRootPath, "Objects", "Cube.glb");
                var previewcube = SharpGLTF.Schema2.ModelRoot.Load(LoadPath);
                



                return View(indexViewModel);


            }
            

           
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> ChangeMaterial(IndexViewModel changedMaterial)
        {
            GltfService gltfService = new GltfService();
            Gltf gltf = new Gltf();
            var LoadPath = Path.Combine(_env.WebRootPath, "Objects", "Current.glb");
            gltf = gltfService.GltfInfo(LoadPath);
            SharpGLTF.Schema2.Material selectedMaterial = gltf.Materials.ToList()[Convert.ToInt32(changedMaterial.SelectedMaterial)];

            IndexViewModel indexViewModel = new IndexViewModel()
            {
                GltfFile = SharpGLTF.Schema2.ModelRoot.Load(LoadPath),
                Name = fileName,
                Animations = gltf.Animations,
                Materials = gltf.Materials,
                Meshes = gltf.Meshes,
                Scenes = gltf.Scenes,
                SelectedMaterialfull = selectedMaterial
            };


            return View("Index",indexViewModel);
        }
    }
}
