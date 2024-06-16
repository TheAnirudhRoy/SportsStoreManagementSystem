using SportsStoreManagementSystem.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SportsStoreManagementSystem.DAL;

using Microsoft.EntityFrameworkCore;

namespace SportsStoreManagementSystem.BL
{
    public class SalesHistoryBL
    {

        public IEnumerable<SalesHistory> GetSalesHistoryDetailsBL()
        {
           SalesHistoryDAL salesHistory = new SalesHistoryDAL();
            return salesHistory.GetSalesHistoryDetailsDAL();

        }

        public SalesHistory GetSalesHistoryBL(int id)
        {
            SalesHistoryDAL salesHistory = new SalesHistoryDAL();
            return salesHistory.GetSalesHistoryDAL(id);
        }

        public void AddSalesHistoryBL(SalesHistory salesHistory)
        {
            SalesHistoryDAL salesHistor = new SalesHistoryDAL();
            salesHistor.AddSalesHistoryDAL(salesHistory);
        }

        public SportsStoreEnum EditSalesHistoryBL(int id, SalesHistory salesHistory)
        {
            SalesHistoryDAL salesHistor = new SalesHistoryDAL();
            return salesHistor.EditSalesHistoryDAL(id, salesHistory);

        }

    }
}
