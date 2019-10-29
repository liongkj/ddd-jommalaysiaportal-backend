using System.Threading.Tasks;
using AutoMapper;
using JomMalaysia.Api.Scope;
using JomMalaysia.Core.Interfaces;
using JomMalaysia.Core.UseCases.UserUseCase;
using JomMalaysia.Core.UseCases.UserUseCase.Create;
using JomMalaysia.Core.UseCases.UserUseCase.Delete;
using JomMalaysia.Core.UseCases.UserUseCase.Get;
using JomMalaysia.Core.UseCases.UserUseCase.Update;
using JomMalaysia.Infrastructure.Auth0.Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace JomMalaysia.Api.UseCases.User
{
    #region dependencies
    [Route("api/[controller]")]
    [ApiController]
    // [Authorize(Policies.ADMIN)]

    public class UsersController : ControllerBase
    {
        private readonly IGetAllUserUseCase _getAllUserUseCase;
        private readonly IGetUserUseCase _getUserUseCase;
        private readonly UserPresenter _userPresenter;
        private readonly ICreateUserUseCase _createUserUseCase;
        private readonly IDeleteUserUseCase _deleteUserUseCase;
        private readonly IUpdateUserUseCase _updateUserUseCase;
        private readonly ILoginInfoProvider _loginInfo;
        private readonly IMapper _mapper;

        public UsersController(
            IGetAllUserUseCase getAllUserUseCase,
            UserPresenter UserPresenter,
            ICreateUserUseCase createUserUseCase,
            IDeleteUserUseCase deleteUserUseCase,
            IGetUserUseCase getUserUseCase,
            IUpdateUserUseCase updateUserUseCase,
            ILoginInfoProvider loginInfo,
            IMapper mapper)
        {
            _getAllUserUseCase = getAllUserUseCase;
            _getUserUseCase = getUserUseCase;
            _createUserUseCase = createUserUseCase;
            _userPresenter = UserPresenter;
            _deleteUserUseCase = deleteUserUseCase;
            _updateUserUseCase = updateUserUseCase;
            _loginInfo = loginInfo;
            _mapper = mapper;
        }
        #endregion


        //GET api/users/
        [HttpGet]
        [Authorize(Roles = "superadmin,manager")]
        public async Task<IActionResult> Get()
        {
            await _getAllUserUseCase.Handle(new GetAllUserRequest(), _userPresenter);

            return _userPresenter.ContentResult;
        }

        [HttpGet("{userid}")]
        public async Task<IActionResult> Get([FromRoute]string userid)
        {
            var req = new GetUserRequest(userid);
            await _getUserUseCase.Handle(req, _userPresenter);
            return _userPresenter.ContentResult;
        }

        //POST api/users/
        [HttpPost]
        public async Task<IActionResult> Post([FromBody]UserDto user)
        {
            var req = new CreateUserRequest(user.username, user.name, user.email);


            await _createUserUseCase.Handle(req, _userPresenter);

            return _userPresenter.ContentResult;
        }

        //DELETE api/users/{userid}
        [HttpDelete("{userid}")]
        public async Task<IActionResult> Delete([FromRoute]string userid)
        {
            var req = new DeleteUserRequest(userid);
            await _deleteUserUseCase.Handle(req, _userPresenter);
            return _userPresenter.ContentResult;
        }


        [HttpPatch("{userid}")]
        public async Task<IActionResult> Update([FromRoute]string userid, [FromBody]UpdateUserRequest updatedUser)
        {
            updatedUser.UserId = userid;
            await _updateUserUseCase.Handle(updatedUser, _userPresenter);
            return _userPresenter.ContentResult;
        }

        // PATCH /api/users/8bf3b151-49d1-4df0-8900-d6bc91b3650f/roles

    }
}