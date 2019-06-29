using System;
using System.Collections.Generic;
using System.Text;
using JomMalaysia.Core.Interfaces;

namespace JomMalaysia.Infrastructure.Data.MongoDb
{
    public class MongoSettings:IMongoSettings
    {
        public string ConnectionString { get; set; }
        public string DatabaseName { get; set; }
    }
}
