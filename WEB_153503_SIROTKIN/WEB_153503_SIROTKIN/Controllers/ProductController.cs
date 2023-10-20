using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using WEB_153503_SIROTKIN.Domain.Entities;
using WEB_153503_SIROTKIN.Domain.Models;
using WEB_153503_SIROTKIN.Services.LaptopCategoryService;
using WEB_153503_SIROTKIN.Services.LaptopService;

namespace WEB_153503_SIROTKIN.Controllers
{
    public class ProductController : Controller
    {
        private ILaptopCategoryService _laptopCategoryService;
        private ILaptopService _laptopService;
        public ProductController(ILaptopService laptopService, ILaptopCategoryService laptopCategoryService)
        {
            _laptopCategoryService = laptopCategoryService;
            _laptopService = laptopService;
        }

        public async Task<IActionResult> Index(string? laptopCategoryNormalized, int pageNo = 1)
        {
            if (laptopCategoryNormalized == "Все")
                laptopCategoryNormalized = null;

            var categories = (await _laptopCategoryService.GetCategoryListAsync()).Data;

            ViewBag.Categories = categories;
            ViewBag.CurrentCategory =
                laptopCategoryNormalized == null ?
                new Category { Id = 3, Name = "Все", NormalizedName = null } :
                categories.Where(category => category.NormalizedName.Equals(laptopCategoryNormalized)).ToList().First();

            var productResponse = await _laptopService.GetLaptopListAsync(laptopCategoryNormalized, pageNo);

            return View(new ListModel<Laptop>
            {
                Items = productResponse.Data.Items,
                CurrentPage = pageNo,
                TotalPages = productResponse.Data.TotalPages,
            });
        }
    }
}
