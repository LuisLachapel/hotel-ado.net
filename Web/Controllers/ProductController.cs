using Microsoft.AspNetCore.Mvc;
using Persistence.Product;
//using Persistence.Category;
    

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

      
        public JsonResult FilterProduct(string parameter)
        {
            Filter products = new Filter();
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

        public JsonResult Get(int id)
        {
            GetById product = new GetById();
            return Json(product.GetProduct(id));
        }

        public int SaveData(Entity.Product product)
        {
            Save save = new Save();
            return save.SaveProduct(product);
        }

        public int DeleteProduct(int id)
        {
            Delete function = new Delete();
            return function.DeleteProduct(id);
        }
    }
}
