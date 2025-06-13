using System.Data;
using System.Data.SqlClient;

namespace Persistence.Category
{
    public class Get: DBConnection
    {
        public Entity.Category GetCategory(int id)
        {
            Entity.Category category = new Entity.Category();
            using(SqlConnection connection = new SqlConnection(db_connection))
            {
                try
                {
                    connection.Open();
                    using(SqlCommand command = new SqlCommand("uspRecuperarCategoria", connection))
                    {
                        command.CommandType = CommandType.StoredProcedure;
                        command.Parameters.AddWithValue("@id", category.id);
                        SqlDataReader reader = command.ExecuteReader();
                        if (reader.HasRows)
                        {
                            int idField = reader.GetOrdinal("IIDCATEGORIA");
                            int nameField = reader.GetOrdinal("NOMBRE");
                            int descriptionField = reader.GetOrdinal("DESCRIPCION");
                            if (reader.Read())
                            {
                                category.id = reader.IsDBNull(idField)? 0 : reader.GetInt32(idField);
                                category.name = reader.IsDBNull(nameField) ? "" : reader.GetString(nameField);
                                category.description = reader.IsDBNull(descriptionField) ? "" : reader.GetString(descriptionField);

                            }
                        }
                    }
                }
                catch (Exception)
                {
                   
                   connection.Close();
                }
            }
            return category;
        }
    }
}
