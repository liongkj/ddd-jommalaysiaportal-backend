using JomMalaysia.Core.Domain.ValueObjects;
using Xunit;

namespace Core.Test.ValueObject
{
    public class ImageTest
    {
        [Fact]
        public void GetOptimizeUrl()
        {
            string url = "https://res.cloudinary.com/jomn9-com/image/upload/v1575877111/listing_images/bj0bg35wotngazxfb7pq.webp";
            Image ogImage = new Image(url);
            var optimzedUrl = ogImage.Url;
            Assert.NotSame(url, optimzedUrl);
        }
    }
}