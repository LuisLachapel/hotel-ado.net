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
                            command.Parameters.Add("@foto", SqlDbType.VarBinary).Value = person.photo ?? (object)DBNull.Value;
                            command.Parameters.Add("@nombre_foto", SqlDbType.VarChar, 60).Value =
                                string.IsNullOrWhiteSpace(person.photo_name) ? (object)DBNull.Value : person.photo_name;


                            SqlParameter outputIdParam = new SqlParameter("@NuevoID", SqlDbType.Int)
                            {
                                Direction = ParameterDirection.Output
                            };
                            command.Parameters.Add(outputIdParam);

                            result = command.ExecuteNonQuery();
                            person.id = (int)outputIdParam.Value;


                        }
                        using (SqlCommand command = new SqlCommand("uspEliminarGustos", connection,transaction))
                        {
                            command.CommandType = CommandType.StoredProcedure;
                            command.Parameters.AddWithValue("@idpersona",person.id);
                            command.ExecuteNonQuery();
                        }
                        for (int index = 0; index < person.likes.Count; index++) // era <=
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
                catch (Exception ex)
                {
                   
                    result = 0;
                    connection.Close();
                    throw new Exception("Error al guardar persona y gustos: " + ex.Message, ex);
                }
            }

            return result;
        }
    }
}
