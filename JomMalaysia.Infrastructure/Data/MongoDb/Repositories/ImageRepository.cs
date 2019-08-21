using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Threading.Tasks;
using JomMalaysia.Core.Interfaces;
using JomMalaysia.Core.Interfaces.Repositories;
using JomMalaysia.Core.Services.ImageProcessingServices;
using JomMalaysia.Infrastructure.Data.MongoDb.Entities;
using MongoDB.Driver;

namespace JomMalaysia.Infrastructure.Data.MongoDb.Repositories
{
    public class ImageRepository : IImageRepository
    {
        private readonly IMongoCollection<ImageDto> _db;
        public ImageRepository(IMongoDbContext context)
        {
            _db = context.Database.GetCollection<ImageDto>("Image");
        }
        public async Task<ImageProcessorResponse> SaveImageAsync(byte[] stream)
        {
            ImageDto dto = new ImageDto()
            {
                BinaryData = stream
            };
            await _db.InsertOneAsync(dto).ConfigureAwait(false);
            return new ImageProcessorResponse("1", true);
        }

        public async Task<ImageProcessorResponse> GetImageAsync(string ListingId)
        {
            ImageDto dto = new ImageDto()
            {

            };
            await _db.InsertOneAsync(dto).ConfigureAwait(false);
            return new ImageProcessorResponse("1", true);
        }
    }
}
