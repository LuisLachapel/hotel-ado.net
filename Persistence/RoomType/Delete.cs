using System.Data;
using System.Data.SqlClient;

namespace Persistence.RoomType
{
    public class Delete: DBConnection
    {
        public int DeleteRoomType(int id)
        {
            int response = 0;
            using(SqlConnection connection = new SqlConnection(db_connection))
            {
                
                try
                {
                    connection.Open();
                    using(SqlCommand command = new SqlCommand("uspEliminarTipohabitacion", connection))
                    {
                        command.CommandType = CommandType.StoredProcedure;
                        command.Parameters.AddWithValue("@id", id);
                        response = command.ExecuteNonQuery();
                        connection.Close();
                    }
                }
                catch (Exception)
                {

                    connection.Close();
                }
            }
            return response;
        }
    }
}
