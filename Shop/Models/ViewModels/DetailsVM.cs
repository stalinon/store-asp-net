namespace Shop.Models.ViewModels
{
    public class DetailsVM
    {
        public Product Product { get; set; }
        public bool ExistInCart { get; set; }

        public DetailsVM()
        {
            Product = new Product();
        }
    }
}
