using System.Data;
using System.Data.SqlClient;

namespace Persistence.UserType
{
    public class Save: DBConnection
    {
        public int SaveUserType(Entity.UserType userType)
        {
            int result = 0;
            using(SqlConnection connection = new SqlConnection(db_connection))
            {
                try
                {
                    connection.Open();
                    using (SqlCommand command = new SqlCommand("uspGuardarTipoUsuario", connection))
                    {
                        command.CommandType = CommandType.StoredProcedure;
                        command.Parameters.AddWithValue("@id",userType.id);
                        command.Parameters.AddWithValue("@nombre", userType.name);
                        command.Parameters.AddWithValue("@descripcion", userType.description);
                        SqlParameter outputIdParam = new SqlParameter("@NuevoID", SqlDbType.Int)
                        {
                            Direction = ParameterDirection.Output
                        };
                        command.Parameters.Add(outputIdParam);

                        result = command.ExecuteNonQuery();
                        if(userType.id == 0)
                        {
                            userType.id =  (int)outputIdParam.Value;
                        }
                    }

                    using(SqlCommand command = new SqlCommand("uspDeshabilitarPaginasTipoUsuario", connection))
                    {
                        command.CommandType = CommandType.StoredProcedure;
                        command.Parameters.AddWithValue("@idtipousuario", userType.id);
                        result = command.ExecuteNonQuery();
                    }

                    for(int index = 0; index <= userType.idPage.Count; index++)
                    {
                        using (SqlCommand command = new SqlCommand("uspGuardarPaginasTipoUsuario", connection))
                        {
                            command.CommandType = CommandType.StoredProcedure;
                            command.Parameters.AddWithValue("@idtipousuario", userType.id);
                            command.Parameters.AddWithValue("@idpagina", userType.idPage[index]);
                            result = command.ExecuteNonQuery();
                        }

                    }
                }
                catch (Exception ex)
                {
                    connection.Close();
                    result = 0;
                    throw new Exception("Errores al guardar tipo usuario" + ex.Message, ex);
                }
            }
            return result;
        }
    }
}
