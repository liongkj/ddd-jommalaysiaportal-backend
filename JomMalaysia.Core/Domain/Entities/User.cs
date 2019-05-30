

namespace JomMalaysia.Core.Entities
{
    public class User
    {
        public string Id { get; }
        public string Username { get; }

        internal User(string firstName, string lastName, string email, string username, string id = null, string passwordHash = null)
        {
            Id = id;
            Username = username;
        }

    }
}
