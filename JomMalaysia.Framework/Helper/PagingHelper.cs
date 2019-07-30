using System;
using System.Collections.Generic;
using System.Text;

namespace JomMalaysia.Framework.Helper
{
    public class PagingHelper<T>
    {
        public int CurrentPage { get; set; }

        private int _pageCount { get; set; }
        public int PageCount
        {
            get { return _pageCount; }
            set { _pageCount = value < 1 ? 1 : value; }
        }

        public int PageSize { get; set; }

        public int TotalRowCount { get; set; }

        public List<T> Results { get; set; }
    }
}
