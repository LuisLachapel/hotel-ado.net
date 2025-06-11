using System.Data;
using System.Data.SqlClient;

namespace Persistence.Person
{
    public class Delete: DBConnection
    {
        public int DeletePerson(int id)
        {
            int result = 0;
            using(SqlConnection connection = new SqlConnection(db_connection))
            {
                try
                {
                    connection.Open();
                    using(SqlCommand command = new SqlCommand("uspEliminarPersona", connection))
                    {
                        command.CommandType = CommandType.StoredProcedure;
                        command.Parameters.AddWithValue("@id", id);
                        result = command.ExecuteNonQuery();
                    }

                }
                catch (Exception)
                {

                    connection.Close();
                }
            }
            return result;
        }
    }
}
