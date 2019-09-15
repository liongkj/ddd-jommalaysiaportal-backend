using System;
using System.Collections.Generic;
using System.Text;

namespace JomMalaysia.Core.Domain.Enums
{
    public class WorkflowStatusEnum : EnumerationBase
    {
        public static WorkflowStatusEnum Pending = new WorkflowStatusEnum(0, "Pending".ToLowerInvariant());
        public static WorkflowStatusEnum Completed = new WorkflowStatusEnum(10, "Completed".ToLowerInvariant());
        public static WorkflowStatusEnum Rejected = new WorkflowStatusEnum(99, "Rejected".ToLowerInvariant());
        public static WorkflowStatusEnum Level1 = new WorkflowStatusEnum(1, "Level1".ToLowerInvariant());
        public static WorkflowStatusEnum Level2 = new WorkflowStatusEnum(2, "Level2".ToLowerInvariant());
        public static WorkflowStatusEnum All = new WorkflowStatusEnum(11, "All".ToLowerInvariant());
        public static WorkflowStatusEnum NotFound = new WorkflowStatusEnum(111, "NotFound".ToLowerInvariant());
        public WorkflowStatusEnum(int id, string name) : base(id, name)
        {

        }


    }
}
