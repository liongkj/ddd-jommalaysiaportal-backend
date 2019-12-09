using CloudinaryDotNet;
using CloudinaryDotNet.Actions;

namespace JomMalaysia.Framework.Helper
{
    public static class ImageHelper
    {
        public static string GetOptimizedUrl(string url)
        {
            CloudinaryDotNet.Account account = new CloudinaryDotNet.Account("jomn9-com", "731121621859821", "NwqqykYyd4ix_KEoZBobAnpqt-I");
            CloudinaryDotNet.Cloudinary cloudinary = new CloudinaryDotNet.Cloudinary(account);
            return cloudinary.Api.UrlImgUp.Transform(new Transformation().FetchFormat("auto").Quality("auto")).BuildUrl(url);
        }
    }
}