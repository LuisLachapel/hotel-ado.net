using System.Data;
using System.Data.SqlClient;


namespace Persistence.Brand
{
    public class GetAll: DBConnection
    {
        public List<Entity.Brand> List()
        {
            List<Entity.Brand> brands = new List<Entity.Brand>();
            using(SqlConnection connection = new SqlConnection(db_connection))
            {
                try
                {
                    connection.Open();
                    using(SqlCommand command = new SqlCommand("uspListarMarca", connection))
                    {
                        command.CommandType = CommandType.StoredProcedure;
                        SqlDataReader reader = command.ExecuteReader();
                        if (reader.HasRows)
                        {
                            int fieldId = reader.GetOrdinal("IIDMARCA");
                            int fieldName = reader.GetOrdinal("NOMBREMARCA");
                            int fieldDescription = reader.GetOrdinal("DESCRIPCION");

                            while (reader.Read())
                            {
                                Entity.Brand brand = new Entity.Brand();
                                brand.id = reader.GetInt32(fieldId);
                                brand.name = reader.GetString(fieldName);
                                brand.description = reader.GetString(fieldDescription);
                                brands.Add(brand);
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
            return brands;
        }
    }
}
