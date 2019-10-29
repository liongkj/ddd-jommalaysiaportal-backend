using System;

namespace JomMalaysia.Core.UseCases.UserUseCase.Get
{
    public class UserViewModel
    {
        public string UserId { get; set; }
        public string Username { get; set; }
        public string Email { get; set; }
        public string Name { get; set; }
        public string Role { get; set; }
        public string PictureUri { get; set; } = "https://www.gravatar.com/avatar/?d=mp";
        public DateTime LastLogin { get; set; }
        public bool HasAuthority { get; set; }
    }
}