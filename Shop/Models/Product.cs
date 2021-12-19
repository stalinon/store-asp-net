using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Shop.Models
{
    /// <summary>
    ///     Класс, описывающий продукт
    /// </summary>
    public class Product
    {
        [Key]
        /// <summary>
        ///     Идентификатор продукта
        /// </summary>
        public int Id { get; set; }

        [Required]
        /// <summary>
        ///     Название продукта
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        ///     Краткое описание продукта
        /// </summary>
        public string ShortDesc { get; set; } 

        /// <summary>
        ///     Описание продукта
        /// </summary>
        public string Description { get; set; } 

        [Range(1, int.MaxValue)]
        /// <summary>
        ///     Цена продукта
        /// </summary>
        public double Price { get; set; }

        /// <summary>
        ///     Ссылка на изображение
        /// </summary>
        public string Image { get; set; }

        [Display(Name = "Category Type")]
        /// <summary>
        ///     Идентификатор категории для обеспечения связи
        ///     с категорией
        /// </summary>
        public int CategoryId { get; set; }

        [ForeignKey("CategoryId")]
        /// <summary>
        ///     Ключ категории, к которой относится продукт
        /// </summary>
        public virtual Category Category { get; set; }

        [Display(Name = "Application Type")]
        /// <summary>
        ///     Идентификатор категории для обеспечения связи
        ///     с ApplicationType
        /// </summary>
        public int ApplicationTypeId { get; set; }

        [ForeignKey("ApplicationTypeId")]
        /// <summary>
        ///     Ключ ApplicationType, к которому относится продукт
        /// </summary>
        public virtual ApplicationType ApplicationType { get; set; }

    }
}
