using System.Data;
using System.Data.SqlClient;


namespace Persistence.TypeRoom
{
    public class GetAllTypeRoom: DBConnection
    {
        public List<Entity.RoomType> List()
        {
            List<Entity.RoomType> typeRooms = new List<Entity.RoomType>();
            using(SqlConnection connection = new SqlConnection(db_connection))
            {
                try
                {
                    connection.Open();
                    using (SqlCommand command = new SqlCommand("uspListarTipoHabitacion", connection))
                    {
                        command.CommandType = CommandType.StoredProcedure;
                        SqlDataReader reader = command.ExecuteReader();
                        
                        if (reader.HasRows)
                        {
                            
                            int fieldId = reader.GetOrdinal("IIDTIPOHABILITACION");
                            int fieldName = reader.GetOrdinal("NOMBRE");
                            int fieldDescription = reader.GetOrdinal("DESCRIPCION");

                            while (reader.Read())
                            {
                                Entity.RoomType typeRoom = typeRoom = new Entity.RoomType();
                                typeRoom.id = reader.GetInt32(fieldId);
                                typeRoom.name = reader.GetString(fieldName);
                                typeRoom.description = reader.GetString(fieldDescription);
                                typeRooms.Add(typeRoom);
                            }
                        }


                    }
                    connection.Close();
                }
                catch (Exception)
                {

                    connection.Close();
                }

            }
            return typeRooms;

        }
    }
}
