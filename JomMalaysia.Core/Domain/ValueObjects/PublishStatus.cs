using System;
using System.Collections.Generic;

namespace JomMalaysia.Core.Domain.ValueObjects
{
    public class PublishStatus : ValueObjectBase
    {
        public bool IsPublished { get; set; }
        public DateTime? ValidityStart { get; set; }
        public DateTime? ValidityEnd { get; set; }


        public void Publish(int months)
        {
            ValidityStart = DateTime.Now;
            ValidityEnd = ValidityStart.Value.AddMonths(months);
            IsPublished = true;
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
            IsPublished = false;
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