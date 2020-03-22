namespace JomMalaysia.Core.Interfaces
{
    public interface IAlgoliaSettings
    {
        string AppId { get; }

        string ApiKey { get; }
        string IndexName { get; }
    }
}