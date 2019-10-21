using System.Collections.Generic;

namespace JomMalaysia.Core.Domain.ValueObjects
{
    public class Image : ValueObjectBase
    {
        public string Url { get; }
        public string ThumbnailUrl { get; } = "https://res.cloudinary.com/jomn9-com/image/upload/v1571632729/category_thumbnail/proz1gzlepy9gp3rk0ej.jpg";


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