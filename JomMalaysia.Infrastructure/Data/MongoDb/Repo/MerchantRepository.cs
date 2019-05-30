using JomMalaysia.Core.Entities;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using JomMalaysia.Core.Interfaces.Repo;
using JomMalaysia.Core.Domain.Entities.Merchants;

namespace JomMalaysia.Infrastructure.Data.MongoDb.Repo
{
    class MerchantRepository : IReadMerchantRepository, IWriteMerchantRepository
    {
        private readonly Context _context;

        public MerchantRepository(Context context)
        {
            _context = context;
        }

        public async Task<Merchant> Get(Guid id)
        {
            Entities.Merchant merchant = await _context.Merchants
                .Find(e => e.SsmId == id)
                .SingleOrDefaultAsync();

            List<Guid> listings = await _context.Listings
                .Find(e => e.Merchant.SsmId == id)
                .Project(p=>p.Id)
                .ToListAsync();


            ListingCollection listingCollection = new ListingCollection();
            foreach (var listingId in listings)
            {
                listingCollection.Add(listingId);
            }
            return Merchant.Load(merchant.SsmId, merchant.Name, listingCollection);
        }

        public Task Add()
        {
            throw new NotImplementedException();
        }

        public Task Delete()
        {
            throw new NotImplementedException();
        }

        

        public Task Update()
        {
            throw new NotImplementedException();
        }
    }
}
