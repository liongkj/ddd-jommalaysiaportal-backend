using System;
using System.Collections.Generic;
using System.Text;

namespace JomMalaysia.Framework.Constant
{
    public partial class APIConstant
    {
        public class API
        {
            public class Path
            {
                public const string GetAllCategory = "api/Categories";

                public const string GetSubcategory = "api/Categories/{catName}";
            }
        }
    }
}
