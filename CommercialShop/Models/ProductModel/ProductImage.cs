using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CommercialShop.Models.ProductModel
{
    public class ProductImage
    {
        [Key]
        public int ImageId {  get; set; }

        public string ImagePath {  get; set; }

        public bool IsShownFirst { get; set; }
        public Guid ProductId { get; set; }
        [ForeignKey("ProductId")]
        public Product product { get; set; }
    }
}
