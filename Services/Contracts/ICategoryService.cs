using Entities.Models;
using Entities.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.Contracts
{
    public interface ICategoryService
    {
        IEnumerable<Category> GetAllCategories(bool trackChanges);

        void CreateCategory(CategoryDtoForInsertion categoryDto);

        Category? GetOneCategory(int id, bool trackChanges);

        CategoryDtoForUpdate GetOneCategoryForUpdate(int id, bool trackChanges);
        void UpdateOneCategory(CategoryDtoForUpdate categoryDto);
        void DeleteOneCategory(int id);
    }
}
