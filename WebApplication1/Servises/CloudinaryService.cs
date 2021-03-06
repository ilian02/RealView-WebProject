using CloudinaryDotNet;
using CloudinaryDotNet.Actions;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace WebSite.Servises
{
    public class CloudinaryService : ICloudinaryService
    {
        private readonly Cloudinary cloudinary;

        public CloudinaryService(string cloudName, string apiKey, string apiSecret)
        {
            this.cloudinary = new Cloudinary(new Account(cloudName, apiKey, apiSecret));
        }

        public async Task<string> UploadImage(IFormFile formFile)
        {
            string formFileId = Guid.NewGuid().ToString();

            if (formFile.Length > 0)
            {
                using (var ms = new MemoryStream())
                {
                    formFile.CopyTo(ms);
                    byte[] formBytes = ms.ToArray();

                    ImageUploadParams imageUploadParams = new ImageUploadParams
                    {
                        File = new FileDescription(formFileId, new MemoryStream(formBytes)),
                        PublicId = formFileId
                    };

                    ImageUploadResult uploadResult = await this.cloudinary.UploadAsync(imageUploadParams);
                    return uploadResult.Url.AbsoluteUri;
                }
            }

            return null;
        }
    }
}
