using Microsoft.AspNetCore.Mvc.Rendering;
using Shop_Models;
using System.Collections.Generic;

namespace Shop_DataAccess.Repository.IRepository
{
    public interface IProductRepository : IRepository<Product>
    {
        void Update(Product product);

        IEnumerable<SelectListItem> GetSelectListItems(string obj);
    }
}
