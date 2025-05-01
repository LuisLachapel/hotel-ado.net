using System.Data;
using System.Data.SqlClient;


namespace Persistence.RoomType
{
    public class GetAll: DBConnection
    {
        public List<Entity.RoomType> List()
        {
            List<Entity.RoomType> roomstype = new List<Entity.RoomType>();
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
                                typeRoom.id = reader.IsDBNull(fieldId) ? 0: reader.GetInt32(fieldId);
                                typeRoom.name = reader.IsDBNull(fieldName) ? "": reader.GetString(fieldName);
                                typeRoom.description = reader.IsDBNull(fieldDescription) ? "":  reader.GetString(fieldDescription);
                                roomstype.Add(typeRoom);
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
            return roomstype;

        }
    }
}
