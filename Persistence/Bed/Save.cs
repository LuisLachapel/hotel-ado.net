using System.Data;
using System.Data.SqlClient;

namespace Persistence.Bed
{
    public class Save: DBConnection
    {
        public int SaveBed(Entity.Bed bed)
        {
            int response = 0;
            using(SqlConnection connection = new SqlConnection(db_connection))
            {
                try
                {
                    connection.Open();
                    using (SqlCommand command = new SqlCommand("uspGuardarCama", connection))
                    {
                        command.CommandType = CommandType.StoredProcedure;
                        command.Parameters.AddWithValue("@id", bed.id);
                        command.Parameters.AddWithValue("@nombre", bed.name);
                        command.Parameters.AddWithValue("@descripcion", bed.description);
                        command.Parameters.AddWithValue("@idestado",bed.idState);
                        
                        response = command.ExecuteNonQuery();

                    }
                }
                catch (Exception)
                {

                    connection.Close();
                }
            }
            return response;
        }
    }
}
