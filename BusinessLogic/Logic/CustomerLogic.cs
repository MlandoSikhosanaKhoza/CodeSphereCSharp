using BusinessEntities;
using DAL;
using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;
namespace BusinessLogic
{
    public class CustomerLogic:ICustomerLogic
    {
        private GenericRepository<Customer> CustomerRepository { get; set; }
        private IUnitOfWork _unitOfWork;
        public CustomerLogic(IUnitOfWork UnitOfWork)
        {
            _unitOfWork = UnitOfWork;
            CustomerRepository = UnitOfWork.GetRepository<Customer>();
        }
        public List<Customer> GetAllCustomers()
        {
            return (List<Customer>)CustomerRepository.All();
        }

        public List<Customer> SearchForCustomers(string Search)
        {
            Search = Search.ToLower();
            return (CustomerRepository.All()
                .Where(s => s.Name.ToLower().Contains(Search) ||
                 s.Surname.ToLower().Contains(Search))).ToList();
        }
        public bool MobileNumberExists(string Mobile)
        {
            if (string.IsNullOrEmpty(Mobile))
            {
                return false;
            }
            return CustomerRepository.All().Where(u => u.Mobile.Equals(Mobile)).Any();
        }
        public void AddCustomer(Customer Customer)
        {
            CustomerRepository.Add(Customer);
            _unitOfWork.CompleteAsync();
        }
        public bool UpdateCustomer(Customer Customer)
        {
            CustomerRepository.Update(Customer);
            _unitOfWork.CompleteAsync();
            return true;
        }
        public bool DeleteCustomer(int CustomerId)
        {
            CustomerRepository.Delete(CustomerId);
            _unitOfWork.CompleteAsync();
            return true;
        }
        public Customer GetCustomer(int CustomerId)
        {
            return CustomerRepository.GetById(CustomerId);
        }

        public Customer GetCustomerByMobileNumber(string MobileNumber)
        {
            Customer customer = CustomerRepository.All().Where(c => c.Mobile.Equals(MobileNumber)).First();
            return customer;
        }
    }
}
