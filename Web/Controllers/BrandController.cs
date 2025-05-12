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

        public JsonResult GetById(int id)
        {
            Get brand = new Get();
            return Json(brand.GetBrand(id));
        }

        public int SaveData(Entity.Brand brand)
        {
            Save save = new Save();
            return save.SaveBrand(brand);
        }

        public int Delete(int id)
        {
            Delete brand = new Delete();
            return brand.DeleteBrand(id);
        }
    }
}
