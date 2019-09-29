using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MyDoctor.Models
{
    public class CutomPropertiy:IdentityUser
    {
        public string ImagePath { get; set; } = "Defulat.jpg";
    }
}
