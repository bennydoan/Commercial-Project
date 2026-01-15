using AspNetCoreGeneratedDocument;
using CommercialShop.Data;
using CommercialShop.Models.ProductModel;
using CommercialShop.Models.ViewModel;
using CommercialShop.ViewModel;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Reflection.Metadata.Ecma335;

namespace CommercialShop.Controllers
{
    public class ProductController : Controller
    {
        public readonly AppDbContext _context;
        public ProductController(AppDbContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<IActionResult> ProductDetails(Guid id)
        {
            if (id == Guid.Empty)
                return BadRequest();

            var vm = await _context.Products
                .Where(p => p.ProductId == id)
                .Select(p => new ProductDetailViewModel
                {
                    ProductId = p.ProductId,
                    ProductName = p.ProductName,
                    ProductDescription = p.ProductDescription,
                    Price = p.Price,

                    Sizes = p.itemVariants.Select(iv => new SizeStockViewModel
                    {
                        SizeId = iv.SizeId,
                        SizeName = iv.size.CodeSize,
                        StockQuantity = iv.StockQuantity
                    }).ToList(),

                    ImagePaths = p.images.Select(i => i.ImagePath).ToList(),
                    Details = p.details.Select(d => d.Detail).ToList()
                })
                .FirstOrDefaultAsync();
            if (vm == null)
                return NotFound();

           

            var catGender = await _context.Products
                .AsNoTracking()
                .Where(p => p.ProductId == id).
                Select(p => p.categories == null ? (Gender?)null : p.categories.gender).FirstOrDefaultAsync();

            if (catGender == Gender.Male)
            {
                ViewBag.SizeImage = "/Image/Men/Size/SizeChart.png";
            }
            else if (catGender == Gender.Female)
            {
                ViewBag.SizeImage = Url.Content("~/Image/Women/Size/WsizeChart.png");
            }
      
            return View(vm);
        }
    }
}
