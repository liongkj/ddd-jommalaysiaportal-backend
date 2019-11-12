using System;
using System.Collections.Generic;
using JomMalaysia.Core.Domain.ValueObjects;

namespace JomMalaysia.Core.UseCases.WorkflowUseCase.Get
{
    public class WorkflowViewModel
    {
        public string WorkflowId { get; set; }
        public string Type { get; set; }
        public string Status { get; set; }
        public string Level { get; set; }
        public ListingSummary Listing { get; set; }
        public UserVM Requester { get; set; }
        public UserVM Responder { get; set; }

        public DateTime Created { get; set; }

        public ICollection<WorkflowSummaryViewModel> HistoryData { get; set; }
    }

    public class WorkflowSummaryViewModel
    {
        public string Status { get; set; }
        public string Level { get; set; }
        public UserVM Responder { get; set; }
        public string Action { get; set; }
        public DateTime Created { get; set; }
    }
    public class UserVM
    {
        public string Role { get; set; }
        public string Username { get; set; }
        public string UserId { get; set; }
    }
    public class ListingSummary
    {
        public string ListingId { get; set; }
        public MerchantVM Merchant { get; set; }
        public string ListingName { get; set; }
        public string ListingType { get; set; }
        public string Status { get; set; }
    }

    public class MerchantVM
    {
        public string MerchantId { get; set; }
        public string SsmId { get; set; }
        public string RegistrationName { get; set; }
    }


}