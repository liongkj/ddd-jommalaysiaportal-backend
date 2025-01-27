﻿using AutoMapper;
using JomMalaysia.Core.Domain.Entities;
using JomMalaysia.Core.Interfaces.Repositories;
using JomMalaysia.Core.UseCases.UserUseCase.Create;
using JomMalaysia.Core.UseCases.UserUseCase.Delete;
using JomMalaysia.Framework.Configuration;
using JomMalaysia.Framework.Helper;
using JomMalaysia.Infrastructure.Auth0.Entities;
using Newtonsoft.Json;
using RestSharp;
using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using System.Net;
using JomMalaysia.Core.UseCases.UserUseCase.Get;
using JomMalaysia.Core.UseCases.UserUseCase.Update;
using System.Linq;
using JomMalaysia.Core.Interfaces;
using JomMalaysia.Core.Exceptions;

namespace JomMalaysia.Infrastructure.Auth0
{
    public class UserRepository : IUserRepository
    {
        private readonly IMapper _mapper;
        private readonly IAuth0Setting _appSetting;
        private readonly IOAuthTokenManager _tokenManager;
        public UserRepository(IMapper mapper, IAuth0Setting auth0Setting, IOAuthTokenManager tokenManager)
        {
            _appSetting = auth0Setting;
            _mapper = mapper;
            _tokenManager = tokenManager;
        }

        //private async Task<string> getAccessToken()
        //{
        //    var client = new RestClient(_appSetting.RequestAccessTokenApi);
        //    var request = new RestRequest(Method.POST);
        //    request.AddHeader("content-type", "application/json");
        //    request.AddParameter("application/json", "{\"client_id\":\"" + _appSetting.Auth0ClientId + "\",\"client_secret\":\"" + _appSetting.Auth0ClientSecret + "\",\"audience\":\"https://jomn9.auth0.com/api/v2/\",\"grant_type\":\"client_credentials\"}", ParameterType.RequestBody);
        //    IRestResponse response = await client.ExecuteAsync(request, new CancellationTokenSource().Token);
        //    dynamic deserializedJson = JsonConvert.DeserializeObject(response.Content);

        //    return (deserializedJson.access_token != null) ? deserializedJson.access_token : null;
        //}




        public async Task<GetAllUserResponse> GetAllUsers(int countperpage = 10, int page = 0)
        {
            PagingHelper<User> result;
            Auth0PagingHelper<Auth0User> deserializedJson;
            try
            {
                var client = new RestClient($"{_appSetting.Auth0UserManagementApi}?per_page=" + countperpage + "&page=" + page + "&include_totals=true");
                var request = new RestRequest(Method.GET);
                request.AddHeader("authorization", "Bearer " + await _tokenManager.GetAccessToken("auth0"));
                IRestResponse response = await client.ExecuteAsync(request, new CancellationTokenSource().Token);

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


        public async Task<GetUserResponse> GetUser(string userId)
        {
            IRestResponse GetResponse;
            GetUserResponse response;
            string accessToken = await _tokenManager.GetAccessToken("auth0");
            try
            {
                var client = new RestClient(_appSetting.Auth0UserManagementApi);
                var GetRequest = new RestRequest(userId, Method.GET);
                GetRequest.AddHeader("authorization", "Bearer " + accessToken);
                GetResponse = await client.ExecuteAsync(GetRequest, new CancellationTokenSource().Token);
                if (GetResponse.StatusCode == HttpStatusCode.OK)
                {
                    var auth0User = JsonConvert.DeserializeObject<Auth0User>(GetResponse.Content);
                    var user = _mapper.Map<User>(auth0User);
                    response = new GetUserResponse(user, true);
                }
                else
                {
                    var Error = JsonConvert.DeserializeObject<Auth0Errors>(GetResponse.Content);
                    response = new GetUserResponse(new List<string> { Error.Error }, false, Error.Message, Error.StatusCode);
                }
                return response;

            }
            catch (Exception e)
            {
                throw e;
            }
        }

        public async Task<CreateUserResponse> CreateUser(User user)
        {

            CreateUserResponse createUserResponse;
            IRestResponse response;
            string accessToken = await _tokenManager.GetAccessToken("auth0");

            try
            {
                var userDto = _mapper.Map<UserDto>(user);
                var client = new RestClient(_appSetting.Auth0UserManagementApi);
                var request = new RestRequest(Method.POST);
                request.AddHeader("authorization", "Bearer " + accessToken);
                request.RequestFormat = DataFormat.Json;
                request.AddJsonBody(userDto);
                response = await client.ExecuteAsync(request, new CancellationTokenSource().Token);
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
                // if (Error.StatusCode == (int)HttpStatusCode.Conflict) throw new DuplicatedException(Error.Error, Error.Message);

                createUserResponse = new CreateUserResponse(new List<string> { Error.Error }, false, Error.Message, Error.StatusCode);
            }
            return createUserResponse;
        }

        public async Task<DeleteUserResponse> DeleteUser(string Userid)
        {
            DeleteUserResponse deleteUserResponse;
            IRestResponse response;
            string accessToken = await _tokenManager.GetAccessToken("auth0");

            try
            {
                var client = new RestClient(_appSetting.Auth0UserManagementApi);
                var DeleteRequest = new RestRequest(Userid, Method.DELETE);
                DeleteRequest.AddHeader("authorization", "Bearer " + accessToken);
                response = await client.ExecuteAsync(DeleteRequest, new CancellationTokenSource().Token);
                if (response.IsSuccessful)
                {
                    deleteUserResponse = new DeleteUserResponse("User Deleted Successfully", true);
                }
                else
                {
                    var Error = JsonConvert.DeserializeObject<Auth0Errors>(response.Content);
                    deleteUserResponse = new DeleteUserResponse(new List<string> { Error.Error }, false, Error.Message, Error.StatusCode);
                }

                return deleteUserResponse;

            }
            catch (Exception e)
            {
                throw e;
            }

        }

        //authorization api

        //private async Task<string> getAuthorizationApiAccessToken()
        //{
        //    var client = new RestClient(_appSetting.RequestAccessTokenApi);
        //    var request = new RestRequest(Method.POST);
        //    request.AddHeader("content-type", "application/json");
        //    request.AddParameter("application/json",
        //    "{\"client_id\":\"" + _appSetting.AuthorizationClientId +
        //    "\",\"client_secret\":\"" + _appSetting.AuthorizationClientSecret +
        //    "\",\"audience\":\"" + _appSetting.AuthorizationAudience +
        //    "\",\"grant_type\":\"client_credentials\"}", ParameterType.RequestBody);

        //    IRestResponse response = await client.ExecuteAsync(request, new CancellationTokenSource().Token);
        //    dynamic deserializedJson = JsonConvert.DeserializeObject(response.Content);

        //    return (deserializedJson.access_token != null) ? deserializedJson.access_token : null;
        //}

        public async Task<UpdateUserResponse> UpdateUser(string userId, string oldRole, string newRole)
        {
            var roleIds = await RoleIds();
            string accessToken = await _tokenManager.GetAccessToken();
            var client = new RestClient($"{_appSetting.AuthorizationApi}/users/{userId}/roles");

            IRestResponse response;

            if (roleIds != null)
            {
                if (oldRole != null)
                {
                    var ToDelete = roleIds.Where(x => oldRole.Equals(x.name.ToLower()));
                    var DeleteRequest = new RestRequest(userId, Method.DELETE, DataFormat.Json);
                    DeleteRequest.AddHeader("content-type", "application/json");
                    DeleteRequest.AddJsonBody(ToDelete);
                    DeleteRequest.AddHeader("authorization", "Bearer " + accessToken);
                    response = await client.ExecuteAsync(DeleteRequest, new CancellationTokenSource().Token);
                }

                var ToAdd = roleIds
                    .Where(x => newRole.Contains(x.name.ToLower()))
                    .Select(x => x._id)
                    .ToArray();

                //.Select(x => x.Item1.Where(updatedUserRole.Item1.Contains(x.Item2.ToLower())));
                //.Where(x => updatedUserRole.Item1.Contains(x.Item2.ToLower()));
                var PatchRequest = new RestRequest(Method.PATCH);
                PatchRequest.AddHeader("content-type", "application/json");
                PatchRequest.AddHeader("authorization", "Bearer " + accessToken);
                PatchRequest.AddJsonBody(ToAdd);
                response = await client.ExecuteAsync(PatchRequest, new CancellationTokenSource().Token);

                var UpdateUserResponse = (response.StatusCode == HttpStatusCode.NoContent) ?
                  new UpdateUserResponse("User Updated to " + newRole, response.IsSuccessful) : //success
                 new UpdateUserResponse(response.Content, response.IsSuccessful);
                return UpdateUserResponse;
            }
            return new UpdateUserResponse(new List<string> { "Not Authorized" }, false);
            //.Where(x => updatedUserRole.Contains(x.name.ToLower()))
        }


        private async Task<List<Auth0RoleList.Role>> RoleIds()
        {
            string accessToken = await _tokenManager.GetAccessToken();
            var client = new RestClient(_appSetting.AuthorizationApi);
            var request = new RestRequest("roles", Method.GET);
            request.AddHeader("authorization", "Bearer " + accessToken);
            var response = await client.ExecuteAsync(request, new CancellationTokenSource().Token);

            var deserializedJson = JsonConvert.DeserializeObject<Auth0RoleList>(response.Content);

            return deserializedJson.roles
                .Select(x => x)
                .ToList();

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
