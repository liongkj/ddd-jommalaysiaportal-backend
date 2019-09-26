﻿using System.Threading.Tasks;
using AutoMapper;
using JomMalaysia.Core.Interfaces;
using JomMalaysia.Core.UseCases.UserUseCase;
using JomMalaysia.Core.UseCases.UserUseCase.Create;
using JomMalaysia.Core.UseCases.UserUseCase.Delete;
using JomMalaysia.Core.UseCases.UserUseCase.Get.Request;
using JomMalaysia.Infrastructure.Auth0.Entities;
using Microsoft.AspNetCore.Mvc;

namespace JomMalaysia.Api.UseCases.User
{
    #region dependenciees
    [Route("api/[controller]")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        private readonly IGetAllUserUseCase _getAllUserUseCase;
        private readonly UserPresenter _userPresenter;
        private readonly ICreateUserUseCase _createUserUseCase;
        private readonly IDeleteUserUseCase _deleteUserUseCase;
        private readonly ILoginInfoProvider _loginInfo;
        private readonly IMapper _mapper;

        public UsersController(
            IGetAllUserUseCase getAllUserUseCase,
            UserPresenter UserPresenter,
            ICreateUserUseCase createUserUseCase,
            IDeleteUserUseCase deleteUserUseCase,
            ILoginInfoProvider loginInfo,
            IMapper mapper)
        {
            _getAllUserUseCase = getAllUserUseCase;
            _createUserUseCase = createUserUseCase;
            _userPresenter = UserPresenter;
            _deleteUserUseCase = deleteUserUseCase;
            _loginInfo = loginInfo;
            _mapper = mapper;
        }
        #endregion

        [HttpGet]
        public async Task<IActionResult> Get()
        {
            await _getAllUserUseCase.Handle(new GetAllUserRequest(), _userPresenter);

            return _userPresenter.ContentResult;
        }

        [HttpPost]
        public async Task<IActionResult> Post([FromBody]UserDto user)
        {
            var req = new CreateUserRequest(user.username, user.name, user.email);


            await _createUserUseCase.Handle(req, _userPresenter);

            return _userPresenter.ContentResult;
        }

        [HttpDelete("{userid}")]
        public async Task<IActionResult> Delete([FromRoute]string userid)
        {
            var req = new DeleteUserRequest(userid);
            await _deleteUserUseCase.Handle(req, _userPresenter);
            return _userPresenter.ContentResult;
        }

    }
}