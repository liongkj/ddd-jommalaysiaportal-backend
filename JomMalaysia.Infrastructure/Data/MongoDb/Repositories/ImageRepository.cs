using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using JomMalaysia.Core.Interfaces;
using JomMalaysia.Core.Interfaces.Repositories;
using JomMalaysia.Core.Services.ImageProcessingServices;
using JomMalaysia.Infrastructure.Data.MongoDb.Entities;
using MongoDB.Driver;
using MongoDB.Driver.Linq;

namespace JomMalaysia.Infrastructure.Data.MongoDb.Repositories
{
    public class ImageRepository : IImageRepository
    {
        private readonly IMongoCollection<ImageDto> _db;
        private readonly IMapper _mapper;
        public ImageRepository(IMongoDbContext context, IMapper mapper)
        {
            _db = context.Database.GetCollection<ImageDto>("Image");
            _mapper = mapper;
        }
        public async Task<ImageProcessorResponse> SaveImageAsync(byte[] stream)
        {
            ImageDto dto = new ImageDto()
            {
                BinaryData = stream
            };
            try
            {
                await _db.InsertOneAsync(dto).ConfigureAwait(false);
            }
            catch (Exception e)
            {
                return new ImageProcessorResponse("Image repo", false, e.Message);
            }
            return new ImageProcessorResponse(dto.Id, true);
        }

        public async Task<ImageLoadedResponse> LoadImageAsync(string id)
        {
            FilterDefinition<ImageDto> filter = Builders<ImageDto>.Filter.Eq(i => i.Id, id);
            try
            {
                var result = await
                     _db.AsQueryable()
                  .Where(M => M.Id == id)
                  .FirstOrDefaultAsync()
                  .ConfigureAwait(false);
                if (result != null)
                {
                    var binary = result.BinaryData;
                    return new ImageLoadedResponse(binary, true);
                }
                return new ImageLoadedResponse(new List<string> { $"Image {id}" }, false, "not found");
            }
            catch (Exception e)
            {
                return new ImageLoadedResponse(new List<string> { "Repository Fetch failed" }, false, $"Error message = {e.Message}");
            }


        }
    }
}
