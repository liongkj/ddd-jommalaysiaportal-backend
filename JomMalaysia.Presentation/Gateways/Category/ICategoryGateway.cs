using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace JomMalaysia.Presentation.Gateways.Category
{
    public interface ICategoryGateway
    {
        List<CategoryViewModel> GetCategories();
    }
}
