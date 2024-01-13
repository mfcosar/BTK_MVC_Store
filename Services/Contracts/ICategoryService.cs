using Entities.Models;
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

        void CreateCategory(Category category);

        Category? GetOneCategory(int id, bool trackChanges);
        void UpdateOneCategory(Category category);
    }
}
