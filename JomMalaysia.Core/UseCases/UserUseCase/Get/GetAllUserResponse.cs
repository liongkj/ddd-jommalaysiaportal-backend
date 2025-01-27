﻿using JomMalaysia.Core.Domain.Entities;
using JomMalaysia.Core.Interfaces;
using JomMalaysia.Framework.Helper;
using System;
using System.Collections.Generic;
using System.Text;

namespace JomMalaysia.Core.UseCases.UserUseCase.Get
{
    public class GetAllUserResponse : UseCaseResponseMessage
    {
        public PagingHelper<User> Users { get; }

        public PagingHelper<UserViewModel> Data { get; }
        public IEnumerable<string> Errors { get; }

        public GetAllUserResponse(IEnumerable<string> errors, bool success = false, string message = null) : base(success, message)
        {
            Errors = errors;
        }

        public GetAllUserResponse(PagingHelper<User> Users, bool success = false, string message = null) : base(success, message)
        {
            this.Users = Users;
        }

        public GetAllUserResponse(PagingHelper<UserViewModel> Users, bool success = false, string message = null) : base(success, message)
        {
            this.Data = Users;
        }
    }
}
