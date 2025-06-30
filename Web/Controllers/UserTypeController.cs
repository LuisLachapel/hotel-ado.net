using Microsoft.AspNetCore.Mvc;
using Persistence.UserType;
using Entity;

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

        public int SaveData(UserType userType, List<int> pages)
        {
            userType.idPage = pages;
            Save function = new Save();
            return function.SaveUserType(userType);
        }
    }
}
