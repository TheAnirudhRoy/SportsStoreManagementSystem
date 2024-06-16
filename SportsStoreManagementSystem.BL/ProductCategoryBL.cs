using SportsStoreManagementSystem.Entities;
using SportsStoreManagementSystem.DAL;

namespace SportsStoreManagementSystem.BL
{
    public class ProductCategoryBL
    {
        ProductCategoryDAL productCategoryObj = new ProductCategoryDAL();

        public IEnumerable<ProductCategory> GetAllProductCategoriesBL()
        {
            return productCategoryObj.GetAllProductCategoriesDAL();
        }

        public bool AddProductCategoryBL(ProductCategory productCategory)
        {
            return productCategoryObj.AddProductCategoryDAL(productCategory);
        }
    }
}
