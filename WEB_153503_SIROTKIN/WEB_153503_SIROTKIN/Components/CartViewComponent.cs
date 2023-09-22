using Microsoft.AspNetCore.Mvc;

namespace WEB_153503_SIROTKIN.ViewComponents
{
    public class CartViewComponent : ViewComponent
    {
        public IViewComponentResult Invoke()
        {

            return View();
        }
    }
}