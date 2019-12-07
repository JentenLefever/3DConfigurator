using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using _3DConfigurator.Models;
using _3DConfigurator.Services;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using static System.Net.Mime.MediaTypeNames;

namespace _3DConfigurator.Controllers
{
    public class ConfiguratorController : Controller
    {

        private readonly IHostingEnvironment _env;

        public ConfiguratorController(IHostingEnvironment env)
        {
            _env = env;

        }
        // GET: Configurator
        public ActionResult Index()
        {
            //service toevoegen
            EditGltfService editGltfService = new EditGltfService();

            //Load Model
            var loadCurrentObjectPath = Path.Combine(_env.WebRootPath, "Objects", "Current.glb");
            var currentObject = editGltfService.PopulateGltfModel(loadCurrentObjectPath, loadCurrentObjectPath);

            //Load Textures
            editGltfService.CreateTextures(currentObject);
            
            


            return View();
        }

        // GET: Configurator/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: Configurator/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Configurator/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(IFormCollection collection)
        {
            try
            {
                // TODO: Add insert logic here

                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: Configurator/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: Configurator/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, IFormCollection collection)
        {
            try
            {
                // TODO: Add update logic here

                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: Configurator/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: Configurator/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int id, IFormCollection collection)
        {
            try
            {
                // TODO: Add delete logic here

                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }
    }
}