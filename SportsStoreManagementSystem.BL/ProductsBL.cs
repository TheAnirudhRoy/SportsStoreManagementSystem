using SportsStoreManagementSystem.Entities;
using SportsStoreManagementSystem.DAL;

namespace SportsStoreManagementSystem.BL
{
    public class ProductsBL
    {
        readonly ProductsDAL productsObj = new ProductsDAL();

        public IEnumerable<Product> GetProductsBL()
        {
            return productsObj.GetProductsDAL();
        }

        public Product GetProductBL(int id)
        {
            return productsObj.GetProductDAL(id);
        }

        public bool PostProductBL(Product product)
        {
           return productsObj.PostProductDAL(product);
        }

        public SportsStoreEnum EditProductsBL(int id, Product product)
        {
            return productsObj.EditProductDAL(id, product);
        }

        public IEnumerable<Product> GetProductsByCategoryInInventoryBL(int categoryId)
        {
            return productsObj.GetProductsByCategoryInInventoryDAL(categoryId);
        }

        public IEnumerable<Product> GetProductsBySupplierBL(int supplierId)
        {
            return productsObj.GetProductsBySupplierDAL(supplierId);
        }
    }
}
