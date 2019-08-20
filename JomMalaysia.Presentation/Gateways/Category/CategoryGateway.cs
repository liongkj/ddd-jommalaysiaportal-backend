using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using AutoMapper;
using JomMalaysia.Framework.Constant;
using JomMalaysia.Framework.Helper;
using JomMalaysia.Framework.WebServices;
using JomMalaysia.Presentation.Manager;
using JomMalaysia.Presentation.Models;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using RestSharp;

namespace JomMalaysia.Presentation.Gateways.Category
{
    public class CategoryGateway : ICategoryGateway
    {
        private readonly IWebServiceExecutor _webServiceExecutor;
        private readonly IAuthorizationManagers _authorizationManagers;
        private readonly IApiBuilder _apiBuilder;
        private readonly IMapper _mapper;
        private readonly string auth;

        public CategoryGateway(IWebServiceExecutor webServiceExecutor, IAuthorizationManagers authorizationManagers, IApiBuilder apiBuilder, IMapper mapper)
        {
            _webServiceExecutor = webServiceExecutor;
            _authorizationManagers = authorizationManagers;
            _apiBuilder = apiBuilder;
            _mapper = mapper;
            auth = _authorizationManagers.accessToken;
        }

        public async Task<IWebServiceResponse> CreateCategory(CategoryViewModel vm)
        {
            IWebServiceResponse<CategoryViewModel> response;
            try
            {
                var req = _apiBuilder.GetApi((APIConstant.API.Path.Category));

                var method = Method.GET;
                response = await _webServiceExecutor.ExecuteRequestAsync<CategoryViewModel>(req, method, auth);
            }
            catch (GatewayException ex)
            {
                throw ex;
            }
            return response;


            //handle exception

        }

        public async Task<List<CategoryViewModel>> GetCategories()
        {
            List<CategoryViewModel> result = new List<CategoryViewModel>();
            IWebServiceResponse<CategoryListViewModel> response = default;

            try
            {
                var req = _apiBuilder.GetApi((APIConstant.API.Path.Category));
                var method = Method.GET;
                response = await _webServiceExecutor.ExecuteRequestAsync<CategoryListViewModel>(req, method, auth);

            }
            catch (GatewayException ex)
            {
                throw ex;
            }
            if (response.StatusCode == HttpStatusCode.OK)
            {
                var categories = response.Data.Categories;
                foreach (var cat in categories)
                {
                    result.Add(cat);
                }


            }
            //handle exception
            return result;
        }
    }
}
