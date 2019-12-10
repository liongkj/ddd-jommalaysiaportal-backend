using System.Collections.Generic;
using JomMalaysia.Core.Domain.ValueObjects;

namespace JomMalaysia.Core.UseCases.ListingUseCase.Shared
{
    public class ListingImagesRequest
    {

        public ImageRequest ListingLogo { get; set; }

        public ImageRequest CoverPhoto { get; set; }

        public List<ImageRequest> Ads { get; }
    }
}