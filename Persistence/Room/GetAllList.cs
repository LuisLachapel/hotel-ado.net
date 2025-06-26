using System.Data;
using System.Data.SqlClient;
using Entity;

namespace Persistence.Room
{
    public class GetAllList: DBConnection
    {
        public RoomList RoomListings()
        {
            RoomList roomList = new RoomList();
            using (SqlConnection connection = new SqlConnection(db_connection))
            {
                try
                {
                    connection.Open();
                    using(SqlCommand command = new SqlCommand("uspListarHabitacion", connection))
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
                            List<Entity.Room> rooms = new List<Entity.Room> ();

                            while (reader.Read())
                            {
                                Entity.Room room = new Entity.Room();
                                room.id = reader.IsDBNull(idField) ? 0 : reader.GetInt32(idField);
                                room.name = reader.IsDBNull(nameField)? "":  reader.GetString(nameField);
                                room.priceByNight = reader.IsDBNull(priceByNightField) ? 0 : reader.GetDecimal(priceByNightField);
                                room.numberOfPeople = reader.IsDBNull(numberOfPeopleField) ? 0 : reader.GetInt32(numberOfPeopleField);
                                room.hasWifi = reader.IsDBNull(hasWifiField) ? "" : reader.GetInt32(hasWifiField) == 1 ? "Si": "No";
                                room.hasSeaView = reader.IsDBNull(hasSeaViewField) ? "" : reader.GetInt32(hasSeaViewField) == 1 ? "Si" : "No";
                                room.hasPool = reader.IsDBNull(hasPoolField) ? "" : reader.GetInt32(hasPoolField) == 1 ? "Si" : "No";
                                rooms.Add(room);
                            }
                            roomList.rooms = rooms;
                        }

                        if (reader.NextResult())
                        {
                            List<Entity.RoomType> roomTypes = new List<Entity.RoomType>();
                            while (reader.Read())
                            {
                                Entity.RoomType roomType = new Entity.RoomType();
                                roomType.id = reader.IsDBNull(0)? 0 : reader.GetInt32(0);
                                roomType.name = reader.IsDBNull(1) ? "" : reader.GetString(1);
                                roomTypes.Add(roomType);
                            }
                            roomList.roomsType = roomTypes;

                        }
                        if (reader.NextResult())
                        {
                            List<Entity.Bed> beds = new List<Entity.Bed>();
                            while (reader.Read())
                            {
                                Entity.Bed bed = new Entity.Bed();
                                bed.id = reader.IsDBNull(0) ? 0 : reader.GetInt32(0);
                                bed.name = reader.IsDBNull(1) ? "" : reader.GetString(1);
                                beds.Add(bed);
                            }
                            roomList.beds = beds;

                        }
                        if (reader.NextResult())
                        {
                            List<Entity.Hotel> hotels = new List<Entity.Hotel>();
                            while (reader.Read())
                            {
                                Entity.Hotel hotel = new Entity.Hotel();
                                hotel.id = reader.IsDBNull(0) ? 0 : reader.GetInt32(0);
                                hotel.name = reader.IsDBNull(1) ? "" : reader.GetString(1);
                                hotels.Add(hotel);
                            }
                            roomList.hotels = hotels;

                        }
                    }
                }
                catch (Exception ex)
                {
                    throw new Exception("Error de listados " + ex.Message, ex);
                    connection.Close();
                }
            }
            return roomList;

        }

    }
}
