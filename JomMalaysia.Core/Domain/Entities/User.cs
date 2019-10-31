using System;
using System.Collections.Generic;
using JomMalaysia.Core.Domain.Enums;
using JomMalaysia.Core.Domain.ValueObjects;
using JomMalaysia.Framework.Helper;
using JomMalaysia.Core.Domain.Entities.Listings;

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
            Email = email;
            Name = Name.For(name);
        }

        public string UserId { get; set; }
        public string Username { get; set; }
        public string Email { get; set; }
        public Name Name { get; set; }
        public UserRoleEnum Role { get; set; }
        public List<string> AdditionalPermissions { get; set; }
        public string PictureUri { get; set; }
        public DateTime? LastLogin { get; set; }
        public bool CanAssign { get; set; } = false;

        public bool CanDelete(User toBeDelete)
        {

            if (UserId == toBeDelete.UserId) return false; //cannot delete self
            if (toBeDelete.Role == UserRoleEnum.Superadmin) return false;
            if (!this.HasHigherRankThan(toBeDelete)) return false;
            return true;
        }

        public bool CanUpdateLiveListing()
        {
            bool can = false;
            if (Role.HasHigherAuthority(UserRoleEnum.Editor))
            {
                can = true;
            }
            return can;
        }

        public Tuple<List<string>, bool> AssignRole(string role)
        {
            List<string> roles = new List<string> { "editor", "admin", "manager", "superadmin" };
            var NewIndex = roles.IndexOf(role.ToLower());
            if (NewIndex == -1) return null;
            roles.RemoveRange(NewIndex + 1, roles.Count - NewIndex - 1);
            return Tuple.Create(roles, false);
        }

        public Tuple<List<string>, bool> UpdateRole(string role)
        {

            bool DeleteOperation = false;
            List<string> roles = new List<string> { "editor", "admin", "manager", "superadmin" };
            var NewIndex = roles.IndexOf(role.ToLower());
            if (Role != null)//user has role -> get list of to be delete roles
            {
                var OldIndex = roles.IndexOf(Role.Name.ToLower());
                if (OldIndex == NewIndex) return null;
                if (OldIndex > NewIndex)
                {//lower rank, delete{
                    roles.RemoveRange(0, NewIndex + 1);
                    DeleteOperation = true;
                }
            }
            else
            {//user has no role -> get list of to be added roles
                roles.RemoveRange(NewIndex + 1, roles.Count - NewIndex - 1);
                DeleteOperation = false;
            }
            if (NewIndex == -1) return null; //cant find role variable 

            return Tuple.Create(roles, DeleteOperation);
        }

        public PagingHelper<User> GetManageableUsers(PagingHelper<User> users)
        {
            List<User> LowerOrSameRankUsers = new List<User>();
            foreach (User u in users.Results)
            {
                if (UserId != u.UserId)
                {

                    if (u.Role != null)
                    {
                        if (HasHigherRankThan(u))
                        {
                            u.CanAssign = true;
                        }
                    }
                    else
                    {
                        u.CanAssign = true;
                    }
                    LowerOrSameRankUsers.Add(u);
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
        public Workflow PublishListing(Listing l, int months = 12)
        {
            //check wheter listing is published
            //check listing is pending
            if (l.IsEligibleToPublish())
            {
                //if eligible to publish, return new Workflow
                return new Workflow(this, l, WorkflowTypeEnum.Publish, months);
            }
            return null;
        }

        public Workflow UnpublishListing(Listing l, string reason = null)
        {
            if (l.IsEligibleToUnpublish())
            {
                return new Workflow(this, l, WorkflowTypeEnum.Unpublish, reason);
            }
            return null;
        }

        public bool ApproveRejectWorkflow(Workflow w, string action, string comments)
        {
            bool CanProceed = false;
            if (!w.IsCompleted())
            //TODO unit test
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

        }
        private int UserWorkflowLevel()
        {
            return Role.Id;

        }

    }
}