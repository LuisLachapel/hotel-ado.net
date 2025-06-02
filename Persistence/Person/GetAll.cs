using System.Data;
using System.Data.SqlClient;

namespace Persistence.Person
{
    public class GetAll: DBConnection
    {
        public List<Entity.Person> List()
        {
            List<Entity.Person> persons = new List<Entity.Person> ();
            using(SqlConnection connection = new SqlConnection(db_connection))
            {
                connection.Open ();
                try
                {
                    using(SqlCommand command = new SqlCommand("uspListarPersona", connection))
                    {
                        command.CommandType = CommandType.StoredProcedure;
                        SqlDataReader reader = command.ExecuteReader();
                        if (reader.HasRows)
                        {
                            int idField = reader.GetOrdinal("IIDPERSONA");
                            int nameField = reader.GetOrdinal("NOMBRECOMPLETO");
                            int sexField = reader.GetOrdinal("NOMBRESEXO");
                            int userTypeField = reader.GetOrdinal("NOMBRETIPOUSUARIO");
                            while (reader.Read())
                            {
                                Entity.Person person = new Entity.Person();
                                person.id = reader.IsDBNull(idField)?  0 : reader.GetInt32(idField);
                                person.name = reader.IsDBNull(nameField) ? "" : reader.GetString(nameField);
                                person.sex = reader.IsDBNull(sexField) ? "": reader.GetString(sexField);
                                person.userType = reader.IsDBNull(userTypeField) ? "" : reader.GetString(userTypeField);
                                persons.Add(person);

                            }
                        }
                    }
                    connection.Close ();

                }
                catch (Exception)
                {

                    connection.Close();
                }
            }
            return persons;
        }
    }
}
