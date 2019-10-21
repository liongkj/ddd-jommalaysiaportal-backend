using System.Collections.Generic;

namespace JomMalaysia.Core.Domain.ValueObjects
{
    public class Image : ValueObjectBase
    {
        public string Url { get; }
        public string ThumbnailUrl { get; } = "https://cloudinary.com/documentation/upload_widget#upload_widget_options";


        Image(string url, string thumbnailurl)
        {
            Url = url;
            ThumbnailUrl = thumbnailurl;
        }

        protected override IEnumerable<object> GetAtomicValues()
        {
            throw new System.NotImplementedException();
        }
    }
}