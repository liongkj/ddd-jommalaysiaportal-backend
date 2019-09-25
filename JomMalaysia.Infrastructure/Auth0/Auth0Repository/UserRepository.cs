using AutoMapper;
using JomMalaysia.Core.Domain.Entities;
using JomMalaysia.Core.Interfaces.Repositories;
using JomMalaysia.Core.UseCases.UserUseCase.Create;
using JomMalaysia.Core.UseCases.UserUseCase.Get.Response;
using JomMalaysia.Framework.Configuration;
using JomMalaysia.Framework.Helper;
using JomMalaysia.Infrastructure.Auth0.Entities;
using JomMalaysia.Infrastructure.Helpers;
using Newtonsoft.Json;
using RestSharp;
using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace JomMalaysia.Infrastructure.Auth0
{
    public class UserRepository : IUserRepository
    {
        private readonly IMapper _mapper;
        private readonly IAuth0Setting _appSetting;

        public UserRepository(IMapper mapper, IAuth0Setting auth0Setting)
        {
            _appSetting = auth0Setting;
            _mapper = mapper;
        }

        private async Task<string> getAccessToken()
        {
            var client = new RestClient("https://jomn9.auth0.com/oauth/token");
            var request = new RestRequest(Method.POST);
            request.AddHeader("content-type", "application/json");
            request.AddParameter("application/json", "{\"client_id\":\"9mWFLX7PoR6sELJWiH4JSUzV913LTuN6\",\"client_secret\":\"Ef3xlYarfqeys8S9g_hmnCjbOfufK63wtMP8Jl2uJM2ZymKd_gd0EUpHNGoFqhV0\",\"audience\":\"https://jomn9.auth0.com/api/v2/\",\"grant_type\":\"client_credentials\"}", ParameterType.RequestBody);
            IRestResponse response = await client.ExecuteTaskAsync(request, new CancellationTokenSource().Token);
            dynamic deserializedJson = JsonConvert.DeserializeObject(response.Content);

            return (deserializedJson.access_token != null) ? deserializedJson.access_token : null;
        }

        public GetAllUserResponse GetAllUsers(int countperpage = 10, int page = 0)
        {
            var client = new RestClient($"{_appSetting.Auth0UserManagementApi}?per_page=" + countperpage + "&page=" + page + "&include_totals=true");
            var request = new RestRequest(Method.GET);
            request.AddHeader("authorization", "Bearer " + getAccessToken());
            IRestResponse response = client.Execute(request);
            Auth0PagingHelper<UserDto> deserializedJson = JsonConvert.DeserializeObject<Auth0PagingHelper<UserDto>>(response.Content);
            var result = _mapper.Map<PagingHelper<User>>(deserializedJson);
            result.CurrentPage = page + 1;
            result.TotalRowCount = deserializedJson.total;
            result.PageSize = countperpage;
            int TotalPageCount = result.TotalRowCount / result.PageSize;
            result.PageCount = (result.TotalRowCount % result.PageSize == 0) ?
                TotalPageCount : TotalPageCount + 1;


            return deserializedJson.length == 0 ?
                new GetAllUserResponse(new List<string> { "No Users" }, false)
                : new GetAllUserResponse(result, true);
        }

        public async Task<CreateUserResponse> CreateUser(User user)
        {
            IRestResponse<CreateUserResponse> response;
            string accessToken = await getAccessToken();

            try
            {
                var userDto = _mapper.Map<UserDto>(user);
                var client = new RestClient(_appSetting.Auth0UserManagementApi);
                var request = new RestRequest(Method.POST);
                request.AddHeader("authorization", "Bearer " + accessToken);
                request.RequestFormat = DataFormat.Json;
                request.AddJsonBody(userDto);

                response = client.Execute<CreateUserResponse>(request);
            }
            catch (Exception e)
            {
                throw e;
            }
            if (response.IsSuccessful)
            {
                if (SendResetPasswordEmail(user.Email.ToString()))
                    return new CreateUserResponse(response.Data.user_id, true);
            }

            //var deserializedJson = JsonConvert.DeserializeObject(response.Content);

            return new CreateUserResponse(new List<string> { response.Content }, response.IsSuccessful);
        }


        public bool SendResetPasswordEmail(string email)
        {
            IRestResponse response;
            //string accessToken = await getAccessToken();

            try
            {

                var client = new RestClient(_appSetting.Auth0SendResetPasswordEmailApi);
                var request = new RestRequest(Method.POST);
                //request.AddHeader("authorization", "Bearer " + accessToken);
                request.AddHeader("content-type", "application/json");
                request.AddParameter("application / json", "{\"client_id\": \"9mWFLX7PoR6sELJWiH4JSUzV913LTuN6\",\"email\": \"" + email + "\",\"connection\": \"Username-Password-Authentication\"}", ParameterType.RequestBody);

                response = client.Execute(request);
            }
            catch (Exception e)
            {
                throw e;
            }
            return response.IsSuccessful;
        }
    }
}
