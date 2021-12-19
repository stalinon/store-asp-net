using System.ComponentModel.DataAnnotations;

namespace Shop.Models
{
    /// <summary>
    ///     Модель категории
    /// </summary>
    public class ApplicationType
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

    }
}
