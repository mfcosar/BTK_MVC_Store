using Microsoft.AspNetCore.Mvc;
using Repositories;
using Services.Contracts;

namespace StoreApp.Components
{
    public class ProductSummaryViewComponent: ViewComponent
    {

        private IServiceManager _manager;

        public ProductSummaryViewComponent(IServiceManager manager)
        {
            _manager = manager;
        }

        /*private readonly RepositoryContext _repositoryContext;
         * Bu sakıncalı kullanım!

        public ProductSummary(RepositoryContext repositoryContext)
        {
            _repositoryContext = repositoryContext;
        }*/

        public string Invoke()
        {
            //return _repositoryContext.Products.Count().ToString();
            return _manager.ProductService.GetAllProducts(false).Count().ToString();
        }
    }
}
