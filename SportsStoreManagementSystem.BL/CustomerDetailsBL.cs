using SportsStoreManagementSystem.Entities;
using SportsStoreManagementSystem.DAL;

namespace SportsStoreManagementSystem.BL
{
    public class CustomerDetailsBL
    {

        public IEnumerable<CustomerDetail> GetCustomerDetailsBL()
        {
            CustomerDetailsDAL customerDetails = new CustomerDetailsDAL();
            return customerDetails.GetCustomerDetailsDAL();
            
        }
        public CustomerDetail GetCustomerDetailBL(int id)
        {
            CustomerDetailsDAL customerDetails = new CustomerDetailsDAL();
            return customerDetails.GetCustomerDetailDAL(id);
        }

        public void AddCustomerDetailBL(CustomerDetail customerDetail)
        {
            CustomerDetailsDAL customerDetails = new CustomerDetailsDAL();
            customerDetails.AddCustomerDetailDAL(customerDetail);
        }


        public SportsStoreEnum EditCustomerDetailBL(int id, CustomerDetail customerDetail)
        {
            CustomerDetailsDAL customerDetails = new CustomerDetailsDAL();
            return customerDetails.EditCustomerDetailDAL(id,customerDetail);

        }

        public CustomerDetail GetCustomerByContactBL(string contact)
        {
            CustomerDetailsDAL customerDetails = new CustomerDetailsDAL();
            return customerDetails.GetCustomerByContactDAL(contact);
        }
    }
}
