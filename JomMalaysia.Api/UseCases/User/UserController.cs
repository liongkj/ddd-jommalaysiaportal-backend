using AutoMapper;
using JomMalaysia.Core.Interfaces;
using JomMalaysia.Core.UseCases.UserUseCase;
using JomMalaysia.Core.UseCases.UserUseCase.Create;
using JomMalaysia.Core.UseCases.UserUseCase.Get.Request;
using JomMalaysia.Infrastructure.Auth0.Entities;
using Microsoft.AspNetCore.Mvc;

namespace JomMalaysia.Api.UseCases.User
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly IGetAllUserUseCase _getAllUserUseCase;
        private readonly UserPresenter _userPresenter;
        private readonly ICreateUserUseCase _createUserUseCase;

        private readonly ILoginInfoProvider _loginInfo;
        private readonly IMapper _mapper;

        #region dependecies
        public UserController(
            IGetAllUserUseCase getAllUserUseCase,
            UserPresenter UserPresenter,
            ICreateUserUseCase createUserUseCase,
            ILoginInfoProvider loginInfo,
            IMapper mapper)
        {
            _getAllUserUseCase = getAllUserUseCase;
            _createUserUseCase = createUserUseCase;
            _userPresenter = UserPresenter;
            _loginInfo = loginInfo;
            _mapper = mapper;
        }
        #endregion

        [HttpGet]
        public IActionResult Get()
        {
            _getAllUserUseCase.Handle(new GetAllUserRequest(), _userPresenter);

            return _userPresenter.ContentResult;
        }

        [HttpPost]
        public IActionResult Post(UserDto user)
        {
            var MappedUser = _mapper.Map<UserDto, Core.Domain.Entities.User>(user);

            _createUserUseCase.Handle(new CreateUserRequest(MappedUser), _userPresenter);

            return _userPresenter.ContentResult;
        }

    }
}