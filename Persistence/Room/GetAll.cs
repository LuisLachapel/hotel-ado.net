using System.Data;
using System.Data.SqlClient;

namespace Persistence.Room
{
    public class GetAll: DBConnection
    {
        public List<Entity.Room> List()
        {
            List<Entity.Room> rooms = new List<Entity.Room>();
            using(SqlConnection connection = new SqlConnection(db_connection))
            {
                try
                {
                    connection.Open();
                    using(SqlCommand command = new SqlCommand("uspHabitaciones", connection))
                    {
                        command.CommandType = CommandType.StoredProcedure;
                        SqlDataReader reader = command.ExecuteReader();
                        if (reader.HasRows)
                        {
                            int idField = reader.GetOrdinal("IIDHABITACION");
                            int nameField = reader.GetOrdinal("NOMBRE");
                            int priceByNightField = reader.GetOrdinal("PRECIOPORNOCHE");
                            int numberOfPeopleField = reader.GetOrdinal("NUMEROPERSONAS");
                            int hasWifiField = reader.GetOrdinal("TIENEWIFI");
                            int hasSeaViewField = reader.GetOrdinal("TIENEVISTAALMAR");
                            int hasPoolField = reader.GetOrdinal("TIENEPISCINA");
                            while (reader.Read())
                            {
                                Entity.Room room = new Entity.Room();
                                room.id = reader.IsDBNull(idField) ? 0 : reader.GetInt32(idField);
                                room.name = reader.IsDBNull(nameField) ? "" : reader.GetString(nameField);
                                room.priceByNight = reader.IsDBNull(priceByNightField) ? 0 : reader.GetDecimal(priceByNightField);
                                room.numberOfPeople = reader.IsDBNull(numberOfPeopleField) ? 0 : reader.GetInt32(numberOfPeopleField);
                                room.hasWifi = reader.IsDBNull(hasWifiField) ? "" : reader.GetInt32(hasWifiField) == 1 ? "Si" : "No";
                                room.hasSeaView = reader.IsDBNull(hasSeaViewField) ? "" : reader.GetInt32(hasSeaViewField) == 1 ? "Si" : "No";
                                room.hasPool = reader.IsDBNull(hasPoolField) ? "" : reader.GetInt32(hasPoolField) == 1 ? "Si" : "No";
                                rooms.Add(room);
                            }

                        }
                    } 


                }
                catch (Exception ex)
                {
                    connection.Close();
                    throw new Exception("Errores en obtener habitacion " + ex.Message, ex);
                }
            }
            return rooms;
        }
    }
}
