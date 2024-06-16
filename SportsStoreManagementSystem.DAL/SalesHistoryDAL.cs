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
    public class SalesHistoryDAL
    {
        SportsDbContext db = new SportsDbContext();

        public IEnumerable<SalesHistory> GetSalesHistoryDetailsDAL() { 
        
            return db.SalesHistories.ToList();
        }

        public SalesHistory GetSalesHistoryDAL(int id)
        {
           return db.SalesHistories.Find(id);
        }

        public  void AddSalesHistoryDAL(SalesHistory salesHistory)
        {
            db.SalesHistories.Add(salesHistory);
            db.SaveChanges();

        }

        public SportsStoreEnum EditSalesHistoryDAL(int id, SalesHistory salesHistory)
        {
            if (id != salesHistory.SalesId)
            {
                return SportsStoreEnum.BadRequest;
            }
            db.Entry(salesHistory).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!SalesHistoryExists(id))
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

        private bool SalesHistoryExists(int id)
        {
            return db.SalesHistories.Any(e => e.SalesId == id);
        }

    }
}
