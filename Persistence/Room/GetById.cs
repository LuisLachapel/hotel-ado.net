using System.Data;
using System.Data.SqlClient;

namespace Persistence.Room
{
    public class GetById: DBConnection
    {
        public Entity.Room GetRoom(int id)
        {
            Entity.Room room = new Entity.Room();
            using (SqlConnection connection = new SqlConnection(db_connection))
            {
                try
                {
                    connection.Open();
                    using (SqlCommand command = new SqlCommand("uspRecuperarHabitacion", connection))
                    {
                        command.CommandType = CommandType.StoredProcedure;
                        command.Parameters.AddWithValue("@id", id);
                        SqlDataReader reader = command.ExecuteReader();
                        if (reader.HasRows)
                        {

                            int idField = reader.GetOrdinal("IIDHABITACION");
                            int nameField = reader.GetOrdinal("NOMBRE");
                            int descriptionField = reader.GetOrdinal("DESCRIPCION");
                            int idBedField = reader.GetOrdinal("IIDCAMA");
                            int idRoomTypeField = reader.GetOrdinal("IIDTIPOHABITACION");
                            int idHotelField = reader.GetOrdinal("idHotel");
                            int priceByNightField = reader.GetOrdinal("PRECIOPORNOCHE");
                            int numberOfPeopleField = reader.GetOrdinal("NUMEROPERSONAS");
                            int hasWifiField = reader.GetOrdinal("TIENEWIFI");
                            int hasSeaViewField = reader.GetOrdinal("TIENEVISTAALMAR");
                            int hasPoolField = reader.GetOrdinal("TIENEPISCINA");
                            while (reader.Read())
                            {
                                
                                room.id = reader.IsDBNull(idField) ? 0 : reader.GetInt32(idField);
                                room.name = reader.IsDBNull(nameField) ? "" : reader.GetString(nameField);
                                room.description = reader.IsDBNull(descriptionField) ? "" : reader.GetString(descriptionField);
                                room.bedId = reader.IsDBNull(idBedField) ? 0 : reader.GetInt32(idBedField);
                                room.hotelId = reader.IsDBNull(idHotelField) ? 0 : reader.GetInt32(idHotelField);
                                room.roomTypeId = reader.IsDBNull(idRoomTypeField) ? 0 : reader.GetInt32(idRoomTypeField);
                                room.priceByNight = reader.IsDBNull(priceByNightField) ? 0 : reader.GetDecimal(priceByNightField);
                                room.numberOfPeople = reader.IsDBNull(numberOfPeopleField) ? 0 : reader.GetInt32(numberOfPeopleField);
                                room.hasWifi = reader.IsDBNull(hasWifiField) ? "" : reader.GetInt32(hasWifiField) == 1 ? "Si" : "No";
                                room.hasSeaView = reader.IsDBNull(hasSeaViewField) ? "" : reader.GetInt32(hasSeaViewField) == 1 ? "Si" : "No";
                                room.hasPool = reader.IsDBNull(hasPoolField) ? "" : reader.GetInt32(hasPoolField) == 1 ? "Si" : "No";
                                
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
                return room;
        }
    }
}
