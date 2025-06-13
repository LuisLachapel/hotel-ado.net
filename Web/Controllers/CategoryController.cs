using Microsoft.AspNetCore.Mvc;
using Persistence.Category;
namespace Web.Controllers
{
    public class CategoryController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }

        public JsonResult List()
        {
            GetAll categories = new GetAll();
            return Json(categories.List());
        }

        public JsonResult GetById(int id)
        {
            Get function = new Get();
            return Json(function.GetCategory(id));

        }

        public int SaveData(Entity.Category category)
        {
            Save function = new Save();
            return function.SaveCategory(category);
        }

        public int DeleteData(int id)
        {
            Delete function = new Delete();
            return function.DeleteCategory(id);
        }


    }
}
