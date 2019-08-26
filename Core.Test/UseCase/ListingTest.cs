using System;
using System.Collections.Generic;
using System.Text;
using FluentValidation;
using JomMalaysia.Core.Domain.Entities;
using JomMalaysia.Core.Domain.Enums;
using JomMalaysia.Core.Domain.ValueObjects;
using JomMalaysia.Core.UseCases.ListingUseCase.Create;
using Xunit;

namespace JomMalaysia.Test.Core.UseCase
{
    public class ListingTest
    {
        [Fact]
        public void CreateListingTest()
        {
            var location = new Location();
            CreateListingRequest req = new CreateListingRequest("", "", "", new Category("haha", "", ""), location, ListingTypeEnum.Event);

        }
    }
}
