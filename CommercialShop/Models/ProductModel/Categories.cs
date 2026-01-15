using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CommercialShop.Models.ProductModel
{
    

    public class Categories
    {
        [Key]
        public int CategoryId { get; set; } 
        [Required(ErrorMessage ="This field is required")]
        public string CategoryName { get; set; } = string.Empty;

        public Gender gender { get; set; }

        public List<Product> Products { get; set; } = new();


    }
}
