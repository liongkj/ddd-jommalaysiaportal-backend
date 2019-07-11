using System.Collections.Generic;
using System.Threading.Tasks;
using AutoMapper;
using JomMalaysia.Core.Domain.Entities;
using JomMalaysia.Core.Interfaces;
using JomMalaysia.Core.Interfaces.Repositories;
using JomMalaysia.Core.UseCases.CatogoryUseCase.Create;
using JomMalaysia.Core.UseCases.CatogoryUseCase.Delete;
using JomMalaysia.Core.UseCases.CatogoryUseCase.Get;
using JomMalaysia.Core.UseCases.CatogoryUseCase.Update;
using JomMalaysia.Infrastructure.Data.MongoDb.Entities;
using MongoDB.Driver;
using MongoDB.Driver.Linq;

namespace JomMalaysia.Infrastructure.Data.MongoDb.Repositories
{
    public class CategoryRepository : ICategoryRepository
    {
        private readonly IMongoCollection<CategoryDto> _db;
        private readonly IMapper _mapper;
        public CategoryRepository(IMongoDbContext context, IMapper mapper)
        {
            _db = context.Database.GetCollection<CategoryDto>("Category");
            
            _mapper = mapper;
        }
        public async Task<CreateCategoryResponse> CreateCategory(Category Category)
        {
            var CategoryDto = _mapper.Map<Category, CategoryDto>(Category);
            
            await _db.InsertOneAsync(CategoryDto);
            return new CreateCategoryResponse(Category.CategoryId, true);
        }

        public DeleteCategoryResponse Delete(string id)
        {
            throw new System.NotImplementedException();
        }

        public DeleteCategoryResponse DeleteCategory(string CategoryId)
        {
            //mongodb driver api
            var result = _db.DeleteOne(filter: m => m.Id == CategoryId);
            //todo TBC soft delete or hard delete
            return new DeleteCategoryResponse(CategoryId, true);
        }

        public GetCategoryResponse FindById(string CategoryId)
        {
            //linq to search with criteria
            var query =
                  _db.AsQueryable()
                  .Where(M => M.Id == CategoryId)
                  .Select(M => M)
                  .FirstOrDefault();
            Category m = _mapper.Map<Category>(query);
            var response = m == null ? new GetCategoryResponse(new List<string> { "Category Not Found" }, false) : new GetCategoryResponse(m, true);
            return response;
        }

        public GetCategoryResponse FindByName(string name)
        {
            throw new System.NotImplementedException();
        }

        public GetAllCategoryResponse GetAllCategories()
        {
            var query =
                 _db.AsQueryable()
                 .ToList();
            List<Category> Categorys = _mapper.Map<List<Category>>(query);
            var response = Categorys.Count < 1 ?
                new GetAllCategoryResponse(new List<string> { "No Categorys" }, false) :
                new GetAllCategoryResponse(Categorys, true);
            return response;
        }



        public UpdateCategoryResponse Update(string id, Category Category)
        {
            throw new System.NotImplementedException();
        }

        public UpdateCategoryResponse UpdateCategory(string id, Category updatedCategory)
        {
            ReplaceOneResult result = _db.ReplaceOne(Category => Category.Id == id, _mapper.Map<CategoryDto>(updatedCategory));
            var response = result.ModifiedCount != 0 ? new UpdateCategoryResponse(id, true)
                : new UpdateCategoryResponse(new List<string>() { "update Category failed" }, false);
            return response;
        }
    }
}