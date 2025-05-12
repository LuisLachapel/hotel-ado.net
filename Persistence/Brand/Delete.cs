using System.Data;
using System.Data.SqlClient;


namespace Persistence.Brand
{
    public class Delete: DBConnection
    {
        public int DeleteBrand(int id)
        {
            int reponse = 0;
            using(SqlConnection connection = new SqlConnection(db_connection))
            {
                try
                {
                    connection.Open();
                    using(SqlCommand command = new SqlCommand("uspEliminarMarca", connection))
                    {
                        command.CommandType = CommandType.StoredProcedure;
                        command.Parameters.AddWithValue("@id", id);
                        command.ExecuteNonQuery();
                    }
                    
                }
                catch (Exception)
                {

                    connection.Close();
                }
            }

            return reponse;
        }

    }
}
