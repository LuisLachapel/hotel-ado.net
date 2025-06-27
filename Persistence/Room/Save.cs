using System.Data;
using System.Data.SqlClient;

namespace Persistence.Room
{
    public class Save: DBConnection
    {
        public int SaveRoom(Entity.Room room)
        {
            int result = 0;
            using(SqlConnection connection = new SqlConnection(db_connection))
            {

                try
                {
                    connection.Open();
                    using(SqlCommand command = new SqlCommand("uspGuardarHabitacion", connection))
                    {
                        command.CommandType = CommandType.StoredProcedure;
                        command.Parameters.AddWithValue("@id", room.id);
                        command.Parameters.AddWithValue("@nombre", room.name);
                        command.Parameters.AddWithValue("@descripcion", room.description);
                        command.Parameters.AddWithValue("@numero", room.numberOfPeople);
                        command.Parameters.AddWithValue("@precio", room.priceByNight);
                        command.Parameters.AddWithValue("@vistaMar", room.hasSeaView);
                        command.Parameters.AddWithValue("@wifi", room.hasWifi);
                        command.Parameters.AddWithValue("@piscina", room.hasPool);
                        command.Parameters.AddWithValue("@idTypeRoom", room.roomTypeId);
                        command.Parameters.AddWithValue("@idHotel", room.hotelId);
                        command.Parameters.AddWithValue("@idBed", room.bedId);
                        result = command.ExecuteNonQuery();
                    }
                }
                catch (Exception ex)
                {
                    result = 0;
                    connection.Close();
                    throw new Exception("Error en guardar / editar habitación " + ex.Message, ex);
                    
                }
            }
            return result;
        } 
    }
}
