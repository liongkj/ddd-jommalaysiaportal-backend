using AutoMapper;
using JomMalaysia.Api.UseCases.User.CreateUser;
using JomMalaysia.Api.UseCases.User.GetUser;
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
        private readonly GetAllUserPresenter _getAllUserPresenter;
        private readonly ICreateUserUseCase _createUserUseCase;
        private readonly CreateUserPresenter _createUserPresenter;
        private readonly ILoginInfoProvider _loginInfo;
        private readonly IMapper _mapper;

        public UserController(
            IGetAllUserUseCase getAllUserUseCase,
            GetAllUserPresenter AllUserPresenter,
            ICreateUserUseCase createUserUseCase,
            CreateUserPresenter CreateUserPresenter,
            ILoginInfoProvider loginInfo,
            IMapper mapper)
        {
            _getAllUserUseCase = getAllUserUseCase;
            _getAllUserPresenter = AllUserPresenter;
            _createUserUseCase = createUserUseCase;
            _createUserPresenter = CreateUserPresenter;
            _loginInfo = loginInfo;
            _mapper = mapper;
        }

        [HttpGet]
        public IActionResult Get()
        {
            _getAllUserUseCase.Handle(new GetAllUserRequest(), _getAllUserPresenter);

            return _getAllUserPresenter.ContentResult;
        }

        [HttpPost]
        public IActionResult Post(UserDto user)
        {
            var MappedUser = _mapper.Map<UserDto, Core.Domain.Entities.User>(user);

            _createUserUseCase.Handle(new CreateUserRequest(MappedUser), _createUserPresenter);

            return _createUserPresenter.ContentResult;
        }

    }
}