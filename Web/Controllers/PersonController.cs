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

        public JsonResult Get(int id)
        {
            GetById person = new GetById();
            return Json(person.GetPerson(id));
        }

        [HttpGet]
        public IActionResult GetPhoto(int id)
        {
            GetById function = new GetById();
            var person = function.GetPerson(id);

            if (person.photo != null && person.photo.Length > 0)
            {
                return File(person.photo, "image/jpeg"); // O "image/png" según el tipo que guardes
            }

            return NotFound(); // O devuelve una imagen por defecto si prefieres
        }



        //Url para el metodo de filtrado: https://localhost:7049/Person/Filter/?id=2
        public JsonResult Filter(int id)
        {
            Filter person = new Filter();
            return Json(person.FilterPerson(id));
        }

        public int SaveData(Entity.Person person, IFormFile photoFile)
        {
            if(photoFile != null && photoFile.Length > 0)
            {
                person.photo_name = photoFile.FileName;
                
                using (var memoryStream = new MemoryStream())
                {
                    photoFile.CopyTo(memoryStream);
                    person.photo = memoryStream.ToArray(); // Guardamos la imagen en byte[]
                }
            }
            Save save = new Save();
            return save.SavePerson(person);

        }

        public int DeleteData(int id)
        {
            Delete function = new Delete();
            return function.DeletePerson(id);
        }
    }
}
