using AutoMapper;
using JomMalaysia.Core.Domain.Entities;
using JomMalaysia.Core.Interfaces.Repositories;
using JomMalaysia.Core.UseCases.UserUseCase.Create;
using JomMalaysia.Core.UseCases.UserUseCase.Delete;
using JomMalaysia.Core.UseCases.UserUseCase.Get.Response;
using JomMalaysia.Framework.Configuration;
using JomMalaysia.Framework.Helper;
using JomMalaysia.Infrastructure.Auth0.Entities;
using JomMalaysia.Infrastructure.Helpers;
using Newtonsoft.Json;
using RestSharp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using System.Net;

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
            var client = new RestClient(_appSetting.RequestAccessTokenApi);
            var request = new RestRequest(Method.POST);
            request.AddHeader("content-type", "application/json");
            request.AddParameter("application/json", "{\"client_id\":\"" + _appSetting.Auth0ClientId + "\",\"client_secret\":\"" + _appSetting.Auth0ClientSecret + "\",\"audience\":\"https://jomn9.auth0.com/api/v2/\",\"grant_type\":\"client_credentials\"}", ParameterType.RequestBody);
            IRestResponse response = await client.ExecuteTaskAsync(request, new CancellationTokenSource().Token);
            dynamic deserializedJson = JsonConvert.DeserializeObject(response.Content);

            return (deserializedJson.access_token != null) ? deserializedJson.access_token : null;
        }

        public async Task<GetAllUserResponse> GetAllUsers(int countperpage = 10, int page = 0)
        {
            PagingHelper<User> result;
            Auth0PagingHelper<Auth0User> deserializedJson;
            try
            {
                var client = new RestClient($"{_appSetting.Auth0UserManagementApi}?per_page=" + countperpage + "&page=" + page + "&include_totals=true");
                var request = new RestRequest(Method.GET);
                request.AddHeader("authorization", "Bearer " + await getAccessToken());
                IRestResponse response = await client.ExecuteTaskAsync(request, new CancellationTokenSource().Token);

                deserializedJson = JsonConvert.DeserializeObject<Auth0PagingHelper<Auth0User>>(response.Content);

                result = _mapper.Map<PagingHelper<User>>(deserializedJson);
            }
            catch (Exception e)
            {
                throw e;
            }

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

            CreateUserResponse createUserResponse;
            IRestResponse response;
            string accessToken = await getAccessToken();

            try
            {
                var userDto = _mapper.Map<UserDto>(user);
                var client = new RestClient(_appSetting.Auth0UserManagementApi);
                var request = new RestRequest(Method.POST);
                request.AddHeader("authorization", "Bearer " + accessToken);
                request.RequestFormat = DataFormat.Json;
                request.AddJsonBody(userDto);
                response = await client.ExecuteTaskAsync(request, new CancellationTokenSource().Token);
            }
            catch (Exception e)
            {
                throw e;
            }
            if (response.IsSuccessful)
            {
                var tempuser = JsonConvert.DeserializeObject<Auth0User>(response.Content);
                var userid = tempuser.user_id;
                createUserResponse = new CreateUserResponse(userid, true, "User Created Successfully");
                if (!SendResetPasswordEmail(tempuser.email))
                    createUserResponse = new CreateUserResponse("Fail to send reset password email.", true);
                return createUserResponse;
            }
            else
            {
                var Error = JsonConvert.DeserializeObject<Auth0Errors>(response.Content);
                createUserResponse = new CreateUserResponse(Error.StatusCode, false, Error.Message);
            }
            return createUserResponse;
        }

        public async Task<DeleteUserResponse> DeleteUser(string Userid)
        {
            DeleteUserResponse deleteUserResponse;
            IRestResponse response;
            string accessToken = await getAccessToken();

            try
            {
                var client = new RestClient(_appSetting.Auth0UserManagementApi);
                var GetRequest = new RestRequest(Userid, Method.GET);
                GetRequest.AddHeader("authorization", "Bearer " + accessToken);
                IRestResponse GetResponse = await client.ExecuteTaskAsync(GetRequest, new CancellationTokenSource().Token);
                if (GetResponse.StatusCode == HttpStatusCode.OK)
                {
                    var DeleteRequest = new RestRequest(Userid, Method.DELETE);
                    DeleteRequest.AddHeader("authorization", "Bearer " + accessToken);
                    response = await client.ExecuteTaskAsync(DeleteRequest, new CancellationTokenSource().Token);
                    if (response.IsSuccessful)
                    {
                        deleteUserResponse = new DeleteUserResponse("User Deleted Successfully", true);
                    }
                    else
                    {
                        var Error = JsonConvert.DeserializeObject<Auth0Errors>(response.Content);
                        deleteUserResponse = new DeleteUserResponse(Error.StatusCode, false, Error.Message);
                    }

                    return deleteUserResponse;
                }
                else
                {
                    var Error = JsonConvert.DeserializeObject<Auth0Errors>(GetResponse.Content);
                    deleteUserResponse = new DeleteUserResponse(Error.StatusCode, false, Error.Message);
                }
                return deleteUserResponse;
            }
            catch (Exception e)
            {
                throw e;
            }

        }
        private bool SendResetPasswordEmail(string email)
        {
            IRestResponse response;

            try
            {

                var client = new RestClient(_appSetting.Auth0SendResetPasswordEmailApi);
                var request = new RestRequest(Method.POST);
                //request.AddHeader("authorization", "Bearer " + accessToken);
                request.AddHeader("content-type", "application/json");
                request.AddParameter("application / json", "{\"client_id\": \"" + _appSetting.Auth0ClientId + "\",\"email\": \"" + email + "\",\"connection\": \"Username-Password-Authentication\"}", ParameterType.RequestBody);

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
