using Microsoft.AspNetCore.Mvc;
using Persistence.TypeRoom;

namespace Web.Controllers
{
    public class RoomTypeController : Controller
    {
        public ActionResult Index()
        {
            return View();
        }

        public JsonResult List()
        {
            GetAllTypeRoom Roomstype = new GetAllTypeRoom();
            return Json(Roomstype.List());
        }
    }
}
