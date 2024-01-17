using Entities.Models;
using Entities.Dtos;
using Repositories.Contracts;
using Services.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;

namespace Services
{
    public class CategoryManager : ICategoryService
    {
        private readonly IRepositoryManager _manager;
        private readonly IMapper _mapper; //automapper kullnmak için enjekte edilir

        public CategoryManager(IRepositoryManager manager, IMapper mapper)
        {
            _manager = manager;
            _mapper = mapper;
        }

        public void CreateCategory(CategoryDtoForInsertion categoryDto)
        {
            /*Category category = new Category()
            {
                CategoryName = categoryDto.CategoryName

            };*/ 
            // bu kısmı Automapper ile yapalım:

            Category category = _mapper.Map<Category>(categoryDto);
            _manager.Category.Create(category);
            _manager.Save();
        }

        public void DeleteOneCategory(int id)
        {
            Category category = GetOneCategory(id, false); // ?? new Category();

            if (category is not null) {
                _manager.Category.DeleteOneCategory(category);
                _manager.Save(); // bütün işlem yapıldıktan sonra kalıcı olarak db'e yansıtılır
            }
            
        }

        public IEnumerable<Category> GetAllCategories(bool trackChanges)
        {
            // throw new NotImplementedException();
            return _manager.Category.FindAll(trackChanges);
        }

        public Category? GetOneCategory(int id, bool trackChanges)
        {
            var category = _manager.Category.GetOneCategory(id, trackChanges);

            if (category is null)
            {
                throw new Exception("Category not found!");
            }
            else return category;
        }

        public CategoryDtoForUpdate GetOneCategoryForUpdate(int id, bool trackChanges)
        {
            var category = GetOneCategory(id, trackChanges);
            var categoryDto = _mapper.Map<CategoryDtoForUpdate>(category);
            return categoryDto;

        }
        public void UpdateOneCategory(CategoryDtoForUpdate categoryDto)
        {
            /* 
            var entity = _manager.Category.GetOneCategory(categoryDto.CategoryId, true);
            Burada mapping automapper ile yapılabilir
            * //değişiklikleri izleyip kaydedeceğiz
            entity.CategoryName = categoryDto.CategoryName;  */

            var entity = _mapper.Map<Category>(categoryDto); // entity'e yeni referans geleceği için manager izlemez
            _manager.Category.UpdateOneCategory(entity);

            // Repo'ya kadar inip tanımları yapmak gerekiyor. Sadece repobase'de update var.
            // .net core zaten izlediği için objeyi. Serviste çözüldü
            _manager.Save();
        }
    }
}
