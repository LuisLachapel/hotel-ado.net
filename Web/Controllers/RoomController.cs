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
        //https://localhost:7049/Room/Get?id=1
        public JsonResult Get(int id)
        {
            GetById function = new GetById();
            return Json(function.GetRoom(id));
        }

        public JsonResult List()
        {
            GetAll function = new GetAll();
            return Json(function.List());
        }
    }
}
