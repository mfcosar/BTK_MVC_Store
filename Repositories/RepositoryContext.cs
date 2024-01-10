using Entities.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Repositories.Config;
using System.Reflection;

namespace Repositories
{

    public class RepositoryContext : IdentityDbContext<IdentityUser>   //DbContext
    {
        //Bu sınıf artık veritabanı gibi işlem görecek
        public DbSet<Product> Products { get; set; }
        public DbSet<Category> Categories { get; set; }

        public DbSet<Order> Orders { get; set; }



        public RepositoryContext(DbContextOptions<RepositoryContext> options) : base(options)
        {

        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            /* modelBuilder.ApplyConfiguration(new ProductConfig());
            modelBuilder.ApplyConfiguration(new CategoryConfig()); */
            /* Bu yol olabilir ya da bir assembly uygulanabilir */

            modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());

            /*  modelBuilder.Entity<Product>().HasData(
                new Product() { ProductId = 1, CategoryId = 2, ProductName = "Computer", Price = 17000 },
                new Product() { ProductId = 2, CategoryId = 2, ProductName = "Keyboard", Price = 1000 },
                new Product() { ProductId = 3, CategoryId = 2, ProductName = "Mouse", Price = 500 },
                new Product() { ProductId = 4, CategoryId = 2, ProductName = "Monitor", Price = 7000 },
                new Product() { ProductId = 5, CategoryId = 2, ProductName = "Deck", Price = 1500 },
                new Product() { ProductId = 6, CategoryId = 1, ProductName = "History", Price = 25 },
                new Product() { ProductId = 7, CategoryId = 1, ProductName = "Dedem Korkut", Price = 40 }
                );*/

            /*  modelBuilder.Entity<Category>().HasData(
                new Category() { CategoryId = 1, CategoryName = "Books" },
                new Category() { CategoryId = 2, CategoryName = "Electronic" }); */
        }
    }
}