using System;
using System.Collections.Generic;
using JomMalaysia.Core.Domain.Enums;

namespace JomMalaysia.Core.Domain.ValueObjects
{
    public class PublishStatus : ValueObjectBase
    {
        public ListingStatusEnum Status { get; private set; }

        public DateTime? ValidityStart { get; private set; }
        public DateTime? ValidityEnd { get; private set; }

        public PublishStatus()
        {
            Status = ListingStatusEnum.Pending;

        }

        public void Publish(int months)
        {
            Status = ListingStatusEnum.Published;
            ValidityStart = DateTime.Now;
            ValidityEnd = ValidityStart.Value.AddMonths(months);
        }

        public void Extend(int months)
        {
            if (ValidityEnd.HasValue)
            {
                ValidityEnd.Value.AddMonths(months);
            }
            else
            {
                Publish(months);
            }

            //TODO unit test
        }

        public void Unpublish()
        {
            Status = ListingStatusEnum.Unpublished;
            ClearValidity();
        }

        private void ClearValidity()
        {
            ValidityStart = null;
            ValidityEnd = null;
        }
        protected override IEnumerable<object> GetAtomicValues()
        {
            throw new NotImplementedException();
        }
    }
}