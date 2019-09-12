using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using AutoMapper;
using JomMalaysia.Core.Domain.Entities;
using JomMalaysia.Core.Domain.ValueObjects;
using JomMalaysia.Core.Interfaces;
using JomMalaysia.Core.Interfaces.Repositories;
using JomMalaysia.Core.UseCases.CatogoryUseCase.Create;
using JomMalaysia.Core.UseCases.CatogoryUseCase.Delete;
using JomMalaysia.Core.UseCases.CatogoryUseCase.Get;
using JomMalaysia.Core.UseCases.CatogoryUseCase.Update;
using JomMalaysia.Infrastructure.Data.MongoDb.Entities;
using MongoDB.Bson;
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
        public async Task<CreateCategoryResponse> CreateCategoryAsync(Category Category)
        {

            try
            {
                var CategoryDto = _mapper.Map<Category, CategoryDto>(Category);
                await _db.InsertOneAsync(CategoryDto).ConfigureAwait(false);
            }
            catch (AutoMapperMappingException e)
            {
                return new CreateCategoryResponse(new List<string> { "Automapper Error" }, false, e.ToString());
            }
            catch (Exception e)
            {
                return new CreateCategoryResponse(new List<string> { "Other Errors" }, false, e.ToString());
            }

            return new CreateCategoryResponse(Category.CategoryId, true, "Category Created");
        }

        public DeleteCategoryResponse Delete(string id)
        {
            //TODO Profile speed
            //FilterDefinition<PlaceDto> filter = Builders<PlaceDto>.Filter.Eq(m => m.Id, id);
            //DeleteResult deleteResult = await _db
            //                                  .DeleteOneAsync(filter);
            //var query = _db.AsQueryable()
            //.Select(p => p.Id);
            //return deleteResult.IsAcknowledged
            //    && deleteResult.DeletedCount > 0;

            //mongodb driver api
            var result = _db.DeleteOne(filter: c => c.Id == id);
            //todo TBC soft delete or hard delete
            return new DeleteCategoryResponse(id, result.IsAcknowledged);
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

        public GetCategoryResponse GetCategory(string name)
        {
            CategoryPath cp = new CategoryPath(name, null);
            //convert to slug
            var querystring = cp.ToString();
            //linq query
            var query =
                _db.AsQueryable()
                .Where(M => M.CategoryPath.StartsWith(querystring))
                .ToList();
            List<Category> c = _mapper.Map<List<Category>>(query);
            var response = c == null ? new GetCategoryResponse(new List<string> { "Category Not Found" }, false) : new GetCategoryResponse(c, true);
            return response;
        }


        /// <summary>
        /// Get category object given category name
        /// </summary>
        /// <param name="name"></param>
        /// <returns></returns>
        public GetCategoryResponse FindByName(string name)
        {
            CategoryPath cp = new CategoryPath(name, null);
            //convert to slug
            var querystring = cp.ToString();
            //linq query
            var query =
                _db.AsQueryable()
                .Where(M => M.CategoryPath.Equals(querystring))
                .FirstOrDefault();
            Category m = _mapper.Map<Category>(query);
            var response = m == null ? new GetCategoryResponse(new List<string> { "Category Not Found" }, false) : new GetCategoryResponse(m, true);
            return response;
        }

        /// <summary>
        /// Get subcategory given category and subcategory name
        /// </summary>
        /// <param name="name"></param>
        /// <returns></returns>
        public GetCategoryResponse FindByName(string cat, string sub)
        {
            CategoryPath cp = new CategoryPath(cat, sub);
            //convert to slug
            var querystring = cp.ToString();
            //linq query
            var query =
                _db.AsQueryable()
                .Where(M => M.CategoryPath.Equals(querystring))
                .FirstOrDefault();
            Category m = _mapper.Map<Category>(query);
            var response = m == null ? new GetCategoryResponse(new List<string> { "Subcategory Not Found" }, false) : new GetCategoryResponse(m, true);
            return response;
        }

        public async Task<GetAllCategoryResponse> GetAllCategoriesAsync(int PageSize = 20, int PageNumber = 1)
        {
            //TODO pagination
            List<Category> Categories;
            try
            {
                var query = await
                 _db.AsQueryable()
                 .ToListAsync()
                 .ConfigureAwait(false)
                 ;
                Categories = _mapper.Map<List<Category>>(query);
            }
            catch (AutoMapperMappingException e)
            {
                throw e;
            }
            catch (Exception e)
            {
                return new GetAllCategoryResponse(new List<string> { e.ToString() });
            }
            if (Categories.Count > 0)
                return new GetAllCategoryResponse(Categories, true);
            return new GetAllCategoryResponse(new List<string> { "No Categories Found" });

        }


        /// <summary>
        /// Get all subcategories given a category name
        /// </summary>
        /// <param name="CategoryName"></param>
        /// <returns></returns>
        //overload to get subcategories
        public GetAllCategoryResponse GetAllCategories(string CategoryName)
        {
            CategoryPath cp = new CategoryPath(CategoryName, null);
            //convert to slug
            var querystring = cp.ToString();

            var query =
                    _db.AsQueryable()
                .Where(M => M.CategoryPath.StartsWith(querystring))
                .Where(M => !M.CategoryPath.Equals(querystring))
                //.Where(m=>!(m.CategoryPath.str).Equals(querystring.Length))
                .OrderBy(c => c.CategoryName)
                .ToList();

            List<Category> Categories = _mapper.Map<List<Category>>(query);
            var response = Categories.Count < 1 ?
                new GetAllCategoryResponse(new List<string> { "No Subcategories" }, false) :
                new GetAllCategoryResponse(Categories, true);
            return response;
        }


        public UpdateCategoryResponse UpdateCategoryWithSession(string id, Category updatedCategory, IClientSessionHandle session)
        {

            ReplaceOneResult result = _db.ReplaceOne(session, Category => Category.Id == id, _mapper.Map<CategoryDto>(updatedCategory));
            var response = result.ModifiedCount != 0 ? new UpdateCategoryResponse(id, true)
                : new UpdateCategoryResponse(new List<string>() { "update Category failed" }, false);
            return response;
        }

        public UpdateCategoryResponse UpdateManyWithSession(List<Category> categories, IClientSessionHandle session)
        {

            var categoryList = _mapper.Map<List<CategoryDto>>(categories);

            var bulkOps = new List<WriteModel<CategoryDto>>();

            foreach (var record in categoryList)
            {
                var updateOne = new ReplaceOneModel<CategoryDto>(
                    Builders<CategoryDto>.Filter.Where(c => c.Id == record.Id),
                    record);

                bulkOps.Add(updateOne);
            }

            var response = _db.BulkWrite(session, bulkOps);

            return new UpdateCategoryResponse("update many operation",
                response.IsAcknowledged,
                response.ModifiedCount + " updated");
        }
    }
}