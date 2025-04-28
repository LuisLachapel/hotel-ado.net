using Microsoft.AspNetCore.Mvc;
using Persistence.TypeRoom;

namespace Web.Controllers
{
    public class TypeRoomController : Controller
    {
        public ActionResult Index()
        {
            return View();
        }

        public JsonResult List()
        {
            GetAllTypeRoom typeRooms = new GetAllTypeRoom();
            return Json(typeRooms.List());
        }
    }
}
