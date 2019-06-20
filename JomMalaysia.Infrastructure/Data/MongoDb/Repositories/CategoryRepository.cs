using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using JomMalaysia.Core.Domain.Entities;
using JomMalaysia.Core.Interfaces;
using JomMalaysia.Core.Interfaces.Repositories;
using JomMalaysia.Core.Services.Categories.UseCaseResponses;
using JomMalaysia.Infrastructure.Data.MongoDb.Entities;
using MongoDB.Bson;
using MongoDB.Driver;

namespace JomMalaysia.Infrastructure.Data.MongoDb.Repositories
{
    public class CategoryRepository : ICategoryRepository
    {
        private readonly IMongoCollection<CategoryDto> _db;
        public readonly IMapper _mapper;

        public CategoryRepository(IApplicationDbContext settings, IMapper mapper)
        {
            var client = new MongoClient(settings.ConnectionString);
            var database = client.GetDatabase(settings.DatabaseName);

            _db = database.GetCollection<CategoryDto>("Category");
            _mapper = mapper;
        }

        public CreateCategoryResponse CreateCategory(Category Category)
        {
            CategoryDto NewCategory = _mapper.Map<Category, CategoryDto>(Category);
            try
            {
                //TODO
                _db.InsertOne(NewCategory);
                return new CreateCategoryResponse(NewCategory.Id, true);
            }
            catch (MongoWriteException e)
            {
                return new CreateCategoryResponse(e.ToString());
            }
            catch (MongoWriteConcernException e)
            {
                return new CreateCategoryResponse(e.ToString());
            }
            catch (MongoException e)
            {
                return new CreateCategoryResponse(e.ToString());
            }
        }

        public DeleteCategoryResponse Delete(string id)
        {
            try
            {
                _db.DeleteOne(c => c.Id == id);
                //TODO
                //Soft Delete
            }
            catch (Exception ex)
            {
                return new DeleteCategoryResponse((IEnumerable<string>)ex, false, "mongodb error: Category delete failed");
            }
            return new DeleteCategoryResponse(id, true, "Category deleted successfully");
        }


        public GetCategoryResponse FindById(string id)
        {
            var filter = Builders<CategoryDto>.Filter.Eq(cd => cd.Id, id);
            var result = _db.Find(filter).SingleOrDefault();
       
            var found = _mapper.Map<CategoryDto, Category>(result);

            return new GetCategoryResponse(found, true, found.CategoryName + "Found by id");
        }

        public async Task<GetAllCategoryResponse> GetAllCategories()
        {
            var filter = Builders<CategoryDto>.Filter.Empty;
            var result = await _db.Find(filter).ToListAsync();
            var categories = _mapper.Map<List<CategoryDto>, List<Category>>(result);

            return new GetAllCategoryResponse(categories, true);

        }

        public GetCategoryResponse FindByName(string name)
        {
            throw new NotImplementedException();
        }

        

        public UpdateCategoryResponse Update(string id, Category Category)
        {
            throw new NotImplementedException();
        }

        public CreateSubcategoryResponse CreateSubcategory(string categoryId, Subcategory subcategory)
        {
            SubcategoryDto sub = _mapper.Map< Subcategory, SubcategoryDto>(subcategory);
            var filter = Builders<CategoryDto>.Filter.Eq(cd => cd.Id, categoryId);
            var update = Builders<CategoryDto>.Update.AddToSet(cd=>cd.Subcategories, sub);
            var result = _db.UpdateOne(filter, update);

            if (result.ModifiedCount == 1)
                return new CreateSubcategoryResponse(result.ToString(), true);
            else return new CreateSubcategoryResponse(result.ToString(), false);


        }
    }
}
