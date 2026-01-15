using CommercialShop.Data;
using CommercialShop.Models.Cart;
using CommercialShop.Models.ProductModel;

using CommercialShop.Repository;
using CommercialShop.ViewModel;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace CommercialShop.Controllers
{
    public class CartController : Controller
    {

        public readonly AppDbContext _context;

        public CartController(AppDbContext context)
        {
            _context = context;
        }

        [HttpGet]
        public IActionResult ShoppingCart(Guid productId, int sizeID)
        {

            return View();
        }


       
    }
}
