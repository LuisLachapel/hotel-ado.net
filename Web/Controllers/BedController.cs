using Microsoft.AspNetCore.Mvc;
using Persistence.Bed;
using Entity;

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

        public JsonResult FilterBeds(string parameter)
        {
            Filter beds = new Filter();
            return Json(beds.FilterBeds(parameter));
        }

        public JsonResult GetById(int id)
        {
            Get bed = new Get();
            return Json(bed.GetBed(id));
        }

        public int SaveData(Bed bed)
        {
            Save save = new Save();
            return save.SaveBed(bed);
        }

        public int Delete(int id)
        {
            Delete bed = new Delete();
            return bed.DeleteBed(id);
        }

    }
}
