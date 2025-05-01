using System.Data;
using System.Data.SqlClient;

namespace Persistence.RoomType
{
    public class Filter: DBConnection
    {
        public List<Entity.RoomType> FilterRoomType(string parameter)
        {
            List<Entity.RoomType> roomTypes = new List<Entity.RoomType>();
            using(SqlConnection connection = new SqlConnection(db_connection))
            {
                connection.Open();
                try
                {
                    using(SqlCommand command = new SqlCommand("uspFiltrarTipoHabitacion", connection))
                    {
                        command.CommandType = CommandType.StoredProcedure;
                        command.Parameters.AddWithValue("@nombrehabitacion", parameter);
                        SqlDataReader reader = command.ExecuteReader();
                        if (reader.HasRows)
                        {
                            int fieldId = reader.GetOrdinal("IIDTIPOHABILITACION");
                            int fieldName = reader.GetOrdinal("NOMBRE");
                            int fieldDescription = reader.GetOrdinal("DESCRIPCION");

                            while (reader.Read())
                            {
                                Entity.RoomType roomType = new Entity.RoomType();
                                roomType.id = reader.IsDBNull(fieldId) ? 0: reader.GetInt32(fieldId);
                                roomType.name = reader.IsDBNull(fieldName) ? "": reader.GetString(fieldName);
                                roomType.description = reader.IsDBNull(fieldDescription) ? "": reader.GetString(fieldDescription);
                                roomTypes.Add(roomType);
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
            return roomTypes;
        }
    }
}
