using JomMalaysia.Core.Domain.Entities;
using JomMalaysia.Core.Domain.Enums;
using JomMalaysia.Core.Interfaces;
using Xunit;

namespace Core.Test.ValueObject
{
    public class UserRoleTest
    {
        User Editor = new User
        {
            UserId = "1",
            Role = UserRoleEnum.Editor
        };
        User Admin = new User
        {
            UserId = "2",
            Role = UserRoleEnum.Admin
        };
        User Manager = new User
        {
            UserId = "3",
            Role = UserRoleEnum.Manager
        };
        User Manager2 = new User
        {
            UserId = "30",
            Role = UserRoleEnum.Manager
        };

        User Superadmin = new User
        {
            UserId = "4",
            Role = UserRoleEnum.Superadmin
        };
        [Fact]
        public void UserCanDeleteSelf_IsFalse()
        {

            Assert.False(Editor.CanDelete(Editor));
        }


        [Fact]
        public void ManagerCanDeleteManager_IsFalse()
        {
            Assert.False(Manager.CanDelete(Manager2));
        }

        [Fact]
        public void ManagerCanDeleteAdmin_IsTrue()
        {

            Assert.True(Manager.CanDelete(Admin));
        }

        [Fact]
        public void AdminCanDeleteManager_IsFalse()
        {

            Assert.False(Admin.CanDelete(Manager));
        }

        [Fact]
        public void EditorCanDeleteManager_IsFalse()
        {

            Assert.False(Editor.CanDelete(Manager));
        }

        [Fact]
        public void ManagerCanDeleteEditor_IsTrue()
        {

            Assert.True(Manager.CanDelete(Editor));
        }

        [Fact]
        public void ManagerHasHigherAuthorityThanAdmin_IsTrue()
        {
            var adminrole = UserRoleEnum.Admin;
            var managerrole = UserRoleEnum.Manager;
            Assert.True(managerrole.HasHigherAuthority(adminrole));
        }

        [Fact]
        public void ManagerHasHigherAuthorityThanManager2_IsFalse()
        {
            var managerrole1 = UserRoleEnum.Manager;
            var managerrole2 = UserRoleEnum.Manager;
            Assert.False(managerrole1.HasHigherAuthority(managerrole2));
        }

        [Fact]
        public void AdminHasHigherAuthorityThanEdior_IsTrue()
        {
            var adminrole = UserRoleEnum.Admin;
            var EditorRole = UserRoleEnum.Editor;
            Assert.True(adminrole.HasHigherAuthority(EditorRole));
        }

        [Fact]
        public void EditorHasHigherAuthorityThanAdmin_IsFalse()
        {
            var adminrole = UserRoleEnum.Admin;
            var EditorRole = UserRoleEnum.Editor;
            Assert.False(EditorRole.HasHigherAuthority(adminrole));
        }
    }
}