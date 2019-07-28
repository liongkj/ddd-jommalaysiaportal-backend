using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;
using JomMalaysia.Core.Domain.Enums;
using JomMalaysia.Core.Domain.ValueObjects;

namespace JomMalaysia.Core.Domain.Entities
{
    public class Workflow
    {
        public string WorkflowId { get; set; }
        public WorkflowTypeEnum Type { get; set; }
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


        public void Approve()
        {
            Status = WorkflowStatusEnum.Level1;
        }

        public void Reject()
        {

        }

       
       
    }
   
}

