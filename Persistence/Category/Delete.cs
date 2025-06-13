using System.Data;
using System.Data.SqlClient;


namespace Persistence.Category
{
    public class Delete: DBConnection
    {
        public int DeleteCategory(int id)
        {
            int result = 0;
            using(SqlConnection connection = new SqlConnection(db_connection))
            {
                try
                {
                    connection.Open();
                    using(SqlCommand command = new SqlCommand("uspEliminarCategoria",connection))
                    {
                        command.CommandType = CommandType.StoredProcedure;
                        command.Parameters.AddWithValue("@id", id);
                        result = command.ExecuteNonQuery();
                    }
                }
                catch (Exception)
                {

                    result = 0;
                    connection.Close();
                }
            }
            return result;
        }
    }
}
