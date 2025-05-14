using System.Data;
using System.Data.SqlClient;

namespace Persistence.Bed
{
    public class Get: DBConnection
    {
        public Entity.Bed GetBed(int id)
        {
            Entity.Bed bed = new Entity.Bed();
            using(SqlConnection connection = new SqlConnection(db_connection))
            {
                try
                {
                    connection.Open();
                    using(SqlCommand command = new SqlCommand("uspRecuperarCama", connection))
                    {
                        command.CommandType = CommandType.StoredProcedure;
                        command.Parameters.AddWithValue("id", id);
                        SqlDataReader reader = command.ExecuteReader();
                        if (reader.HasRows)
                        {
                            int fieldId = reader.GetOrdinal("IIDCAMA");
                            int fieldName = reader.GetOrdinal("NOMBRE");
                            int fieldDescription = reader.GetOrdinal("DESCRIPCION");
                            while (reader.Read())
                            {
                                bed.id = reader.IsDBNull(fieldId) ? 0 : reader.GetInt32(fieldId);
                                bed.name = reader.IsDBNull(fieldName) ? "" : reader.GetString(fieldName);
                                bed.description = reader.IsDBNull(fieldDescription) ? "" : reader.GetString(fieldDescription);
                            }

                        }
                    }

                }
                catch (Exception)
                {

                    connection.Close();
                }
            }
            return bed;
        }
    }
}
