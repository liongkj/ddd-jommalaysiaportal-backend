﻿using System;
using System.Collections.Generic;
using System.Text;
using JomMalaysia.Core.Domain.Entities;
using JomMalaysia.Core.Interfaces;

namespace JomMalaysia.Core.UseCases.WorkflowUseCase.Get
{
    public class GetAllWorkflowResponse : UseCaseResponseMessage
    {
        public List<Workflow> Workflows { get; }
        public List<WorkflowViewModel> Data { get; }
        public IEnumerable<string> Errors { get; }

        public GetAllWorkflowResponse(IEnumerable<string> errors, bool success = false, string message = null) : base(success, message)
        {
            Errors = errors;
        }

        public GetAllWorkflowResponse(List<Workflow> Workflows, bool success = false, string message = null) : base(success, message)
        {
            this.Workflows = Workflows;
        }

        public GetAllWorkflowResponse(List<WorkflowViewModel> Workflows, bool success = false, string message = null) : base(success, message)
        {
            this.Data = Workflows;
        }
    }
}
