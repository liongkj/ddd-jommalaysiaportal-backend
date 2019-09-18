using System;
using System.Collections.Generic;
using System.Text;
using FluentValidation;
using JomMalaysia.Core.Domain.Entities;
using JomMalaysia.Core.Domain.Enums;
using JomMalaysia.Core.Domain.ValueObjects;
using JomMalaysia.Core.UseCases.ListingUseCase.Create;
using Xunit;

namespace JomMalaysia.Test.Core.UseCase
{

    public class WorkflowTest
    {

        [Fact]
        public void WorkflowStatusLevel1_IsComplete_ReturnFalse()
        {
            var workflow = new Workflow();
            workflow.Status = WorkflowStatusEnum.Level1;
            Assert.False(workflow.IsCompleted(), "Should be false");
        }

        [Fact]
        public void WorkflowStatusLevel2_IsComplete_ReturnFalse()
        {
            var workflow = new Workflow();
            workflow.Status = WorkflowStatusEnum.Level2;
            Assert.False(workflow.IsCompleted(), "Should be false");
        }

        [Fact]
        public void WorkflowStatusPending_IsComplete_ReturnFalse()
        {
            var workflow = new Workflow();
            workflow.Status = WorkflowStatusEnum.Pending;
            Assert.False(workflow.IsCompleted(), "Should be false");
        }

        [Fact]
        public void WorkflowStatusRejected_IsComplete_ReturnTrue()
        {
            var workflow = new Workflow();
            workflow.Status = WorkflowStatusEnum.Rejected;
            Assert.True(workflow.IsCompleted(), "Should be True");
        }

        [Fact]
        public void WorkflowStatusCompleted_IsComplete_ReturnTrue()
        {
            var workflow = new Workflow();
            workflow.Status = WorkflowStatusEnum.Completed;
            Assert.True(workflow.IsCompleted(), "Should be True");
        }
    }
}
