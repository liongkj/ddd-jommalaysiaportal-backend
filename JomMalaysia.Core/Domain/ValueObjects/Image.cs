using System;
using System.Collections.Generic;
using JomMalaysia.Core.Interfaces;
using JomMalaysia.Framework.Helper;

namespace JomMalaysia.Core.Domain.ValueObjects
{
    public class Image : ValueObjectBase
    {
        private string _Url;
        public string Url
        {
            get
            {
                return ImageHelper.GetOptimizedUrl(_Url);
            }
            private set
            {

            }

        }

        public Image(string url)
        {
            _Url = url;
        }


        public Image()
        {
            Url = Constants.DefaultImages.Url;

        }

        protected override IEnumerable<object> GetAtomicValues()
        {
            throw new System.NotImplementedException();
        }
    }
}