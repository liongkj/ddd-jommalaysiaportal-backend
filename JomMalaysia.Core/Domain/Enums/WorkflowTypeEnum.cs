using System;
using System.Collections.Generic;
using System.Text;

namespace JomMalaysia.Core.Domain.Enums
{
    public class WorkflowTypeEnum : EnumerationBase
    {
        public static WorkflowTypeEnum Publish = new WorkflowTypeEnum(0, "Publish".ToLowerInvariant());
        public static WorkflowTypeEnum Edit = new WorkflowTypeEnum(1, "Edit".ToLowerInvariant());
        public static WorkflowTypeEnum Unpublish = new WorkflowTypeEnum(1, "Unpublish".ToLowerInvariant());
        public static WorkflowTypeEnum Delete = new WorkflowTypeEnum(1, "Delete".ToLowerInvariant());
       
       
        public WorkflowTypeEnum(int id, string name) : base(id, name)
        {

        }
    }
}
