using System.Data;
using System.Data.SqlClient;

namespace Persistence.Category
{
    public class GetAll: DBConnection
    {
        public List<Entity.Category> List()
        {
            List<Entity.Category> list = new List<Entity.Category>();
            using(SqlConnection connection = new SqlConnection(db_connection))
            {
                connection.Open();
                try
                {
                    using(SqlCommand command = new SqlCommand("uspListarCategorias", connection))
                    {
                        command.CommandType = CommandType.StoredProcedure;
                        SqlDataReader reader = command.ExecuteReader();
                        if (reader.HasRows)
                        {
                            int idField = reader.GetOrdinal("IIDCATEGORIA");
                            int nameField = reader.GetOrdinal("NOMBRE");
                            int descriptionField = reader.GetOrdinal("DESCRIPCION");
                            while (reader.Read())
                            {
                                Entity.Category category = new Entity.Category();
                                category.id = reader.IsDBNull(idField) ? 0 : reader.GetInt32(idField);
                                category.name = reader.IsDBNull(nameField)? "" :  reader.GetString(nameField);
                                category.description = reader.IsDBNull(descriptionField) ? "" : reader.GetString(descriptionField);
                                list.Add(category);
                            }
                        }
                    }

                }
                catch (Exception)
                {

                    connection.Close();
                }
                connection.Close();
            }

            return list;
        }
    }
}
