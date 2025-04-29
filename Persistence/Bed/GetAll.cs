using System.Data;
using System.Data.SqlClient;

namespace Persistence.Bed
{
    public class GetAll: DBConnection
    {
        public List<Entity.Bed> List()
        {
            List<Entity.Bed> beds = new List<Entity.Bed>();
            using(SqlConnection connection = new SqlConnection(db_connection))
            {
                
                try
                {
                    connection.Open();
                    using (SqlCommand command = new SqlCommand("uspListarCama", connection))
                    {
                        command.CommandType = CommandType.StoredProcedure;
                        SqlDataReader reader = command.ExecuteReader();
                        if (reader.HasRows)
                        {
                            int fieldId = reader.GetOrdinal("IIDCAMA");
                            int fieldName = reader.GetOrdinal("NOMBRE");
                            int fieldDescription = reader.GetOrdinal("DESCRIPCION");

                            while (reader.Read())
                            {
                                Entity.Bed bed = new Entity.Bed();
                                bed.id = reader.IsDBNull(fieldId) ? 0: reader.GetInt32(fieldId);
                                bed.name = reader.IsDBNull(fieldName) ? "":  reader.GetString(fieldName);
                                bed.description = reader.IsDBNull(fieldDescription) ? "":  reader.GetString(fieldDescription);
                                beds.Add(bed);

                            }
                        }
                    }
                    connection.Close();

                }
                catch (Exception)
                {

                    connection.Close();
                }
            }
            return beds;
        }
    }
}
