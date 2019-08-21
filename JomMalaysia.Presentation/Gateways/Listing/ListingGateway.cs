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

namespace JomMalaysia.Presentation.Gateways.Listing
{
    public class ListingGateway : IListingGateway
    {
        private readonly IWebServiceExecutor _webServiceExecutor;
        private readonly IAuthorizationManagers _authorizationManagers;
        private readonly IApiBuilder _apiBuilder;
        private readonly IMapper _mapper;
        private readonly string auth;

        public ListingGateway(IWebServiceExecutor webServiceExecutor, IAuthorizationManagers authorizationManagers, IApiBuilder apiBuilder, IMapper mapper)
        {
            _webServiceExecutor = webServiceExecutor;
            _authorizationManagers = authorizationManagers;
            _apiBuilder = apiBuilder;
            _mapper = mapper;
            auth = _authorizationManagers.accessToken;
        }

        public async Task<IWebServiceResponse> CreateListing(ListingViewModel vm)
        {
            IWebServiceResponse<ListingViewModel> response;
            try
            {
                var req = _apiBuilder.GetApi((APIConstant.API.Path.Listing));

                var method = Method.GET;
                response = await _webServiceExecutor.ExecuteRequestAsync<ListingViewModel>(req, method, auth);
            }
            catch (GatewayException ex)
            {
                throw ex;
            }
            return response;


            //handle exception

        }

        public async Task<List<ListingViewModel>> GetListings()
        {
            List<ListingViewModel> result = new List<ListingViewModel>();
            IWebServiceResponse<ListingListViewModel> response = default;

            try
            {
                var req = _apiBuilder.GetApi((APIConstant.API.Path.Listing));
                var method = Method.GET;
                response = await _webServiceExecutor.ExecuteRequestAsync<ListingListViewModel>(req, method, auth);

            }
            catch (GatewayException ex)
            {
                throw ex;
            }
            if (response.StatusCode == HttpStatusCode.OK)
            {
                var listings = response.Data.Listings;
                foreach (var list in listings)
                {
                    result.Add(list);
                }


            }
            //handle exception
            return result;
        }
    }
}
