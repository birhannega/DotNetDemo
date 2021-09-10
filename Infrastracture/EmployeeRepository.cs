using Contracts;
using DataModel;
using DataModel.Common;
using Infrastracture.Validators;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Infrastracture
{
    public class EmployeeRepository : IEmployee
    {
        private readonly EmployeeDbContext _dbContext;
        //private readonly EmployeeValidator _employeeValidator;
        public EmployeeRepository(EmployeeDbContext dbContext)
        {
            _dbContext = dbContext;
        }
        public ResponseModel<Employee> Create(Employee employee)
        {
            var response = new ResponseModel<Employee>();
            var validator = new EmployeeValidator(_dbContext);
            var result = validator.Validate(employee);
            //var result = _employeeValidator.Validate(employee);
            if (!result.IsValid) {
                response.TotalCount = 0;
                response.Sucess = false;
                response.Error = new ErrorModel()
                {
                    ErrorCode = 0,
                    ErrorDescription = "please fix validation Error",
                    ErrorMessage = result.Errors[0].ErrorMessage
                };
                return response;
                    }
            _dbContext.Employees.Add(employee);
            _dbContext.SaveChanges();
            response.Sucess = true;
            response.Error = null;
            response.TotalCount = 1;
            response.Data = new List<Employee>()
            {
                Get(employee.Id)
             };
            return response;
        }

        public bool Delete(int id)
        {
            var existing = _dbContext.Employees.Find(id);
            if (existing != null)
            {
                _dbContext.Employees.Remove(existing);
                _dbContext.SaveChanges();
                return true;
            }
            return false;
            
        }

        public Employee Get(int id)
        {
            return _dbContext.Employees.Find(id);
        }

        public List<Employee> GetAll()
        {
            return _dbContext.Employees
                .Where(x=>x.Id>0)
                .Include(x=>x.Department)
                .ToList();
        }

        public bool Update(int id, Employee employee)
        {
            var existing = _dbContext.Employees.Find(id);
            if (existing != null)
            {
              existing.FirstName = employee.FirstName;
            existing.LastName = employee.LastName;
            existing.Gender = employee.Gender;
            existing.BirtDate = employee.BirtDate;
            _dbContext.Employees.Update(existing);
            _dbContext.SaveChanges();
            return true;
            }
            return false;

        }

    }
}
