using CloudinaryDotNet;
using CloudinaryDotNet.Actions;
using DataAccess.Context;
using Domin.Repository;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;

namespace DataAccess.Implementation
{
    public class CloudinaryServices : ICloudinaryServices
    {
        private readonly Cloudinary _cloudinary;
        private readonly AppDbContext _context;
        private IConfiguration _config;
        public CloudinaryServices(AppDbContext context, Cloudinary cloudinary, IConfiguration config)
        {
            _cloudinary = cloudinary;
            _context = context;
            _config = config;
        }


        public string SaveImage(IFormFileCollection file)
        {

            using (var stream = file[0].OpenReadStream())
            {

                var uploadParams = new ImageUploadParams
                {
                    File = new FileDescription(file[0].FileName, stream),
                    Transformation = new Transformation().Width(1000).Height(1000),
                    Folder = _config.GetSection("CloudFolderName").Value,
                };

                var uploadResult = _cloudinary.Upload(uploadParams);

                if (uploadResult.Error != null)
                {
                    return uploadResult.Error.Message;
                }
                string imageUrl = uploadResult.SecureUrl.ToString();
                return imageUrl;

            }
        }

        public List<string> SaveImages(IFormFileCollection file)
        {
            List<string> images = new List<string>();
            for (int i = 0; i < file.Count(); i++)
            {
                using (var stream = file[i].OpenReadStream())
                {
                    var uploadParams = new ImageUploadParams
                    {
                        File = new FileDescription(file[i].FileName, stream),
                        Transformation = new Transformation().Width(1000).Height(1000),
                        Folder = _config.GetSection("CloudFolderName").Value,
                    };


                    var uploadResult = _cloudinary.Upload(uploadParams);
                    if (uploadResult.Error != null)
                    {
                        return new List<string> { uploadResult.Error.Message };
                    }
                    string imageUrl = uploadResult.SecureUrl.ToString();
                    images.Add(imageUrl);
                }
            }
            return images;

        }
    }
}

