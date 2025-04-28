using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using System.Data.SqlClient;


namespace Persistence.TypeRoom
{
    public class Get: DBConnection
    {
        public List<Entity.TypeRoom> List()
        {
            List<Entity.TypeRoom> typeRooms = new List<Entity.TypeRoom>();
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
                                Entity.TypeRoom typeRoom = typeRoom = new Entity.TypeRoom();
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
