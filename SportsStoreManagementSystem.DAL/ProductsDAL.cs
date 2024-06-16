using Microsoft.EntityFrameworkCore;
using System.Linq;
using SportsStoreManagementSystem.DAL.Models;
using SportsStoreManagementSystem.Entities;

namespace SportsStoreManagementSystem.DAL
{
    public class ProductsDAL
    {
        readonly SportsDbContext db = new SportsDbContext();

        public IEnumerable<Product> GetProductsDAL()
        {
            return db.Products
                .Include(p => p.ProcurementDetails)
                .Include(p => p.ProductInventories)
                .Include(p => p.SalesItems)
                .ToList();
        }

        public Product GetProductDAL(int id)
        {
            var product = db.Products
                .Include(p => p.ProcurementDetails)
                .Include(p => p.ProductInventories)
                .Include(p => p.SalesItems)
                .FirstOrDefault(p => p.ProductId == id);

            return product;
        }

        public bool PostProductDAL(Product product)
        {
            db.Products.Add(product);
            int noOfRowsAffected = db.SaveChanges();
            if (noOfRowsAffected > 0)
                return true;
            else
                return false;
        }

        public SportsStoreEnum EditProductDAL(int id, Product product)
        {
            if (id != product.ProductId)
            {
                return SportsStoreEnum.BadRequest;
            }

            db.Entry(product).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ProductExistsDAL(id))
                {
                    return SportsStoreEnum.NotFound;
                }
                else
                {
                    throw;
                }
            }
            return SportsStoreEnum.NoContent;
        }

        private bool ProductExistsDAL(int id)
        {
            return db.Products.Any(p => p.ProductId == id);
        }

        public IEnumerable<Product> GetProductsByCategoryInInventoryDAL(int categoryId)
        {
            var products = from P in db.Products
                           join
                           I in db.ProductInventories
                           on
                           P.ProductId equals I.ProductId
                           where P.CategoryId == categoryId
                           select P;

            return products.ToList();
        }

        public IEnumerable<Product> GetProductsBySupplierDAL(int supplierId)
        {
            var products = db.Products
            .Include(p => p.ProductInventories)
            .Include(p => p.ProcurementDetails)
            .Where(p => p.ProcurementDetails.Any(pd => pd.SupId == supplierId) && p.ProductInventories.Any(pi => pi.Stocks > 0))
            .ToList();

            return products.ToList();
        }
    }
}
