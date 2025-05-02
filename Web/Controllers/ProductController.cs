using Microsoft.AspNetCore.Mvc;
using Persistence.Product;

namespace Web.Controllers
{
    public class ProductController : Controller
    {
        public ActionResult Index()
        {
            return View();
        }

        public JsonResult List()
        {
            GetAll products = new GetAll();
            return Json(products.List());
        }
    }
}
