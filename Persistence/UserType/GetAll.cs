using System.Data;
using System.Data.SqlClient;

namespace Persistence.UserType
{
    public class GetAll: DBConnection
    {
        public List<Entity.UserType> List()
        {
            List<Entity.UserType> userTypes = new List<Entity.UserType>();
            using(SqlConnection connection = new SqlConnection(db_connection))
            {
                connection.Open();
                try
                {
                    using (SqlCommand command = new SqlCommand("uspListarTipoUsuario", connection))
                    {
                        command.CommandType = CommandType.StoredProcedure;
                        SqlDataReader reader = command.ExecuteReader();
                        if (reader.HasRows)
                        {
                            int idField = reader.GetOrdinal("IIDTIPOUSUARIO");
                            int nameField = reader.GetOrdinal("NOMBRE");
                            int descriptionField = reader.GetOrdinal("DESCRIPCION");

                            while (reader.Read())
                            {
                                Entity.UserType userType = new Entity.UserType();
                                userType.id = reader.IsDBNull(idField) ? 0 : reader.GetInt32(idField);
                                userType.name = reader.IsDBNull(nameField) ? "" : reader.GetString(nameField);
                                userType.description = reader.IsDBNull(descriptionField) ? "" : reader.GetString(descriptionField);
                                userTypes.Add(userType);
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
            return userTypes;
        }
    }
}
