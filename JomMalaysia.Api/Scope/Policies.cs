namespace JomMalaysia.Api.Scope
{
    public static class Policies
    {
        public const string NAMESPACE = "https://jomn9.com/roles";
        public const string MANAGER = "RequireManagerRole";
        public const string SUPERADMIN = "RequireSuperadminRole";
        public static string EDITOR = "RequireEditorRole";
    }
}