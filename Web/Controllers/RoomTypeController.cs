using Microsoft.AspNetCore.Mvc;
using Persistence.RoomType;
using Entity;

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
            GetAll Roomstype = new GetAll();
            return Json(Roomstype.List());
        }

        // Url para el metodo de filtrado:https://localhost:7049/RoomType/FilterRoomType/?parameter=i
        public JsonResult FilterRoomType(string parameter)
        {
            Filter filter = new Filter();
            return Json(filter.FilterRoomType(parameter));
        }

        public int SaveData(RoomType roomType)
        {
            Save save = new Save();
            return save.SaveRoomType(roomType);
        }


        // url para obtener por id: https://localhost:7049/RoomType/GetById/?id=1
        public JsonResult GetById(int id)
        {
            Get roomType = new Get();
            return Json(roomType.GetRoomType(id));
        }
    }
}
