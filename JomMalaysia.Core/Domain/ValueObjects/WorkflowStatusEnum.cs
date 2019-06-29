using System;
using System.Collections.Generic;
using System.Text;

namespace JomMalaysia.Core.Domain.ValueObjects
{
    public class WorkflowStatusEnum : EnumerationBase
    {
        public static WorkflowStatusEnum Pending = new WorkflowStatusEnum(0, "Pending".ToLowerInvariant());
        public static WorkflowStatusEnum Completed = new WorkflowStatusEnum(1, "Completed".ToLowerInvariant());
        public static WorkflowStatusEnum Rejected = new WorkflowStatusEnum(1, "Rejected".ToLowerInvariant());
        public static WorkflowStatusEnum Level1 = new WorkflowStatusEnum(1, "Level1".ToLowerInvariant());
        public static WorkflowStatusEnum Level2 = new WorkflowStatusEnum(1, "Level2".ToLowerInvariant());
       
        public WorkflowStatusEnum(int id, string name) : base(id, name)
        {

        }
    }
}
