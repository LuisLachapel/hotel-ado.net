using System.Data;
using System.Data.SqlClient;

namespace Persistence.Product
{
    public class GetAll: DBConnection
    {
        public List<Entity.Product> List()
        {
            List<Entity.Product> products = new List<Entity.Product>();
            using(SqlConnection connection = new SqlConnection(db_connection))
            {
                try
                {
                    connection.Open();
                    using(SqlCommand command = new SqlCommand("uspListarProductos", connection))
                    {
                        command.CommandType = CommandType.StoredProcedure;
                        SqlDataReader reader = command.ExecuteReader();
                        if (reader.HasRows)
                        {
                            int idField = reader.GetOrdinal("IIDPRODUCTO");
                            int nameField = reader.GetOrdinal("NOMBRE");
                            int brandNameField = reader.GetOrdinal("NOMBREMARCA");
                            int salePriceField = reader.GetOrdinal("PRECIOVENTA");
                            int stockField = reader.GetOrdinal("STOCK");

                            while (reader.Read())
                            {
                                Entity.Product product = new Entity.Product();
                                product.id = reader.IsDBNull(idField) ? 0 : reader.GetInt32(idField);
                                product.name = reader.IsDBNull(nameField) ? "": reader.GetString(nameField);
                                product.BrandName = reader.IsDBNull(brandNameField) ? "" : reader.GetString(brandNameField);
                                product.SalePrice = reader.IsDBNull(salePriceField) ? 0: reader.GetDecimal(salePriceField);
                                product.stock = reader.IsDBNull(stockField) ? 0 : reader.GetInt32(stockField);
                                product.denomination = reader.IsDBNull(stockField) ? "":( reader.GetInt32(stockField) > 50 ? "Alto" : "Bajo");
                                products.Add(product);

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
            return products;
        }
    }
}
