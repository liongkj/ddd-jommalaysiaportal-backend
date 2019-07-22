using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using AutoMapper;
using JomMalaysia.Framework.Constant;
using JomMalaysia.Framework.WebServices;
using JomMalaysia.Presentation.Manager;
using RestSharp;

namespace JomMalaysia.Presentation.Gateways.Category
{
    public class CategoryGateway : ICategoryGateway
    {
        private readonly IWebServiceExecutor _webServiceExecutor;
        private readonly IAuthorizationManagers _authorizationManagers;
        private readonly IApiBuilder _apiBuilder;
        private readonly IMapper _mapper;

        public CategoryGateway(IWebServiceExecutor webServiceExecutor, IAuthorizationManagers authorizationManagers, IApiBuilder apiBuilder, IMapper mapper)
        {
            _webServiceExecutor = webServiceExecutor;
            _authorizationManagers = authorizationManagers;
            _apiBuilder = apiBuilder;
            _mapper = mapper;
        }

        public List<CategoryViewModel> GetCategories()
        {
            List<CategoryViewModel> result = new List<CategoryViewModel>(); 
            IWebServiceResponse<CategoryViewModel> response = default;

            try
            {
                var req = _apiBuilder.GetApi((APIConstant.API.Path.GetAllCategory));
                var method = Method.GET;
                _webServiceExecutor.ExecuteRequest<CategoryViewModel>(req, method);
            }
            catch (GatewayException ex)
            {

            }
            if (response.StatusCode == HttpStatusCode.OK)
            {
                result = Mapper.Map<List<CategoryViewModel>>(response.Data);
            }
            //handle exception
            return result;
        }
    }
}
