using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using JomMalaysia.Core.Domain.Entities;
using JomMalaysia.Core.Domain.ValueObjects;
using JomMalaysia.Core.Exceptions;
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
        public async Task<CreateCategoryResponse> CreateCategoryAsync(Category Category)
        {

            try
            {
                var CategoryDto = _mapper.Map<CategoryDto>(Category);
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

        public async Task<DeleteCategoryResponse> DeleteAsync(string id)
        {
            DeleteResult result;
            //mongodb driver api
            try
            {
                result = await _db.DeleteOneAsync(filter: c => c.Id == id);
            }
            catch (Exception e)
            {
                return new DeleteCategoryResponse(new List<string> { "delete repo error" }, false, e.ToString());
            }
            //todo TBC soft delete or hard delete
            return new DeleteCategoryResponse(id, result.DeletedCount == 1);
        }



        public async Task<GetCategoryResponse> FindByIdAsync(string CategoryId)
        {
            //linq to search with criteria
            Category m;
            try
            {
                //linq query
                var query = await
                    _db.AsQueryable()
                    .Where(M => M.Id.Equals(CategoryId))
                    .FirstOrDefaultAsync();
                if (query == null) throw new NotFoundException(CategoryId);
                m = _mapper.Map<Category>(query);
            }
            catch (AutoMapperMappingException e)
            {
                throw e;
            }
            catch (NotFoundException e)
            {
                throw e;
            }
            catch (Exception e)
            {
                return new GetCategoryResponse(new List<string> { "FindByIdAsync repo error" }, false, e.ToString());
            }

            var response = new GetCategoryResponse(m, true);
            return response;
        }



        /// <summary>
        /// Get category object given category name
        /// </summary>
        /// <param name="name"></param>
        /// <returns></returns>
        public async Task<GetCategoryResponse> FindByNameAsync(string name)
        {
            Category m;
            try
            {
                CategoryPath cp = new CategoryPath(name, null);
                //convert to slug
                var querystring = cp.ToString();
                //linq query
                var query = await
                    _db.AsQueryable()
                    .Where(M => M.CategoryPath.Equals(querystring))
                    .FirstOrDefaultAsync();
                m = _mapper.Map<Category>(query);
            }
            catch (AutoMapperMappingException e)
            {
                throw e;
            }
            catch (Exception e)
            {
                return new GetCategoryResponse(new List<string> { "FindByNameAsync repo error" }, false, e.ToString());
            }
            var response = m == null ? new GetCategoryResponse(new List<string> { "Category Not Found" }, false) : new GetCategoryResponse(m, true);
            return response;
        }

        /// <summary>
        /// Get subcategory given category and subcategory name
        /// </summary>
        /// <param name="name"></param>
        /// <returns></returns>
        public async Task<GetCategoryResponse> FindByNameAsync(string cat, string sub)
        {
            Category m;
            try
            {
                CategoryPath cp = new CategoryPath(cat, sub);
                //convert to slug
                var querystring = cp.ToString();
                //linq query
                var query = await
                    _db.AsQueryable()
                    .Where(M => M.CategoryPath.Equals(querystring))
                    .FirstOrDefaultAsync()
                    .ConfigureAwait(false);
                m = _mapper.Map<Category>(query);
            }
            catch (AutoMapperMappingException e)
            {
                throw e;
            }
            catch (Exception e)
            {
                return new GetCategoryResponse(new List<string> { "FindByName Repo Error" }, false, e.ToString());
            }

            var response = m == null ?
                new GetCategoryResponse(new List<string> { "Subcategory Not Found" }, false, "Subcategory Not Found")
                : new GetCategoryResponse(m, true);
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

            return new GetAllCategoryResponse(Categories, true, $"{Categories.Count} result found");

        }


        /// <summary>
        /// Get all subcategories given a category name
        /// </summary>
        /// <param name="CategoryName"></param>
        /// <returns></returns>
        //overload to get subcategories
        public async Task<GetAllCategoryResponse> GetAllCategoriesAsync(string CategoryName)
        {
            List<Category> Categories;
            try
            {
                CategoryPath cp = new CategoryPath(CategoryName, null);
                //convert to slug
                var querystring = cp.ToString();

                var query = await
                        _db.AsQueryable()
                    .Where(M => M.CategoryPath.StartsWith(querystring))
                    //.Where(M => !M.CategoryPath.Equals(querystring)) //include category
                    .OrderBy(c => c.CategoryName)
                    .ToListAsync();

                Categories = _mapper.Map<List<Category>>(query);
            }
            catch (Exception e)
            {
                throw e;
            }
            var response = Categories.Count < 1 ?
                new GetAllCategoryResponse(new List<string> { "No Categories found" }, false) :
                new GetAllCategoryResponse(Categories, true, $"{Categories.Count} result found");
            return response;
        }


        public async Task<UpdateCategoryResponse> UpdateCategoryWithSession(string id, Category updatedCategory, IClientSessionHandle session)
        {

            ReplaceOneResult result = await _db.ReplaceOneAsync(session, Category => Category.Id == id, _mapper.Map<CategoryDto>(updatedCategory));
            var response = result.ModifiedCount != 0 ? new UpdateCategoryResponse(id, true, "Category Updated Successfully")
                : new UpdateCategoryResponse(new List<string>() { "update Category failed" }, false);
            return response;
        }

        public async Task<UpdateCategoryResponse> UpdateManyWithSession(List<Category> categories, IClientSessionHandle session)
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

            var response = await _db.BulkWriteAsync(session, bulkOps);

            return new UpdateCategoryResponse("update many operation",
                response.IsAcknowledged,
                response.ModifiedCount + " updated");
        }
    }
}