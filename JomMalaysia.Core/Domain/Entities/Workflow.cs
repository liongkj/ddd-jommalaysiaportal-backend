using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;
using JomMalaysia.Core.Domain.ValueObjects;

namespace JomMalaysia.Core.Domain.Entities
{
    public class Workflow
    {
        public string WorkflowId { get; set; }
        public string Type { get; set; }
        public int Lvl { get; set; }

        public string ListingId { get; set; }
        public User Requester { get; set; }
        public User Responder { get; set; }
        public WorkflowStatusEnum Status { get; set; }
        public DateTime Created { get; set; }
        public string Details { get; set; }
        public ICollection<Workflow> PreviousWorkflows { get; private set; }

        public Workflow()
        {
            PreviousWorkflows = new Collection<Workflow>();
        }


        public List<Workflow> CreateNewWorkflowRequest(List<string> listingIds, User requester)
        {
            List<Workflow> Workflows = new List<Workflow>();
            foreach (var listing in listingIds)
            {
                if(listing!=null)
                Workflows.Add(NewWorkflowInit(requester, listing));
            };

            return Workflows;
        }

        public void Approve()
        {
            Status = WorkflowStatusEnum.Level1;
        }

        public void Reject()
        {

        }

        #region helper
        private Workflow NewWorkflowInit(User requester, string listing)
        {
            Workflow NewWorkflow = new Workflow
            {
                ListingId = listing,
                Requester = requester,
                Status = WorkflowStatusEnum.Pending,
                Lvl = 0,
                Type = "New Publish Request",
                Created = DateTime.Now
            };
            return NewWorkflow;
        }
    }
    #endregion
}

