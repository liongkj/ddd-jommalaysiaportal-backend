using JomMalaysia.Core.Domain.ValueObjects;

namespace JomMalaysia.Infrastructure.Data.MongoDb.Entities
{
    public class ContactsDto
    {
        public string Name { get; set; }
        public string Email { get; set; }
        public Phone Phone { get; set; }
        public bool IsPrimary { get; set; }
    }
}