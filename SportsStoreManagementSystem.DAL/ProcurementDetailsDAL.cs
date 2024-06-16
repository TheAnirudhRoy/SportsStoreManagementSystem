using Microsoft.EntityFrameworkCore;
using System.Linq;
using SportsStoreManagementSystem.DAL.Models;
using SportsStoreManagementSystem.Entities;

namespace SportsStoreManagementSystem.DAL
{
    public class ProcurementDetailsDAL
    {
        readonly SportsDbContext db = new SportsDbContext();

        public IEnumerable<ProcurementDetail> GetAllProcurementDetailsDAL()
        {
            return db.ProcurementDetails
                                 .Include(pd => pd.Product)
                                 .Include(pd => pd.Sup)
                                 .ToList();
        }

        public bool AddProcurementDetailDAL(ProcurementDetail procurementDetail)
        {
            int? Price = Convert.ToInt32(GetProductPrice(procurementDetail.ProductId));
            procurementDetail.TotalPrice = procurementDetail.Qty * Price;

            db.ProcurementDetails.Add(procurementDetail);
            int noOfRowsAffected = db.SaveChanges();
            if (noOfRowsAffected > 0)
            {
                UpdateInventory(procurementDetail);
                return true;
            }
            else
                return false; 
        }

        private int? GetProductPrice(int id)
        {
            Product? productDetail = db.Products.Find(id);
            if (productDetail == null)
            {
                return null;
            }
            else
            {
                return productDetail.Price;
            }
        }

        private void UpdateInventory(ProcurementDetail procurementDetail)
        {
            var inventory = new ProductInventory
            {
                SupId = Convert.ToInt32(procurementDetail.SupId), //Since Allowed Null is On in Proc.SupId
                ProductId = Convert.ToInt32(procurementDetail.ProductId), //Since Allowed Null is On in Proc.SupId
                Stocks = procurementDetail.Qty
            };

            var existing = db.ProductInventories.FirstOrDefault(x => x.ProductId == inventory.ProductId && x.SupId == inventory.SupId);

            if (existing != null)
            {
                existing.Stocks += inventory.Stocks;
                db.Entry(existing).State = EntityState.Modified;
                db.SaveChanges();
            }
            else
            {
                db.ProductInventories.Add(inventory);
                db.SaveChanges();
            }
        }
    }
}
