using System.Collections.Generic;

namespace JomMalaysia.Api.Providers
{
    public class ErrorModel
    {
        public string FieldName { get; set; }
        public string Message { get; set; }
    }

    public class ErrorReponse
    {
        public List<ErrorModel> Errors { get; set; } = new List<ErrorModel>();
    }
}