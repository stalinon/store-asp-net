using Microsoft.AspNetCore.Mvc.Rendering;
using Shop_DataAccess.Data;
using Shop_DataAccess.Repository.IRepository;
using Shop_Models;
using Shop_Utility;
using System.Collections.Generic;
using System.Linq;

namespace Shop_DataAccess.Repository
{
    public class ProductRepository : Repository<Product>, IProductRepository
    {
        private readonly ApplicationDbContext _db;

        public ProductRepository(ApplicationDbContext db) : base(db)
        {
            _db = db;
        }

        public IEnumerable<SelectListItem> GetSelectListItems(string obj)
        {
            if (obj == WC.CategoryName)
            {
                return _db.Category.Select(u => new SelectListItem
                {
                    Text = u.Name,
                    Value = u.Id.ToString()
                });
            }
            else if (obj == WC.ApplicationTypeName)
            {
                return _db.ApplicationType.Select(u => new SelectListItem
                {
                    Text = u.Name,
                    Value = u.Id.ToString()
                });
            }
            return default(IEnumerable<SelectListItem>);
        }

        public void Update(Product product)
        {
            _db.Product.Update(product);
        }
    }
}
