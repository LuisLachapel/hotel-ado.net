using Microsoft.AspNetCore.Mvc;
using Persistence.Person;

namespace Web.Controllers
{
    public class PersonController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }

        public JsonResult List()
        {
            GetAll person = new GetAll();
            return Json(person.List());
        }


        //Url para el metodo de filtrado: https://localhost:7049/Person/Filter/?id=2
        public JsonResult Filter(int id)
        {
            Filter person = new Filter();
            return Json(person.FilterPerson(id));
        }
    }
}
