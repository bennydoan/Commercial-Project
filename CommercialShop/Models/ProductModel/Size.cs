using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace CommercialShop.Models.ProductModel
{
    public class Size
    {
        [Key]
        public int SizeId { get; set; }

        [Required]
        public string CodeSize { get; set; } = string.Empty;

        public List<ItemVariant> itemVariants { get; set; } = new();
    }
}
