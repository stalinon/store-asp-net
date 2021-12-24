using System.Collections.Generic;

namespace Shop_Models.ViewModels
{
    public class InquiryVM
    {
        public InquiryHeader InquiryHeader { get; set; }

        public IEnumerable<InquiryDetail> InquiryDetail { get; set; }

    }
}
