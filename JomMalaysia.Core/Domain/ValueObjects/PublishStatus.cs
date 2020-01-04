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
        ///Feature flag to direct publish listing
        ///Change here after worklfow feature is done
        private const bool WorkflowFeatureEnabled = false;
        public PublishStatus()
        {

            if (WorkflowFeatureEnabled) Status = ListingStatusEnum.Pending;
            else
            {
                Status = ListingStatusEnum.Published;
            }

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