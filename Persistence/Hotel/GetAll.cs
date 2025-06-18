using System.Data;
using System.Data.SqlClient;

namespace Persistence.Hotel
{
    public class GetAll: DBConnection
    {
        public List<Entity.Hotel> hotels()
        {
            List<Entity.Hotel> hotels = new List<Entity.Hotel>();
            using(SqlConnection connection = new SqlConnection(db_connection))
            {
                try
                {
                    connection.Open();
                    using (SqlCommand command = new SqlCommand("uspListarHotel", connection))
                    {
                        command.CommandType = CommandType.StoredProcedure;
                        SqlDataReader reader = command.ExecuteReader();
                        if (reader.HasRows)
                        {
                            int idField = reader.GetOrdinal("IIDHOTEL");
                            int nameField = reader.GetOrdinal("NOMBRE");
                            int descriptionField = reader.GetOrdinal("DESCRIPCION");
                            int addressField = reader.GetOrdinal("DIRECCION");
                            while (reader.Read())
                            {
                                Entity.Hotel hotel = new Entity.Hotel();
                                hotel.id = reader.IsDBNull(idField) ? 0 : reader.GetInt32(idField);
                                hotel.name = reader.IsDBNull(nameField) ? "" : reader.GetString(nameField);
                                hotel.description = reader.IsDBNull(descriptionField) ? "": reader.GetString(descriptionField);
                                hotel.address = reader.IsDBNull(addressField) ? "": reader.GetString(addressField);
                                hotels.Add(hotel);
                            }

                        }
                        connection.Close();
                        
                    }
                }
                catch (Exception)
                {

                    connection.Close();
                }
            }
            return hotels;
        }
    }
}
