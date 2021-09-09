﻿using Contracts;
using DataModel;
using DataModel.common;
using Infrastructure.Validators;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
namespace Infrastructure
{
    public class EmployeeRepository : IEmployee
    {
        private readonly EmployeeDbContext _dbContext;
        private readonly EmployeeValidator _employeeValidator;
        public EmployeeRepository(EmployeeDbContext dbContext)
        {
            _dbContext = dbContext;
            _employeeValidator = new EmployeeValidator(_dbContext);   
        }
        //ghgh
        public ResponseModel<Employee> Create(Employee employee)
        {
            var response = new ResponseModel<Employee>();
            var result = _employeeValidator.Validate(employee);
            if (!result.IsValid)
            {
                response.TotalCount = 0; response.Success = false;
                response.Error = new ErrorModel()
                {
                    ErrorCode = 0, ErrorDescription = "Please fix validation errors",
                    ErrorMessage = result.Errors[0].ErrorMessage
                };
                return response;
            }
           _dbContext.Add(employee);
           _dbContext.SaveChanges();

            response.Success=true; 
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
            throw new System.NotImplementedException();
        }

        public Employee Get(int id)
        {
          //  return _dbContext.Employees.Where(x=>x.Id==id).FirstOrDefault();
          return _dbContext.Employees.FirstOrDefault(x => x.Id == id);

        }

        public List<Employee> GetAll()
        {
            return _dbContext.Employees
                .Where(x=>x.Id>1)
                .Include(x=>x.VDepartment)
                .ToList();
        }

        public async Task<bool> Update(int id, Employee UpdatedData)
        {
            Employee oldData =await _dbContext.Employees.FindAsync(id);
            if(oldData is null)
            {
                return false;
            }
            oldData.DepartmentId= UpdatedData.DepartmentId;
            _dbContext.Update(oldData);
           await _dbContext.SaveChangesAsync();
            return true;
        }
    }
}
