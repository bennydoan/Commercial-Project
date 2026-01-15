using CommercialShop.Data;
using CommercialShop.Models;
using CommercialShop.Models.ProductModel;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Razor.Compilation;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Query.Internal;
using System.Diagnostics;
using System.Reflection;

namespace CommercialShop.Controllers
{
    [Authorize]
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly AppDbContext _context;

        public HomeController(ILogger<HomeController> logger, AppDbContext context)
        {
            _logger = logger;
            _context = context;
        }

        public IActionResult Index()
        {
            return View();
        }

        [HttpGet]
        public async Task<IActionResult> MenPage()
        {
            var products = await _context.Products
                 .Include(p => p.categories)
                 .Include(p => p.images)
                 .Where(p => p.categories != null && p.categories.gender == Gender.Male)
                 .Select(p => new Product
                 {
                     ProductId = p.ProductId,
                     CategoryId = p.CategoryId,
                     ProductName = p.ProductName,
                     Price = p.Price,
                     // only include images where IsShownFirst == true
                     images = p.images
                                 .Where(img => img.IsShownFirst)
                                 .ToList(),
                 }).ToListAsync();

            var menCategories = await _context.Categories
                .Where(c => c.gender == Gender.Male)
                .ToListAsync();

            ViewBag.MenCategories = menCategories;

            return View(products);
        }


        [HttpGet]
        public async Task<IActionResult> WomenPage()
        {
            var products = await _context.Products
                .Include (p => p.categories)
                .Include(p=>p.images)
                .Where(p=>p.categories !=null && p.categories.gender == Gender.Female)
                .Select(p => new Product
                {
                    ProductId = p.ProductId,
                    CategoryId = p.CategoryId,
                    ProductName = p.ProductName,
                    Price = p.Price,
                    images = p.images.Where(img=>img.IsShownFirst).ToList(),
                }).ToListAsync();

            var WomenCategories = await _context.Categories.Where(c=>c.gender == Gender.Female).ToListAsync();
            ViewBag.WomenCategories = WomenCategories;

            return View(products);
        }

        [HttpGet]
        public async Task<IActionResult> Filter(Gender? gender)
        {
            var targetGender = gender ?? Gender.Male;

            var categories = await _context.Categories.Where(c=>c.gender == targetGender).OrderBy(c=>c.CategoryName).ToListAsync();

            return PartialView("~/Views/Shared/_Partial/_FilterPartial.cshtml", categories);
        }


        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
