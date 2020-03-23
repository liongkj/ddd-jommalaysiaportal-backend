using System.Collections.Generic;
using System.Threading.Tasks;
using Algolia.Search.Models.Common;
using AutoMapper;
using JomMalaysia.Core.Indexes;
using JomMalaysia.Core.Interfaces;
using JomMalaysia.Core.UseCases.ListingUseCase.Get;
using JomMalaysia.Infrastructure.Algolia.Entities;

namespace JomMalaysia.Infrastructure.Algolia.Indexers
{
    public class ListingIndex:IIndex<ListingViewModel>
    {
        private readonly IAlgoliaClient _indexClient;
        private readonly IMapper _mapper;
        public ListingIndex( IAlgoliaClient indexClient, IMapper mapper)
        {
            _indexClient = indexClient;
            _mapper = mapper;
        }
        
        public async Task<BatchIndexingResponse> SaveObject(IEnumerable<ListingViewModel> items)
        {
            var list = _mapper.Map<List<ListingIndexDto>>(items);
          return await _indexClient.Index.SaveObjectsAsync(list);
        }
    }

    
}