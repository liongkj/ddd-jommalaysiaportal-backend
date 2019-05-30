namespace JomMalaysia.Core.Interfaces
{
    public abstract class UsecaseResponseMessage
    {
        public bool Success { get; set; }
        public string Message { get; set; }

        protected UsecaseResponseMessage(bool success= false, string message = null)
        {
            Success = success;
            Message = message;
        }
    }
}
