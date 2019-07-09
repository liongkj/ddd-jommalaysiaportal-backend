using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using JomMalaysia.Api.Serialization;
using JomMalaysia.Core.Interfaces;

namespace JomMalaysia.Api.UseCases.Categories
{
    public sealed class CategoryPresenter:IOutputPort<UseCaseResponseMessage>
    {
       
            public JsonContentResult ContentResult { get; }

            public CategoryPresenter()
            {
                ContentResult = new JsonContentResult();
            }


            public void Handle(UseCaseResponseMessage response)
            {
                ContentResult.StatusCode = (int)(response.Success ? HttpStatusCode.OK : HttpStatusCode.BadRequest);
                ContentResult.Content = JsonSerializer.SerializeObject(response);
            }
        }
    }

