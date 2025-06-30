using System.Data;
using System.Data.SqlClient;
namespace Persistence.Page
{
    public class GetAll: DBConnection
    {
        public List<Entity.Page> List()
        {
            List<Entity.Page> pages = new List<Entity.Page>();
            using(SqlConnection connection = new SqlConnection(db_connection))
            {
                try
                {
                    connection.Open();
                    using (SqlCommand command = new SqlCommand("uspListarPagina", connection))
                    {
                        command.CommandType = CommandType.StoredProcedure;
                        SqlDataReader reader = command.ExecuteReader();
                        if (reader.HasRows)
                        {
                            int idField = reader.GetOrdinal("IIDPAGINA");
                            int messageField = reader.GetOrdinal("MENSAJE");
                            while (reader.Read())
                            {
                                Entity.Page page = new Entity.Page();
                                page.id = reader.IsDBNull(idField) ? 0 : reader.GetInt32(idField);
                                page.message = reader.IsDBNull(messageField) ? "" : reader.GetString(messageField);
                                pages.Add(page);
                            }
                        }

                    }
                    

                }
                catch (Exception ex)
                {
                    connection.Close();
                    throw new Exception("Error en GetAll Paginas " + ex.Message, ex);
                    
                }
            }
            return pages;
        }
    }
}
