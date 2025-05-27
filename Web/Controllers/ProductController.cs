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
            Persistence.Product.GetAll products = new Persistence.Product.GetAll();
            return Json(products.List());
        }
        public JsonResult FilterProduct(string parameter)
        {
            Persistence.Product.Filter products = new Persistence.Product.Filter();
            return Json(products.FilterProducts(parameter));
        }

        public JsonResult ProductBrandList()
        {
            
            ProductBrand productBrand = new ProductBrand();
            return Json(productBrand.list());
            

            
        }

        public JsonResult FilterProductByBrand(int id)
        {
            FilterByBrand products = new FilterByBrand();
            return Json(products.listByBrand(id));
        }
    }
}
