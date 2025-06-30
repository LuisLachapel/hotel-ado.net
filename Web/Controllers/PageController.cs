using Microsoft.AspNetCore.Mvc;
using Persistence.Page;


namespace Web.Controllers
{
    public class PageController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }

        public JsonResult List()
        {
            GetAll function = new GetAll();
            return Json(function.List());
        }
    }
}
