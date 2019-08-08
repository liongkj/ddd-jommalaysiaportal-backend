using System;
using JomMalaysia.Core.Domain.Entities;
using JomMalaysia.Core.Domain.Enums;
using JomMalaysia.Core.Domain.Factories;
using JomMalaysia.Core.Domain.ValueObjects;
using Xunit;

namespace JomMalaysia.Test.Core

{
    public class FactoryTest
    {
        [Fact]
        public void CreateEventListingTest()
        {
            //Given
            var eventName = "Music Festival 2019";
            var eventDescription = "the best music festival in malaysia";
            var eventType = ListingTypeEnum.Event;
            Category cat = new Category("Music","muzik","音乐", (CategoryPath)(",Festival,")) ;
            
            Location loc = new Location();
            //When
            var listingobj = ListingFactory.Create(eventName, eventDescription, cat, loc, eventType);
            EventListing listing = (EventListing)listingobj;

            listing.updateEventDate(DateTime.Now);
            //Then
            Assert.IsType<EventListing>(listing);
            Assert.True(listing.EventDateTime.Year == 2019);
        }

        [Fact]
        public void CreateGovernmentListingTest()
        {
            //Given

           // var listingobj = ListingFactory.Create(ListingTypeEnum.Government.Id);
            //When
            //GovernmentListing listing = (GovernmentListing)listingobj;
            //Then
            //Assert.IsType<EventListing>(listing);
        }

        [Fact]
        public void CreatePrivateListingTest()
        {
            //Given

           // var listingobj = ListingFactory.Create(ListingTypeEnum.Private.Id);
            //When
            //PrivateListing listing = (PrivateListing)listingobj;
            //Then
            //Assert.IsType<EventListing>(listing);
        }
    }
}