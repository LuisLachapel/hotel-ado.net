using System.Data;
using System.Data.SqlClient;


namespace Persistence.Product
{
    public class Save: DBConnection
    {
        public int SaveProduct(Entity.Product product)
        {
            int result = 0;
            using(SqlConnection connection = new SqlConnection(db_connection))
            {
                
                try
                {
                    connection.Open();
                    using (SqlCommand command = new SqlCommand("uspGuardarProducto", connection))
                    {
                        command.CommandType = CommandType.StoredProcedure;
                        command.Parameters.AddWithValue("@id", product.id);
                        command.Parameters.AddWithValue("@nombre", product.name);
                        command.Parameters.AddWithValue("@idmarca", product.idBrand);
                        command.Parameters.AddWithValue("@descripcion", product.description);
                        command.Parameters.AddWithValue("@preciocompra", product.buyPrice);
                        command.Parameters.AddWithValue("@precioventa", product.SalePrice);
                        command.Parameters.AddWithValue("@stock", product.stock);
                        command.Parameters.AddWithValue("@idcategoria", product.idCategory);
                        result = command.ExecuteNonQuery();

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
