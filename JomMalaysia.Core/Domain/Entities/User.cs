using System;
using System.Reflection.Metadata.Ecma335;
using JomMalaysia.Core.Domain.Enums;
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

        public User(string userId)
        {
            UserId = userId;
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
        /// <summary>
        /// Create a new workflow object 
        /// </summary>
        /// <param name="listing"></param>
        /// <param name="details-optional"></param>
        /// <returns>Workflow object</returns>
        public Workflow PublishListing(Listing l, string details = null)
        {
            if (!l.isPublish.IsPublished)
            {
                Workflow PublishRequestInit = new Workflow
                {
                    Requester = this,
                    Created = DateTime.Now,
                    ListingId = l.ListingId,
                    Lvl = 0, //get role,
                    Type = WorkflowTypeEnum.Publish,
                    Status = WorkflowStatusEnum.Pending,
                    Details = details

                };
                return PublishRequestInit;
            }
            return null;
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