using System.Collections.Generic;
using JomMalaysia.Core.Domain.Entities;
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
            Assert.Equal(roles1, roles);
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
            Assert.Equal(roles1, roles);
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
            Assert.Equal(roles1, roles);
        }
    }
}