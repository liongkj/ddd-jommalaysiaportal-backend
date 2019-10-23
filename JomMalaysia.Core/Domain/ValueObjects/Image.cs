using System.Collections.Generic;

namespace JomMalaysia.Core.Domain.ValueObjects
{
    public class Image : ValueObjectBase
    {
        public string Url { get; }
        public string ThumbnailUrl { get; }

        public Image(string url, string thumbnailurl)
        {
            Url = url;
            ThumbnailUrl = thumbnailurl;
        }

        public Image()
        {
            Url = Constants.DefaultImages.Url;
            ThumbnailUrl = Constants.DefaultImages.ThumbnailUrl;
        }

        protected override IEnumerable<object> GetAtomicValues()
        {
            throw new System.NotImplementedException();
        }
    }
}