using Microsoft.AspNetCore.Identity;

namespace Shop_Models
{
    public class ApplicationUser : IdentityUser
    {
        public string FullName { get; set; }
    }
}
