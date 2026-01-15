using CommercialShop.Models.ProductModel;
using CommercialShop.Models.ViewModel;

namespace CommercialShop.ViewModel
{
    public class ProductDetailViewModel
    {
        public Guid ProductId { get; set; }
        public string ProductName { get; set; } = string.Empty;
        public string ProductDescription { get; set; }

        public decimal Price {  get; set; }

        public List<SizeStockViewModel> Sizes { get; set; } = new();
        public List<string> Details { get; set; } = new();
        public List<string> ImagePaths { get; set; } = new();
    }
}


