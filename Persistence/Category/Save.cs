using System.Data;
using System.Data.SqlClient;


namespace Persistence.Category
{
    public class Save: DBConnection
    {
        public int SaveCategory(Entity.Category category)
        {
            int result = 0;
            using(SqlConnection connection = new SqlConnection(db_connection))
            {
                try
                {
                    connection.Open();
                    using(SqlCommand command = new SqlCommand("uspInsertarCategoria", connection))
                    {
                        command.CommandType = CommandType.StoredProcedure;
                        command.Parameters.AddWithValue("@id", category.id);
                        command.Parameters.AddWithValue("@nombre", category.name);
                        command.Parameters.AddWithValue("@descripcion", category.description);
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
