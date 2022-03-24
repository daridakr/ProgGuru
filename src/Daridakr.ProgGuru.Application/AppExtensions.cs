using Microsoft.AspNetCore.Http;
using System.IO;
using System.Threading.Tasks;

namespace Daridakr.ProgGuru
{
    public class AppExtensions
    {
        public static async Task<string> GetFilePathAsync(IFormFile file)
        {
            var filePath = Path.GetTempFileName();
            using (var stream = File.Create(filePath))
            {
                await file.CopyToAsync(stream);
            }
            return filePath;
        }
    }
}
