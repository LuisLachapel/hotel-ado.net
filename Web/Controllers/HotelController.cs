using Microsoft.AspNetCore.Mvc;
using Persistence.Hotel;
using Entity;

namespace Web.Controllers
{
    public class HotelController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }

        public JsonResult List()
        {
            GetAll function = new GetAll();
            return Json(function.hotels());
        }

        
        public int SaveData(Hotel hotel, IFormFile photoFile)
        {
            if (photoFile != null && photoFile.Length > 0)
            {
                hotel.file_name = Path.GetFileName(photoFile.FileName); // Seguridad básica

                // Obtener la ruta absoluta a la carpeta "Files"
                string uploadsFolder = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "Files");

                // Asegurarse que el directorio existe
                if (!Directory.Exists(uploadsFolder))
                {
                    Directory.CreateDirectory(uploadsFolder);
                }

                hotel.path = uploadsFolder;

                // Copiar el archivo a un array de bytes
                using (var memoryStream = new MemoryStream())
                {
                    photoFile.CopyTo(memoryStream);
                    hotel.photo = memoryStream.ToArray();
                }
            }

            Save function = new Save();
            return function.SaveHotel(hotel);
        }
    }
}
