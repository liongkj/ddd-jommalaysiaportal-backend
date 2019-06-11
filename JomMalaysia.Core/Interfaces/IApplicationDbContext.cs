using System;
using System.Collections.Generic;
using System.Text;

namespace JomMalaysia.Core.Interfaces
{
    public interface IApplicationDbContext
    {
        string CollectionName { get; set; }
        string ConnectionString { get; set; }
        string DatabaseName { get; set; }
    }
}
