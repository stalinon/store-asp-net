using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Shop_Models;

namespace Shop_DataAccess.Data
{
    /// <summary>
    ///     Контекст базы данных
    /// </summary>
    public class ApplicationDbContext : IdentityDbContext
    {
        /// <summary>
        ///     Датасет для категорий
        /// </summary>
        public DbSet<Category> Category { get; set; }

        /// <summary>
        ///     Датасет для ApplicationType
        /// </summary>
        public DbSet<ApplicationType> ApplicationType { get; set; }

        /// <summary>
        ///     Датасет для Product
        /// </summary>
        public DbSet<Product> Product { get; set; }

        /// <summary>
        ///     Датасет для ApplicationUser
        /// </summary>
        public DbSet<ApplicationUser> ApplicationUser { get; set; }

        /// <summary>
        ///     Датасет для InquiryHeader
        /// </summary>
        public DbSet<InquiryHeader> InquiryHeader { get; set; }

        /// <summary>
        ///     Датасет для InquiryDetail
        /// </summary>
        public DbSet<InquiryDetail> InquiryDetail { get; set; }

        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options) {}
    }
}
