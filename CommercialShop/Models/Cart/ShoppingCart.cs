using CommercialShop.Models.ProductModel;
using System.ComponentModel.DataAnnotations;

public class ShoppingCart
{
    [Key]
    public int Id { get; set; }

    // Variant (best)
    public Guid VariantId { get; set; }
    public ItemVariant Variant { get; set; } // product ID + size id 

    // Snapshot fields (safe for history)
    public Guid ProductId { get; set; }
    public string ProductName { get; set; }
    public string SizeName { get; set; }

    public int Quantity { get; set; }
    public decimal Price { get; set; }

    public string ImagePath { get; set; }

    public decimal Total => Price * Quantity;
}
