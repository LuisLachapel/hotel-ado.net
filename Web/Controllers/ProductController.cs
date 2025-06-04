using Microsoft.AspNetCore.Mvc;
using Persistence.Product;
using Persistence.Category;
    

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

        public JsonResult CategoryList()
        {
            Persistence.Category.GetAll categories = new Persistence.Category.GetAll();
            return Json(categories.List());
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


        //Filtrado por categoria: https://localhost:7049/Product/FilterProductByCategory/?id=1
        public JsonResult FilterProductByCategory(int id)
        {
            FilterByCategory products = new FilterByCategory();
            return Json(products.ListByCategory(id));
        }
    }
}
