﻿using System.Collections.Generic;
using System.Collections.ObjectModel;
using JomMalaysia.Core.Domain.Entities;
using JomMalaysia.Core.Domain.Enums;
using JomMalaysia.Core.Domain.ValueObjects;
using JomMalaysia.Core.Interfaces;
using JomMalaysia.Core.UseCases.CatogoryUseCase.Get;

namespace JomMalaysia.Core.UseCases.WorkflowUseCase.Get
{
    public class GetAllWorkflowRequest : IUseCaseRequest<GetAllWorkflowResponse>
    {
        public WorkflowStatusEnum Status;

        public GetAllWorkflowRequest(string status)
        {
            switch (status)
            {
                case "pending":
                    Status= WorkflowStatusEnum.Pending;
                    break;
                case "level1":
                    Status = WorkflowStatusEnum.Level1;
                    break;
                case "level2":
                    Status = WorkflowStatusEnum.Level2;
                    break;
                case "completed":
                    Status = WorkflowStatusEnum.Completed;
                    break;
                case "rejected":
                    Status = WorkflowStatusEnum.Rejected;
                    break;
                default:
                    Status = WorkflowStatusEnum.All;
                    break;
            }
        }
    }
}