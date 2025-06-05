using System.Data;
using System.Data.SqlClient;

namespace Persistence.Person
{
    public class GetById: DBConnection
    {
        public Entity.Person GetPerson(int id)
        {
            Entity.Person person = new Entity.Person();
            using(SqlConnection connection = new SqlConnection(db_connection))
            {
                connection.Open();
                try
                {
                    using(SqlCommand command = new SqlCommand("uspRecuperarPersona", connection))
                    {
                        command.CommandType = CommandType.StoredProcedure;
                        command.Parameters.AddWithValue("@idpersona", id);
                        SqlDataReader reader = command.ExecuteReader();
                        if (reader.HasRows)
                        {
                            //int idField = reader.GetOrdinal("IIDPERSONA");
                            int nameField = reader.GetOrdinal("NOMBRE");
                            int lastNameField = reader.GetOrdinal("APELLIDO");
                            int PhoneField = reader.GetOrdinal("TELEFONOFIJO");
                            int sexField = reader.GetOrdinal("IIDSEXO");
                            int userTypeField = reader.GetOrdinal("IIDTIPOUSUARIO");

                            while (reader.Read())
                            {
                                //person.id = reader.IsDBNull(idField)? 0: reader.GetInt32(idField);
                                person.name = reader.IsDBNull(nameField)? "" : reader.GetString(nameField);
                                person.last_name = reader.IsDBNull(lastNameField) ? "" : reader.GetString(lastNameField);
                                person.Phone = reader.IsDBNull(PhoneField) ? "" : reader.GetString(PhoneField);
                                person.idSex = reader.IsDBNull(sexField) ? 0 : reader.GetInt32(sexField);
                                person.iduserType = reader.IsDBNull(userTypeField) ? 0: reader.GetInt32(userTypeField);
                            }
                        }
                    }
                }
                catch (Exception)
                {

                    connection.Close();
                }
                connection.Close();
            }
            return person;
        }
    }
}
