using Microsoft.EntityFrameworkCore;
using SportsStoreManagementSystem.DAL.Models;
using SportsStoreManagementSystem.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SportsStoreManagementSystem.DAL
{
    public class SupplierDetailsDAL
    {
        SportsDbContext db = new SportsDbContext();

        public IEnumerable<SupplierDetail> GetSupplierDetailsDAL()
        {
            return  db.SupplierDetails.ToList();
        }

        public SupplierDetail GetSupplierDetailDAL(int id)
        {
            return db.SupplierDetails.Find(id);
        }

        public void AddSupplierDetailDAL(SupplierDetail supplierDetail)
        {
            db.SupplierDetails.Add(supplierDetail);
            db.SaveChanges();
        }

        public  SportsStoreEnum EditSupplierDetailDAL(SupplierDetail supplierDetail)
        {
          
            db.Entry(supplierDetail).State = EntityState.Modified;

            try
            {
                db.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!SupplierDetailExists(supplierDetail.SupId))
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

        public IEnumerable<SupplierDetail> GetSupplierDetailsByProductIdDAL(int productId)
        {
            var suppliers = from S in db.SupplierDetails
                            join
                            I in db.ProductInventories
                            on
                            S.SupId equals I.SupId
                            where I.ProductId == productId
                            select S;

            return suppliers.ToList();
        }

        private bool SupplierDetailExists(int id)
        {
            return db.SupplierDetails.Any(e => e.SupId == id);
        }

    }
}
