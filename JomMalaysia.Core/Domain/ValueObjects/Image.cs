using System;
using System.Collections.Generic;
using JomMalaysia.Core.Interfaces;
using JomMalaysia.Framework.Helper;

namespace JomMalaysia.Core.Domain.ValueObjects
{
    public class Image : ValueObjectBase
    {

        public string Url
        {
            get
           ;
            private set;


        }

        public Image(string url)
        {
            Url = url;
        }



        public Image()
        {
           

        }

        protected override IEnumerable<object> GetAtomicValues()
        {
            throw new System.NotImplementedException();
        }
    }
}