using System.Collections.Generic;
using JomMalaysia.Core.Domain.Entities;
using JomMalaysia.Core.Domain.Enums;
using Xunit;

namespace Core.Test.ValueObject
{
    public class UserRoleListTest
    {
        [Fact]
        public void ManagerIsSelected_IsTrue()
        {
            //Given
            User user = new User();
            string role = "manager";
            //When
            var roles1 = user.UpdateRole(role);
            //Then
            List<string> roles = new List<string> { "editor", "admin", "manager" };
            Assert.Equal(roles1.Item1, roles);
            Assert.False(roles1.Item2);
        }

        [Fact]
        public void SupeadminIsSelected_IsTrue()
        {
            //Given
            User user = new User();
            string role = "superadmin";
            //When
            var roles1 = user.UpdateRole(role);
            //Then
            List<string> roles = new List<string> { "editor", "admin", "manager", "superadmin" };
            Assert.Equal(roles1.Item1, roles);
            Assert.False(roles1.Item2);
        }

        [Fact]
        public void EditorIsSelected_IsTrue()
        {
            //Given
            User user = new User();
            string role = "editor";
            //When
            var roles1 = user.UpdateRole(role);
            //Then
            List<string> roles = new List<string> { "editor" };
            Assert.Equal(roles1.Item1, roles);
            Assert.False(roles1.Item2);
        }


        [Fact]
        public void UpdateFromManagerToSuperadmin()
        {
            //Given
            User user = new User
            {
                Role = UserRoleEnum.Manager
            };
            string role = "Superadmin";
            //When
            var roles1 = user.UpdateRole(role);
            //Then

            Assert.Equal(roles1.Item1.Count, 4);
            Assert.False(roles1.Item2);
        }

        [Fact]
        public void UpdateFromSuperadminToManager()
        {
            //Given
            User user = new User { Role = UserRoleEnum.Superadmin };
            string role = "Manager";
            //When
            var roles1 = user.UpdateRole(role);
            //Then

            Assert.Equal(roles1.Item1.Count, 1);
            Assert.True(roles1.Item2);
        }


        [Fact]
        public void UpdateFromSuperadminToEditor()
        {
            //Given
            User user = new User { Role = UserRoleEnum.Superadmin };
            string role = "Editor";
            //When
            var roles1 = user.UpdateRole(role);
            //Then

            Assert.Equal(roles1.Item1.Count, 3);
            Assert.True(roles1.Item2);
        }
    }
}