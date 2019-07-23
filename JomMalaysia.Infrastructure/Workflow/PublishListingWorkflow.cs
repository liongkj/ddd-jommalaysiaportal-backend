using System;
using System.Collections.Generic;
using System.Text;
using JomMalaysia.Core.Interfaces;
using JomMalaysia.Core.UseCases.WorkflowUseCase;
using WorkflowCore.Interface;

namespace JomMalaysia.Infrastructure.Workflow
{
    public class PublishListingWorkflow : IPublishWorkflow
    {
        public string Id => "Publish Workflow";

        public int Version => 1;

        public void Build(IWorkflowBuilder<object> builder)
        {
            builder
                .StartWith<PublishWorkflowInit>();
        }
    }
}
