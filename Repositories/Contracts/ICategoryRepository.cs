﻿using Entities.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repositories.Contracts
{
    public interface ICategoryRepository: IRepositoryBase<Category>
    {
        /*    IQueryable<Product> GetAllProducts(bool trackChanges);
            public Product? GetOneProduct(int id, bool trackChanges);*/

        void CreateOneCategory(Category category);
        void DeleteOneCategory(Category category);
        Category? GetOneCategory(int id, bool trackChanges);
        void UpdateOneCategory(Category entity);
    }
}
