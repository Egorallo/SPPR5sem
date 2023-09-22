using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using WEB_153503_SIROTKIN.Models;

namespace WEB_153503_SIROTKIN.Controllers
{
    public class HomeController : Controller
    {
        public IActionResult Index()
        {
            ViewData["CurrentLab"] = "Lab 2";

            var listItems = new List<ListDemo>
            {
                new ListDemo { Id = 1, Name = "Item 1" },
                new ListDemo { Id = 2, Name = "Item 2" },
                new ListDemo { Id = 3, Name = "Item 3" }
            };

            var selectList = new SelectList(listItems, "Id", "Name");
  
            return View(selectList);
        }
    }
}
