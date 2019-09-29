using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace MyDoctor.Controllers
{
    public class ImageController : Controller
    {

     
        private readonly IHostingEnvironment _hostingEnvironment;
        public ImageController(IHostingEnvironment hostingEnvironment)
        {
            this._hostingEnvironment = hostingEnvironment;
           
        }
        [HttpGet]
        public string index(string name)
        {
            return name;
        }
        [HttpGet]
        public async Task  UploadFiles(IFormFile file)
        {
            var filename = Path.Combine(_hostingEnvironment.WebRootPath, "uploads");
            var fullpath = Path.Combine(filename,file.Name);
            using (var createimage=new FileStream(fullpath,FileMode.Create))
            {
               await file.CopyToAsync(createimage);
            }
           
        }
    }
}