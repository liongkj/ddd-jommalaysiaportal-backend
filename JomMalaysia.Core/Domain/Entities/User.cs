using System;
using System.Collections.Generic;
using System.Reflection.Metadata.Ecma335;
using JomMalaysia.Core.Domain.Enums;
using JomMalaysia.Core.Domain.ValueObjects;

namespace JomMalaysia.Core.Domain.Entities
{
    public class User
    {


        public User()
        {

        }

        public User(string username, string name, string email)
        {
            //For auth0 new users
            Username = username;
            Email = Email.For(email);
            Name = Name.For(name);
        }

        public string UserId { get; set; }
        public string Username { get; set; }
        public Email Email { get; set; }
        public Name Name { get; set; }
        public UserRoleEnum Role { get; set; }
        public List<string> AdditionalPermissions { get; set; }
        public string PictureUri { get; set; }
        public DateTime LastLogin { get; set; }

        public bool CanDelete(User toBeDelete)
        {

            if (UserId == toBeDelete.UserId) return false; //cannot delete self
            if (toBeDelete.Role == UserRoleEnum.Superadmin) return false;
            if (!this.HasHigherRankThan(toBeDelete)) return false;
            return true;
        }

        private bool HasHigherRankThan(User toBeDelete)
        {
            return Role.HasHigherAuthority(toBeDelete.Role);
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

        public Workflow UnpublishListing(Listing l, string details = null)
        {
            if (l.IsEligibleToUnpublish())
            {
                return new Workflow(this, l, WorkflowTypeEnum.Unpublish);
            }
            return null;
        }

        public bool ApproveRejectWorkflow(Workflow w, string action, string comments)
        {
            bool CanProceed = false;
            if (!w.IsCompleted())
            {
                var ChildWorkflow = new Workflow(this, w, comments);

                if (!UserHasAuthorityIn(ChildWorkflow)) //not enough authority
                {
                    w.Responder = this;
                }
                else
                {
                    var status = ChildWorkflow.ApproveRejectOperation(action);

                    w.UpdateWorkflowStatus(status);
                    w.HistoryData.Add(ChildWorkflow);
                    CanProceed = true;
                }
            }

            return CanProceed;
        }


        private bool UserHasAuthorityIn(Workflow workflow)
        {
            return UserWorkflowLevel() >= workflow.Lvl;
            //TODO Auth0 role
        }
        private int UserWorkflowLevel()
        {
            return Role.Id;

        }

    }
}