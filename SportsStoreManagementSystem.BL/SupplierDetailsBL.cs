using SportsStoreManagementSystem.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SportsStoreManagementSystem.DAL;

namespace SportsStoreManagementSystem.BL
{
    public class SupplierDetailsBL
    {
        public IEnumerable<SupplierDetail> GetSupplierDetailsBL()
        {
            SupplierDetailsDAL supplierDetails = new SupplierDetailsDAL();
            return supplierDetails.GetSupplierDetailsDAL();
        }

        public SupplierDetail GetSupplierDetailBL(int id)
        {
            SupplierDetailsDAL supplierDetails = new SupplierDetailsDAL();
            return supplierDetails.GetSupplierDetailDAL(id);
        }

        public void AddSupplierDetailBL(SupplierDetail supplierDetail)
        {
            SupplierDetailsDAL supplierDetails = new SupplierDetailsDAL();
            supplierDetails.AddSupplierDetailDAL(supplierDetail);
        }

        public SportsStoreEnum EditSupplierDetailBL(SupplierDetail supplierDetail)
        {
            SupplierDetailsDAL supplierDetails = new SupplierDetailsDAL();
            return supplierDetails.EditSupplierDetailDAL(supplierDetail);
        }

        public IEnumerable<SupplierDetail> GetSupplierDetailsByProductIdBL(int productId)
        {
            SupplierDetailsDAL supplierDetails = new SupplierDetailsDAL();
            return supplierDetails.GetSupplierDetailsByProductIdDAL(productId);
        }

    }
}
