using Microsoft.AspNetCore.Hosting;
using Shop_DataAccess.Data;
using Shop_DataAccess.Repository.IRepository;
using Shop_Models;
using Shop_Utility;
using System.IO;
using System.Linq;

namespace Shop_DataAccess.Repository
{
    public class ApplicationTypeRepository : Repository<ApplicationType>, IApplicationTypeRepository
    {
        private readonly ApplicationDbContext _db;

        private readonly IWebHostEnvironment _webHostEnvironment;

        public ApplicationTypeRepository(ApplicationDbContext db, IWebHostEnvironment webHostEnvironment) : base(db)
        {
            _db = db;
            _webHostEnvironment = webHostEnvironment;
        }

        public override void Remove(ApplicationType applicationType)
        {
            foreach (var prod in _db.Product)
            {
                if (prod != null && prod.CategoryId == applicationType.Id)
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
            _db.Remove(applicationType);
        }

        public void Update(ApplicationType applicationType)
        {
            var objFRomDb = base.FirstOrDefault(u => u.Id == applicationType.Id);
            if (objFRomDb != null)
            {
                objFRomDb.Name = applicationType.Name;
            }
        }
    }
}
