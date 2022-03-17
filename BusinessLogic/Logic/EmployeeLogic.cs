using BusinessEntities;
using DAL;
using System;
using System.Collections.Generic;
using System.Text;

namespace BusinessLogic
{
    public class EmployeeLogic:IEmployeeLogic
    {
        private GenericRepository<Employee> EmployeeRepository { get; set; }
        private IUnitOfWork _unitOfWork;
        public EmployeeLogic(IUnitOfWork UnitOfWork)
        {
            _unitOfWork = UnitOfWork;
            EmployeeRepository = UnitOfWork.GetRepository<Employee>();
        }

        public List<Employee> GetAllEmployees()
        {
            return (List<Employee>)EmployeeRepository.All();
        }

        public void AddEmployee(Employee Employee)
        {
            EmployeeRepository.Add(Employee);
            _unitOfWork.CompleteAsync();
        }

        public Employee GetEmployee(int EmployeeId)
        {
            return EmployeeRepository.GetById(EmployeeId);
        }

        public bool UpdateEmployee(Employee Employee)
        {
            EmployeeRepository.Update(Employee);
            _unitOfWork.CompleteAsync();
            return true;
        }

        public bool DeleteEmployee(int EmployeeId)
        {
            EmployeeRepository.Delete(EmployeeId);
            _unitOfWork.CompleteAsync();
            return true;
        }
    }
}
