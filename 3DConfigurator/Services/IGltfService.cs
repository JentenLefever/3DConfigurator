using _3DConfigurator.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace _3DConfigurator.Services
{
   public  interface IGltfService
    {
        public Gltf GltfInfo(string objectAdres, string saveadres);
       
    }
}
