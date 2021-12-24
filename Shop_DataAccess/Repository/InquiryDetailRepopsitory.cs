using Shop_DataAccess.Data;
using Shop_DataAccess.Repository.IRepository;
using Shop_Models;

namespace Shop_DataAccess.Repository
{
    public class InquiryDetailRepository : Repository<InquiryDetail>, IInquiryDetailRepository
    {
        private readonly ApplicationDbContext _db;

        public InquiryDetailRepository(ApplicationDbContext db) : base(db)
        {
            _db = db;
        }

        public void Update(InquiryDetail inquiryDetail)
        {
            _db.InquiryDetail.Update(inquiryDetail);
        }
    }
}
