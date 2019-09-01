using System;
using System.Collections.Generic;

namespace JomMalaysia.Core.Domain.ValueObjects
{
    public class PublishStatus : ValueObjectBase
    {

        //TODO published workflow
        public bool IsPublished { get; set; }
        public DateTime ValidityStart { get; set; }
        public DateTime ValidityEnd { get; set; }

        public static explicit operator PublishStatus(int months)
        {
            return For(months);
        }

        public static PublishStatus For(int months)
        {
            throw new NotImplementedException();
        }

        private PublishStatus()
        {

        }



        public void Extend()
        {

        }

        public void Unpublish()
        {

        }

        protected override IEnumerable<object> GetAtomicValues()
        {
            throw new NotImplementedException();
        }
    }
}