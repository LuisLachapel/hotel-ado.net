using Microsoft.AspNetCore.Mvc;
using Persistence.Bed;

namespace Web.Controllers
{
    public class BedController : Controller
    {
        public ActionResult Index()
        {
            return View();
        }

        public JsonResult List()
        {
            GetAll beds = new GetAll();
            return Json(beds.List());
        }

    }
}
