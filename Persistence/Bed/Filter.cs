using System.Data;
using System.Data.SqlClient;


namespace Persistence.Bed
{
    public class Filter: DBConnection
    {
        public List<Entity.Bed> FilterBeds (string parameter)
        {
            List<Entity.Bed> beds = new List<Entity.Bed>();
            using(SqlConnection connection = new SqlConnection(db_connection))
            {
                try
                {
                    connection.Open();
                    using(SqlCommand command = new SqlCommand("uspFiltrarCama", connection))
                    {
                        command.CommandType = CommandType.StoredProcedure;
                        command.Parameters.AddWithValue("@nombrecama", parameter);
                        SqlDataReader reader = command.ExecuteReader();
                        if (reader.HasRows)
                        {
                            int idField = reader.GetOrdinal("IIDCAMA");
                            int nameField = reader.GetOrdinal("NOMBRE");
                            int descriptionField = reader.GetOrdinal("DESCRIPCION");

                            while (reader.Read())
                            {
                                Entity.Bed bed = new Entity.Bed();
                                bed.id = reader.IsDBNull(idField) ? 0: reader.GetInt32(idField);
                                bed.name = reader.IsDBNull(nameField) ? "": reader.GetString(nameField);
                                bed.description = reader.IsDBNull(descriptionField) ? "": reader.GetString(descriptionField);
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
