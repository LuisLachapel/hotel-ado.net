using System.Data;
using System.Data.SqlClient;
namespace Persistence.Person
{
    public class Save: DBConnection
    {
        public int SavePerson(Entity.Person person)
        {
            int result = 0;
            using(SqlConnection connection = new SqlConnection(db_connection))
            {
                
                try
                {
                    connection.Open();
                    using (SqlTransaction transaction = connection.BeginTransaction())
                    {
                        using (SqlCommand command = new SqlCommand("uspGuardarPersona", connection,transaction))
                        {
                            command.CommandType = CommandType.StoredProcedure;
                            command.Parameters.AddWithValue("@idpersona", person.id);
                            command.Parameters.AddWithValue("@nombre", person.name);
                            command.Parameters.AddWithValue("@apellidopaterno", person.last_name);
                            command.Parameters.AddWithValue("@apellidomaterno", person.second_last_name);
                            command.Parameters.AddWithValue("@telefono", person.Phone);
                            command.Parameters.AddWithValue("@idsexo", person.idSex);
                            command.Parameters.AddWithValue("@idtipousuario", person.iduserType);
                            command.Parameters.AddWithValue("@foto", person.photo);
                            command.Parameters.AddWithValue("@nombre_foto", person.photo_name);
                            result = command.ExecuteNonQuery();

                        }
                        using (SqlCommand command = new SqlCommand("uspEliminarGustos", connection,transaction))
                        {
                            command.CommandType = CommandType.StoredProcedure;
                            command.Parameters.AddWithValue("@idpersona",person.id);
                            command.ExecuteNonQuery();
                        }
                        for (int index = 0; index <= person.likes.Count; index++)
                        {
                            using (SqlCommand command = new SqlCommand("agregarGusto", connection, transaction))
                            {
                                command.CommandType = CommandType.StoredProcedure;
                                command.Parameters.AddWithValue("@idpersona", person.id);
                                command.Parameters.AddWithValue("@idgusto", person.likes[index]);
                                command.ExecuteNonQuery();
                            }

                        }

                        transaction.Commit();
                    }
                    

                }
                catch (Exception)
                {
                    result = 0;
                    connection.Close();
                }
            }

            return result;
        }
    }
}
