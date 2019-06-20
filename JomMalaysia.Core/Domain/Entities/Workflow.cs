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
        public WorkflowStatusEnum Status { get; set; }
        public string Details{get;set;}
        public ICollection<Workflow> PreviousWorkflows { get; set; }

        public Workflow()
        {
            PreviousWorkflows = new Collection<Workflow>();
        }

        public void Approve()
        {

        }

        public void Reject()
        {

        }

    }
}
