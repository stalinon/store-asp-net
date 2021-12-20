using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Shop_Models;

namespace Shop.Data
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

        public DbSet<ApplicationUser> ApplicationUser { get; set; }

        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options) {}
    }
}
