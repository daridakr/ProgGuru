using CloudinaryDotNet;
using CloudinaryDotNet.Actions;
using System;
using System.IO;
using System.Text.RegularExpressions;
using Volo.Abp.Domain.Services;

namespace Daridakr.ProgGuru.CloudStorage
{
    public class CloudinaryManager : DomainService
    {
        static Account account = new Account(
            CloudinaryAccount.Cloud,
            CloudinaryAccount.ApiKey,
            CloudinaryAccount.ApiSecret);

        Cloudinary cloudinary = new Cloudinary(account);

        public Uri UploadImage(string file)
        {
            var uploadParams = new ImageUploadParams()
            {
                File = new FileDescription(@file)
            };
            var uploadResult = cloudinary.Upload(uploadParams);
            return uploadResult.Url;
        }

        public void DeleteImageFromCloud(string url)
        {
            var match = Regex.Match(url, @"^(http|https):\/\/([\w\d-]+\.[\w\d]+).*\/([^\/]+)$");
            var publicId = match.Groups[3].ToString();
            cloudinary.Destroy(new DeletionParams(Path.GetFileNameWithoutExtension(publicId)));
        }
    }
}
