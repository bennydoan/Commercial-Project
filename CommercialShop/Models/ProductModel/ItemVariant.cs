using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CommercialShop.Models.ProductModel
{
    public class ItemVariant
    {
        [Key]
        public Guid VariantId { get; set; }

        //Fk
        public Guid ProductId{ get; set; }
        [ForeignKey("ProductId")]
        public Product product { get; set; }

        public int SizeId {  get; set; }
        [ForeignKey("SizeId")]
        public Size size { get; set; }
        [Range(0, int.MaxValue)]
        [Required(ErrorMessage ="Stock is required")]
        public int StockQuantity { get; set; }
    }
}
