using System.Data;
using System.Data.SqlClient;


namespace Persistence.RoomType
{
    public class Save : DBConnection
    {
        public int SaveRoomType(Entity.RoomType roomType)
        {
           int response = 0;
            using (SqlConnection connection = new SqlConnection(db_connection))
            {
                try
                {
                    connection.Open();
                    using(SqlCommand command = new SqlCommand("uspGuardarTipohabitacion", connection))
                    {
                        command.CommandType = CommandType.StoredProcedure;
                        command.Parameters.AddWithValue("@id", roomType.id);
                        command.Parameters.AddWithValue("@nombre", roomType.name);
                        command.Parameters.AddWithValue("@descripcion", roomType.description);
                        response = command.ExecuteNonQuery();
                        //El bloque using se encarga de liberar los recursos utilizados despues de que termine el proceso
                        //connection.Close();


                    }

                }
                catch (Exception)
                {
                    response = 0;
                    connection.Close();
                }
            }
            return response;
        }
    }
}
