
namespace Persistence.Product
{
    public class ProductBrand
    {
        
        public Entity.ProductBrand list()
        {
            Brand.GetAll brand = new Brand.GetAll();
            Product.GetAll product = new Product.GetAll();
            Entity.ProductBrand productsBrand = new Entity.ProductBrand();
            productsBrand.Brands= brand.List();
            productsBrand.Products = product.List();
            return productsBrand;
        }
    }
}
