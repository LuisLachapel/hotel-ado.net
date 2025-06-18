using System.Data;
using System.Data.SqlClient;
using System.IO;

namespace Persistence.Hotel
{
    public class Save: DBConnection
    {
        public int SaveHotel(Entity.Hotel hotel)
        {
            int result = 0;
            using (SqlConnection connection = new SqlConnection(db_connection))
            {
                try
                {
                    connection.Open();
                    using (SqlCommand command = new SqlCommand("uspInsertarHotel", connection))
                    {
                        command.CommandType = CommandType.StoredProcedure;
                        command.Parameters.AddWithValue("@id", hotel.id); 
                        command.Parameters.AddWithValue("@nombre", hotel.name);
                        command.Parameters.AddWithValue("@descripcion", hotel.description);
                        command.Parameters.AddWithValue("@direccion", hotel.address);
                        command.Parameters.AddWithValue("@file_name", hotel.file_name);
                        if (hotel.file_name != null)
                        {
                            File.WriteAllBytes(Path.Combine(hotel.path,hotel.file_name),hotel.photo);
                        }

                        result = command.ExecuteNonQuery();



                    }
                }
                catch (Exception)
                {

                   result = 0;
                    connection.Close();
                }
            }
            return result;
        }
    }
}
