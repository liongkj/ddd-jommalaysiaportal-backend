using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using JomMalaysia.Presentation.Models;

namespace JomMalaysia.Presentation.Gateways.Category
{
    public interface ICategoryGateway
    {
        Task<List<CategoryViewModel>> GetCategories();
    }
}
