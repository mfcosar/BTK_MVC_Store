using Entities.Models;
using Repositories.Contracts;
using Services.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services
{
    public class CategoryManager : ICategoryService
    {
        private readonly IRepositoryManager _manager;

        public CategoryManager(IRepositoryManager manager)
        {
            _manager = manager;
        }

        public void CreateCategory(Category category)
        {
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

        public void UpdateOneCategory(Category category)
        {
            var entity = _manager.Category.GetOneCategory(category.CategoryId, true);
            //değişiklikleri izleyip kaydedeceğiz
            entity.CategoryName = category.CategoryName;  
            // Repo'ya kadar inip tanımları yapmak gerekiyor. Sadece repobase'de update var.
            // .net core zaten izlediği için objeyi. Serviste çözüldü
            _manager.Save();
        }
    }
}
