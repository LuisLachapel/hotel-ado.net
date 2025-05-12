using System.Data;
using System.Data.SqlClient;


namespace Persistence.Brand
{
    public class Save: DBConnection
    {
        public int SaveBrand(Entity.Brand brand )
        {
            int response = 0;
            using(SqlConnection connection = new SqlConnection(db_connection))
            {
                try
                {
                    connection.Open();
                    using (SqlCommand command = new SqlCommand("uspGuardarMarca", connection))
                    {
                        command.CommandType = CommandType.StoredProcedure;
                        command.Parameters.AddWithValue("@id", brand.id);
                        command.Parameters.AddWithValue("@nombre", brand.name);
                        command.Parameters.AddWithValue("@descripcion", brand.description);
                        command.ExecuteNonQuery();
                    }

                }
                catch (Exception)
                {

                    throw;
                }
            }
            return response;

        }
    }
}
