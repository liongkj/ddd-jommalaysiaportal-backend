using System;
using System.Collections.Generic;
using System.Text;

namespace JomMalaysia.Core.Domain.Enums
{
    public class UserRoleEnum : EnumerationBase
    {
        public static UserRoleEnum Superadmin = new UserRoleEnum(4, "Superadmin".ToLowerInvariant());
        public static UserRoleEnum Manager = new UserRoleEnum(3, "Manager".ToLowerInvariant());
        public static UserRoleEnum Admin = new UserRoleEnum(2, "Admin".ToLowerInvariant());
        public static UserRoleEnum Editor = new UserRoleEnum(1, "Editor".ToLowerInvariant());

        public UserRoleEnum(int id, string name) : base(id, name)
        {

        }
        public bool HasHigherAuthority(UserRoleEnum otherRoles)
        {
            return this.Id > otherRoles.Id;
        }
        public static UserRoleEnum For(string enumstring)
        {

            return Parse<UserRoleEnum>(enumstring);
        }
    }
}