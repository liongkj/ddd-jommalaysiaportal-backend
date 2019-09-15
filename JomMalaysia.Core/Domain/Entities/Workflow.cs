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
        public int Lvl { get; set; } = 1;

        public Listing Listing { get; set; }
        public User Requester { get; set; }
        public User Responder { get; set; }
        public WorkflowStatusEnum Status { get; set; }
        public DateTime Created { get; set; }
        public string Details { get; set; }
        public ICollection<Workflow> PreviousWorkflows { get; private set; }

        public Workflow()
        {

        }
        public Workflow(User user, Listing listing, WorkflowTypeEnum type)
        {
            Requester = user;
            Listing = listing;
            Created = DateTime.Now;
            Type = type;
            Status = WorkflowStatusEnum.Pending;

            PreviousWorkflows = new Collection<Workflow>();
        }


        public void Approve()
        {
            Status = WorkflowStatusEnum.Level1;
        }

        public void Reject()
        {

        }

        public void Start()
        {
            switch (this.Type.ToString())
            {
                case "publish":
                    PublishListingOperation();
                    break;
                case "update":
                    EditLiveListingOperation();
                    break;
            }

        }

        private void EditLiveListingOperation()
        {
        }

        private void PublishListingOperation()
        {
            //Type = WorkflowTypeEnum.Publish;

        }
    }

}
