using System.Collections.Generic;

namespace JomMalaysia.Core.Domain.ValueObjects
{
    public class Image : ValueObjectBase
    {
        public string Url { get; }
        public string ThumbnailUrl { get; }


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