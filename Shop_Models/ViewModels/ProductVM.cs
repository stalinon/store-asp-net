using Microsoft.AspNetCore.Mvc.Rendering;
using System.Collections.Generic;

namespace Shop_Models.ViewModels
{
    /// <summary>
    ///     ViewModel для класса продукта
    /// </summary>
    public class ProductVM
    {
        /// <summary>
        ///     Объект класса продукта
        /// </summary>
        public Product Product { get; set; }

        /// <summary>
        ///     Список возможных для выбора категорий
        ///     для выпадающего списка
        /// </summary>
        public IEnumerable<SelectListItem> CategorySelectList { get; set; }

        /// <summary>
        ///     Список возможных для выбора ApplicationType
        ///     для выпадающего списка
        /// </summary>
        public IEnumerable<SelectListItem> ApplicationTypeSelectList { get; set; }

    }
}
