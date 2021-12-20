using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace Shop_Models
{
    /// <summary>
    ///     Модель категории
    /// </summary>
    public class Category
    {
        /// <summary>
        ///     Идентификатор категории
        /// </summary>
        [Key]
        public int Id { get; set; }

        /// <summary>
        ///     Название категории
        /// </summary>
        [Required]
        public string Name { get; set; }

        /// <summary>
        ///     
        /// </summary>
        [DisplayName("Display Order")]
        [Required]
        [Range(1,int.MaxValue,ErrorMessage = "Display Order for category must be greater than 0")]
        public int DisplayOrder { get; set; }
    }
}
