using Microsoft.AspNetCore.Mvc;
using Persistence.Brand;

namespace Web.Controllers
{
    public class BrandController : Controller
    {
        public ActionResult Index()
        {
            return View();
        }

        public JsonResult List()
        {
            GetAll brands = new GetAll();
            return Json(brands.List());
        }

        public JsonResult FilterBrand(string parameter)
        {
            Filter brands = new Filter();
            return Json(brands.FilterBrands(parameter));
        }
    }
}
