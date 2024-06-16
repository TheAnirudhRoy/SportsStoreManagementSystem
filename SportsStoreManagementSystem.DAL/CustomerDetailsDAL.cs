using Microsoft.EntityFrameworkCore;
using SportsStoreManagementSystem.DAL.Models;
using SportsStoreManagementSystem.Entities;

namespace SportsStoreManagementSystem.DAL
{
    public class CustomerDetailsDAL
    {
        SportsDbContext db = new SportsDbContext();


        public IEnumerable<CustomerDetail> GetCustomerDetailsDAL()
        {
            return db.CustomerDetails.ToList();
        }


        public CustomerDetail GetCustomerDetailDAL(int id)
        {
            return db.CustomerDetails.Find(id);
        }

        public void AddCustomerDetailDAL(CustomerDetail customerDetail)
        {
            db.CustomerDetails.Add(customerDetail);
            db.SaveChanges();
        }

        public SportsStoreEnum EditCustomerDetailDAL(int id, CustomerDetail customerDetail)
        {
            if (id != customerDetail.CustomerId)
            {
                return SportsStoreEnum.BadRequest;
            }

            db.Entry(customerDetail).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!CustomerDetailExists(id))
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
        public CustomerDetail GetCustomerByContactDAL(string contact)
        {
            var customerDetail =  db.CustomerDetails.FirstOrDefault(c => c.CustomerContact == contact);
            return customerDetail;
        }
        private bool CustomerDetailExists(int id)
        {
            return db.CustomerDetails.Any(e => e.CustomerId == id);
        }

    }
}
