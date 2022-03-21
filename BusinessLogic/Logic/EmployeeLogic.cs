using BusinessEntities;
using DAL;
using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;

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
            return EmployeeRepository.All().Where(e => !e.IsDeleted).ToList();
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
            Employee employee = GetEmployee(EmployeeId);
            employee.IsDeleted = true;
            UpdateEmployee(employee);
            _unitOfWork.CompleteAsync();
            return true;
        }
    }
}
