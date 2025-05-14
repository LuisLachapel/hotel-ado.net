using System.Data;
using System.Data.SqlClient;

namespace Persistence.Bed
{
    public class Delete: DBConnection
    {
        public int DeleteBed(int id)
        {
            int response = 0;
            using(SqlConnection connection = new SqlConnection(db_connection))
            {
                try
                {
                    connection.Open();
                    using (SqlCommand command = new SqlCommand("uspEliminarCama", connection))
                    {
                        command.CommandType = CommandType.StoredProcedure;
                        command.Parameters.AddWithValue("@id", id);
                        response = command.ExecuteNonQuery();
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
