using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;
using JomMalaysia.Core.Domain.Enums;
using JomMalaysia.Core.Domain.ValueObjects;
using JomMalaysia.Core.UseCases.ListingUseCase.Publish;

namespace JomMalaysia.Core.Domain.Entities
{
    public class Workflow
    {
        public string WorkflowId { get; set; }
        public WorkflowTypeEnum Type { get; set; }
        public int Lvl { get; set; }

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
        public Workflow(User user, Listing listing, WorkflowTypeEnum type) //create new workflow
        {
            Requester = user;
            Listing = listing;
            Created = DateTime.Now;
            Type = type;
            Status = WorkflowStatusEnum.Pending;

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
                return WorkflowStatusEnum.For(Lvl);
            }
            else
            {
                Lvl++;
                return WorkflowHasCompleted();
            }
        }



        private WorkflowStatusEnum WorkflowIsRejected()
        {
            return WorkflowStatusEnum.Rejected;
        }
        private WorkflowStatusEnum WorkflowHasCompleted()
        {
            return WorkflowStatusEnum.Completed;
            //TODO initiate change update listing
        }

        public bool IsCompleted()
        {
            if (Status != null)
                return Status == WorkflowStatusEnum.Completed || Status == WorkflowStatusEnum.Rejected;
            return Lvl == WorkflowStatusEnum.Completed.Id || Lvl == WorkflowStatusEnum.Rejected.Id;
        }


    }

}
