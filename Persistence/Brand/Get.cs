using System.Data;
using System.Data.SqlClient;

namespace Persistence.Brand
{
    public class Get: DBConnection
    {
        public Entity.Brand GetBrand(int id)
        {
            Entity.Brand brand = new Entity.Brand();
            using(SqlConnection connection = new SqlConnection(db_connection))
            {
                try
                {
                    connection.Open();
                    using (SqlCommand command = new SqlCommand("uspRecuperarMarca", connection))
                    {
                        command.CommandType = CommandType.StoredProcedure;
                        command.Parameters.AddWithValue("@id", id);
                        SqlDataReader reader = command.ExecuteReader();
                        if (reader.HasRows)
                        {
                            int fieldId = reader.GetOrdinal("IIDMARCA");
                            int fieldName = reader.GetOrdinal("NOMBREMARCA");
                            int fieldDescription = reader.GetOrdinal("DESCRIPCION");

                            while (reader.Read())
                            {
                                brand.id = reader.IsDBNull(fieldId) ? 0 : reader.GetInt32(fieldId);
                                brand.name = reader.IsDBNull(fieldName) ? "": reader.GetString(fieldName);
                                brand.description = reader.IsDBNull(fieldDescription) ? "" : reader.GetString(fieldDescription);
                            }
                        }
                    }
                }
                catch (Exception)
                {

                    connection.Close();
                }
            }
            return brand;
        }
    }
}
