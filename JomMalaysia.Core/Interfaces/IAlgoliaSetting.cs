namespace JomMalaysia.Core.Interfaces
{
    public interface IAlgoliaSetting
    {
        string AppId { get; set; }
        string ApiKey { get;  set; }
        string IndexName { get;  set; }
    }
}