using Microsoft.AspNetCore.Mvc;
using Persistence.UserType;

namespace Web.Controllers
{
    public class UserTypeController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }

        public JsonResult List()
        {
            GetAll userType = new GetAll();
            return Json(userType.List());
        }
    }
}
