using Microsoft.EntityFrameworkCore;
using SportsStoreManagementSystem.DAL.Models;
using SportsStoreManagementSystem.Entities;

namespace SportsStoreManagementSystem.DAL
{
    public class ProductCategoryDAL
    {
        readonly SportsDbContext db = new SportsDbContext();

        public IEnumerable<ProductCategory> GetAllProductCategoriesDAL()
        {
            return db.ProductCategories.ToList();
        }

        public bool AddProductCategoryDAL(ProductCategory productCategory)
        {
            if (ProductInventoryExists(productCategory.CategoryName))
            {
                return false;
            }
            db.ProductCategories.Add(productCategory);
            db.SaveChanges();
            return true;
        }

        private bool ProductInventoryExists(string catName)
        {
            return db.ProductCategories.Any(e => e.CategoryName == catName);
        }
    }
}
