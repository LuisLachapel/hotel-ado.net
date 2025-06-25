using Microsoft.AspNetCore.Mvc;
using Persistence.Room;

namespace Web.Controllers
{
    public class RoomController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }

        public JsonResult GetList()
        {
            GetAllList function = new GetAllList();
            return Json(function.RoomListings());
        }
    }
}
