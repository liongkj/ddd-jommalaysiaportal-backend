using System;
using System.Collections.Generic;
using System.Reflection.Metadata.Ecma335;
using JomMalaysia.Core.Domain.Enums;
using JomMalaysia.Core.Domain.ValueObjects;
using JomMalaysia.Framework.Helper;

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

        public Tuple<List<string>, bool> UpdateRole(string role)
        {

            bool DeleteOperation;
            List<string> roles = new List<string> { "editor", "admin", "manager", "superadmin" };
            var OldIndex = roles.IndexOf(Role.Name.ToLower());

            var NewIndex = roles.IndexOf(role.ToLower());
            if (NewIndex == -1) return null;
            if (OldIndex == NewIndex) return null;
            if (OldIndex > NewIndex)
            {//lower rank, delete{
                roles.RemoveRange(0, NewIndex);
                DeleteOperation = true;
            }
            else
            {
                roles.RemoveRange(NewIndex + 1, roles.Count - NewIndex - 1);
                DeleteOperation = false;
            }//TODO do unit test
            return Tuple.Create(roles, DeleteOperation);
        }

        public PagingHelper<User> GetManageableUsers(PagingHelper<User> users)
        {
            List<User> LowerOrSameRankUsers = new List<User>();
            foreach (User u in users.Results)
            {
                if (UserId != u.UserId)
                {
                    if (u.Role == null || HasHigherRankThan(u) || HasEqualRankTo(u))
                    {
                        LowerOrSameRankUsers.Add(u);
                    }
                }
            }
            users.Results = LowerOrSameRankUsers;
            users.TotalRowCount = LowerOrSameRankUsers.Count;
            return users;
        }

        private bool HasHigherRankThan(User user2)
        {
            return Role.HasHigherAuthority(user2.Role);
        }

        private bool HasEqualRankTo(User user2)
        {
            return Role.Equals(user2.Role);
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