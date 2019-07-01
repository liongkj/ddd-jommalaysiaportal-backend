using System.Reflection.Metadata.Ecma335;
using JomMalaysia.Core.Domain.ValueObjects;

namespace JomMalaysia.Core.Domain.Entities
{
    public class User
    {
        public User(string userId, string username, Email email, Name name)
        {
            this.UserId = userId;
            this.Username = username;
            this.Email = email;
            this.Name = name;

        }
        public string UserId { get; set; }
        public string Username { get; set; }
        public Email Email { get; set; }
        public Name Name { get; set; }


        public void AddListing()
        {

        }

        public void AddNewUser(User user)
        {

        }

        public void PublishListing(Listing l)
        {

        }

        public void UnpublishListing()
        {

        }

        public void ApproveWorkflow(Workflow w)
        {

        }

        public void RejectWorkflow(Workflow w)
        {

        }

        public void AddNewPackage()
        {

        }

        public void DeletePackage()
        {

        }
    }
}