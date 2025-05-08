using System.Data;
using System.Data.SqlClient;


namespace Persistence.RoomType
{
    public  class Get: DBConnection
    {
        public Entity.RoomType GetRoomType(int id)
        {
            Entity.RoomType roomType = null;
            using(SqlConnection connection = new SqlConnection(db_connection))
            {
                try
                {
                    connection.Open();
                    using (SqlCommand command = new SqlCommand("uspRecuperarTipoHabitacion", connection))
                    {
                        command.CommandType = CommandType.StoredProcedure;
                        command.Parameters.AddWithValue("@id", id);
                        SqlDataReader reader = command.ExecuteReader();

                        if (reader.HasRows)
                        {

                            int fieldId = reader.GetOrdinal("IIDTIPOHABILITACION");
                            int fieldName = reader.GetOrdinal("NOMBRE");
                            int fieldDescription = reader.GetOrdinal("DESCRIPCION");

                            while (reader.Read())
                            {
                                roomType = new Entity.RoomType();
                                roomType.id = reader.IsDBNull(fieldId) ? 0 : reader.GetInt32(fieldId);
                                roomType.name = reader.IsDBNull(fieldName) ? "" : reader.GetString(fieldName);
                                roomType.description = reader.IsDBNull(fieldDescription) ? "" : reader.GetString(fieldDescription);
                                
                            }
                        }
                    }
                }
                catch (Exception)
                {

                   connection.Close();
                }

            }
            return roomType;
        }
    }
}
