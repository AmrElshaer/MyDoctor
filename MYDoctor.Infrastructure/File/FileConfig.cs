using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using System;
using System.IO;
namespace MYDoctor.Infrastructure.File
{
    public class FileConfig : IFileConfig
    {
        private readonly IHostingEnvironment _ihostEnv;

        public FileConfig(IHostingEnvironment ihostEnv)
        {
            _ihostEnv = ihostEnv;
        }
        public string AddFile(IFormFile file,string filePath)
        {
            if (file != null)
            {

                string path = Path.Combine(_ihostEnv.WebRootPath, filePath);
                string uniquename = Guid.NewGuid() + "_" + file.FileName;
                string realpath = Path.Combine(path, uniquename);
                file.CopyTo(new FileStream(realpath, FileMode.Create));
                return uniquename;
            }
            throw new ArgumentNullException("File is Null");
        }

        public void DeleteFile(string fileName,string filePath)
        {
            var path = Path.Combine(_ihostEnv.WebRootPath, filePath, fileName);

            if (System.IO.File.Exists(path))
            {
                System.IO.File.Decrypt(path);
            }
        }
    }
}
