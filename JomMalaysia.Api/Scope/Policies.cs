namespace JomMalaysia.Api.Scope
{
    public static class Policies
    {

        public const string MANAGER = "RequireManagerRole";
        public const string SUPERADMIN = "RequireSuperadminRole";
        public const string EDITOR = "RequireEditorRole";
        public const string ADMIN = "RequireAdminRole";
    }
}