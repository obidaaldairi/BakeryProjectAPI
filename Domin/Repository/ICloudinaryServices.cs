using Microsoft.AspNetCore.Http;

namespace Domin.Repository
{
    public interface ICloudinaryServices
    {
        string SaveImage(IFormFileCollection file);
        List<string> SaveImages(IFormFileCollection file);
    }
}
