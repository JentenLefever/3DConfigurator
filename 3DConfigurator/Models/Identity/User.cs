using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace _3DConfigurator.Models.Identity
{
    public class User: IdentityUser
    {
        public string WebsiteRank { get; set; }
        public string Userfolder { get; set; }

        
    }
}
