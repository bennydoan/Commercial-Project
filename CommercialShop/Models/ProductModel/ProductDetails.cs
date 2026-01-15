using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CommercialShop.Models.ProductModel
{
    public class ProductDetails
    {
        [Key]
        public int DetailId {  get; set; }
        [Required(ErrorMessage ="Details can not be blank")]
        public string Detail {  get; set; }

        //Fk

        public Guid ProductId {  get; set; }
        [ForeignKey("ProductId")]
        public Product product { get; set; }

    }
}
