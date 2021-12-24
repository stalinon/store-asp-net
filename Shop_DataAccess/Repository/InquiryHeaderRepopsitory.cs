using Shop_DataAccess.Data;
using Shop_DataAccess.Repository.IRepository;
using Shop_Models;

namespace Shop_DataAccess.Repository
{
    public class InquiryHeaderRepository : Repository<InquiryHeader>, IInquiryHeaderRepository
    {
        private readonly ApplicationDbContext _db;

        public InquiryHeaderRepository(ApplicationDbContext db) : base(db)
        {
            _db = db;
        }

        public void Update(InquiryHeader inquiryHeader)
        {
            _db.InquiryHeader.Update(inquiryHeader);
        }
    }
}
