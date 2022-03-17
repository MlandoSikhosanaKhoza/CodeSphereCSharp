using BusinessEntities;
using System;
using System.Collections.Generic;
using System.Text;

namespace BusinessLogic
{
    public interface ICustomerLogic
    {
        List<Customer> GetAllCustomers();
        void AddCustomer(Customer Customer);
        Customer GetCustomer(int CustomerId);
        bool UpdateCustomer(Customer Customer);
        bool DeleteCustomer(int CustomerId);
        List<Customer> SearchForCustomers(string Search);
        bool MobileNumberExists(string Mobile);
    }
}
