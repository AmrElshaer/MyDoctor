using Microsoft.AspNetCore.Http;
namespace MYDoctor.Infrastructure.File
{
    public  interface IFileConfig
    {
        string  AddFile(IFormFile file, string filePath);
        void DeleteFile(string fileName, string filePath);
    }
}
