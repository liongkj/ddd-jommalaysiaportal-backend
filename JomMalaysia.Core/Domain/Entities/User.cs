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

        public User()
        {

        }

        public User(string userId)
        {
            UserId = userId;
        }

        public string UserId { get; set; }
        public string Username { get; set; }
        public string Nickname { get; set; }
        //public string StringEmail { get; set; }
        //public string StringName { get; set; }
        public Email Email { get; set; }
        public Name Name { get; set; }
        public string Role { get; set; }
        public bool VerifyEmail { get; set; }
        public string Connection { get; set; }
        public string Password { get; set; }

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
            //check wheter listing is published
            //check listing is pending
            if (l.IsEligibleToPublish())
            {
                //if eligible to publish, return new Workflow
                return new Workflow(this, l, WorkflowTypeEnum.Publish);

            }
            return null;
        }

        public void UnpublishListing()
        {

        }

        public Workflow ApproveRejectWorkflow(Workflow w, string action, string comments)
        {
            if (!w.IsCompleted())
            {
                var ChildWorkflow = new Workflow(this, w, comments);


                if (UserHasAuthorityIn(ChildWorkflow))
                {
                    var status = ChildWorkflow.IsApprovedOrRejected(action);

                    w.UpdateWorkflowStatus(status);
                    w.HistoryData.Add(ChildWorkflow);
                    return w;
                }
            }
            else
            {
                return w;
            }
            return null;
        }


        private bool UserHasAuthorityIn(Workflow workflow)
        {
            return UserWorkflowLevel() >= workflow.Lvl;
        }
        private int UserWorkflowLevel()
        {
            var assignedRole = Role.ToLower();
            switch (assignedRole)
            {
                case "admin":
                    return 2;
                case "editor":
                    return 1;
                case "manager":
                    return 3;
                default:
                    return 0;
            }

        }

    }
}