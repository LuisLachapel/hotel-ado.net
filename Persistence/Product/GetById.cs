using System.Data;
using System.Data.SqlClient;

namespace Persistence.Product
{
    public class GetById: DBConnection
    {
        public Entity.Product GetProduct(int id)
        {
            Entity.Product product = new Entity.Product();
            using(SqlConnection connection = new SqlConnection(db_connection))
            {
                
                try
                {
                    connection.Open();
                    using (SqlCommand command = new SqlCommand("uspRecuperarProducto", connection))
                    {
                        command.CommandType = CommandType.StoredProcedure;
                        command.Parameters.AddWithValue("@id", id);
                        SqlDataReader reader = command.ExecuteReader();
                        if (reader.HasRows)
                        {
                            int idField = reader.GetOrdinal("IIDPRODUCTO");
                            int nameField = reader.GetOrdinal("NOMBRE");
                            int idBrandField = reader.GetOrdinal("IIDMARCA");
                            int descriptionField = reader.GetOrdinal("DESCRIPCION");
                            int buyPriceField = reader.GetOrdinal("PRECIOCOMPRA");
                            int salePriceField = reader.GetOrdinal("PRECIOVENTA");
                            int stockField = reader.GetOrdinal("STOCK");
                            int idCategoryField = reader.GetOrdinal("IIDCATEGORIA");
                            while (reader.Read())
                            {
                                product.id = reader.IsDBNull(idField) ? 0 :reader.GetInt32(idField);
                                product.name = reader.IsDBNull(nameField) ? "" : reader.GetString(nameField);
                                product.idBrand = reader.IsDBNull(idBrandField) ? 0 : reader.GetInt32(idBrandField);
                                product.description = reader.IsDBNull(descriptionField) ? "" : reader.GetString(descriptionField);
                                product.buyPrice = reader.IsDBNull(buyPriceField) ? 0: reader.GetDecimal(buyPriceField);
                                product.SalePrice = reader.IsDBNull(salePriceField) ? 0: reader.GetDecimal(salePriceField);
                                product.stock = reader.IsDBNull(stockField) ? 0 : reader.GetInt32(stockField);
                                product.idCategory = reader.IsDBNull(idCategoryField) ? 0 : reader.GetInt32(idCategoryField);

                            }
                        }
                    }
                }
                catch (Exception)
                {

                    connection.Close();
                }
            }
            return product;
        }

    }
}
