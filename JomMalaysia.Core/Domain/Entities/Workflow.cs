using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using JomMalaysia.Core.Domain.Entities.Listings;
using JomMalaysia.Core.Domain.Enums;

namespace JomMalaysia.Core.Domain.Entities
{
    public class Workflow
    {
        public string WorkflowId { get; set; }
        public WorkflowTypeEnum Type { get; set; }
        public int Lvl { get; set; }
        public int Months { get; set; }
        public Listing Listing { get; set; }
        public User Requester { get; set; }
        public User Responder { get; set; }
        public WorkflowStatusEnum Status { get; set; }
        public DateTime Created { get; set; }
        public string Comments { get; set; }
        public ICollection<Workflow> HistoryData { get; private set; }

        public Workflow()
        {

        }
        public Workflow(User user, Listing listing, WorkflowTypeEnum type, int months) //create new workflow
        {
            Requester = user;
            Listing = listing;
            Created = DateTime.Now;
            Type = type;
            Status = WorkflowStatusEnum.Pending;
            Type = WorkflowTypeEnum.Publish;
            Months = months;
            HistoryData = new Collection<Workflow>();
        }

        public Workflow(User user, Listing listing, WorkflowTypeEnum type, string reason) //create new workflow
        {
            Requester = user;
            Listing = listing;
            Created = DateTime.Now;
            Type = type;
            Status = WorkflowStatusEnum.Pending;
            Type = WorkflowTypeEnum.Unpublish;
            Comments = reason;
            HistoryData = new Collection<Workflow>();
        }
        public Workflow(User responder, Workflow parentWorkflow, string comments)
        {
            Comments = comments;
            Created = DateTime.Now;
            Lvl = parentWorkflow.Status.Id;
            Responder = responder;

        }

        public void UpdateWorkflowStatus(WorkflowStatusEnum action)
        {
            Status = action;
        }


        public WorkflowStatusEnum ApproveRejectOperation(string action)
        {

            if (action.ToLower().Equals("approve"))
            {
                return WorkflowIsApproved();
            }
            else
            {
                return WorkflowIsRejected();
            }


        }


        private WorkflowStatusEnum WorkflowIsApproved()
        {
            if (!IsCompleted())
            {
                Lvl++;
                Status = WorkflowStatusEnum.For(Lvl);
                return Status;
            }
            else
            {
                Lvl++;
                return WorkflowHasCompleted();
            }
        }



        private WorkflowStatusEnum WorkflowIsRejected()
        {
            Status = WorkflowStatusEnum.Rejected;
            return Status;
        }
        private WorkflowStatusEnum WorkflowHasCompleted()
        {

            Status = WorkflowStatusEnum.Completed;
            return Status;
        }

        public bool IsCompleted()
        {
            if (Status != null)
                return Status == WorkflowStatusEnum.Completed || Status == WorkflowStatusEnum.Rejected;
            return Lvl == WorkflowStatusEnum.Completed.Id || Lvl == WorkflowStatusEnum.Rejected.Id;
        }


    }

}
