using Microsoft.AspNetCore.Hosting;
using Shop_DataAccess.Data;
using Shop_DataAccess.Repository.IRepository;
using Shop_Models;
using Shop_Utility;
using System.IO;
using System.Linq;

namespace Shop_DataAccess.Repository
{
    public class CategoryRepository : Repository<Category>, ICategoryRepository
    {
        private readonly ApplicationDbContext _db;

        private readonly IWebHostEnvironment _webHostEnvironment;

        public CategoryRepository(ApplicationDbContext db, IWebHostEnvironment webHostEnvironment) : base(db)
        {
            _db = db;
            _webHostEnvironment = webHostEnvironment;
        }

        public override void Remove(Category category)
        {
            foreach (var prod in _db.Product)
            {
                if (prod != null && prod.CategoryId == category.Id)
                {
                    string webRootPath = _webHostEnvironment.WebRootPath;
                    string upload = webRootPath + WC.ImagePath;

                    var file = Path.Combine(upload, prod.Image);

                    if (System.IO.File.Exists(file))
                    {
                        System.IO.File.Delete(file);
                    }

                    _db.Product.Remove(prod);
                }
            }
            _db.Remove(category);
        }

        public void Update(Category category)
        {
            var objFRomDb = base.FirstOrDefault(u => u.Id == category.Id);
            if (objFRomDb != null)
            {
                objFRomDb.Name = category.Name;
                objFRomDb.DisplayOrder = category.DisplayOrder;
            }
        }
    }
}
