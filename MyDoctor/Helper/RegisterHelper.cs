using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace MyDoctor.Helper
{
    public static class RegisterHelper
    {
        public static string ConfigImagePath(IFormFile imagepath, IHostingEnvironment _IhostEnv)
        {
            if (imagepath != null)
            {

                string path = Path.Combine(_IhostEnv.WebRootPath, "images");
                string uniquename = Guid.NewGuid() + "_" + imagepath.FileName;
                string realpath = Path.Combine(path, uniquename);
                imagepath.CopyTo(new FileStream(realpath, FileMode.Create));
                return uniquename;
            }
            return null;
                
        }
    }
}
