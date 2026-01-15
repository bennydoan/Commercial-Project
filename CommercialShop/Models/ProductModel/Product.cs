using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CommercialShop.Models.ProductModel
{
    public class Product
    {
        [Key]
        public Guid ProductId { get; set; }

        [Required(ErrorMessage = "Product name is required")]
        public string ProductName { get; set; } = string.Empty;

        [Required(ErrorMessage = "Price can not null")]
        [Range(0.0, double.MaxValue, ErrorMessage = "Price must be a positive value")]
        public decimal Price { get; set; }

        [Required, MaxLength(500)]
        public string ProductDescription { get; set; } = string.Empty;

        //FK
        public int CategoryId { get; set; }

        [ForeignKey("CategoryId")]
        public Categories categories { get; set; }

        // Navigation properties (initialized to avoid null refs)
        public List<ItemVariant> itemVariants { get; set; } = new();
        public List<ProductImage> images { get; set; } = new();
        public List<ProductDetails> details { get; set; } = new();
    }
}
