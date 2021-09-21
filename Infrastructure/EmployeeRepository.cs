using Contracts;
using DataModel;
using DataModel.common;
using DataModel.common.Enum;
using DataModel.DTO;
using DataModel.Entity;
using Infrastructure.Validators;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
namespace Infrastructure
{
    public class EmployeeRepository : IEmployee
    {
        private readonly EmployeeDbContext _dbContext;
        private readonly EmployeeValidator _employeeValidator;
        //  private readonly IMapper _mapper;
        public EmployeeRepository(EmployeeDbContext dbContext)
        {
            //_mapper = mapper;
            _dbContext = dbContext;
            _employeeValidator = new EmployeeValidator(_dbContext);
        }
        public ResponseModel<Employee> Create(CreateEmployeeDto employeeDto)
        {
            using (var transaction = _dbContext.Database.BeginTransaction())
            {
                try
                {
                    //insert employee info  into employee table 
                    var response = new ResponseModel<Employee>();
                    var result = _employeeValidator.Validate(employeeDto);
                    if (!result.IsValid)
                    {
                        response.TotalCount = 0;
                        response.Success = false;
                        response.Error = GetNotFoundError();
                        return response;
                    }
                    // insert into Address Table
                    var address = employeeDto.EmployeeAddress;

                    var empAdress = new Address()
                    {
                        CurrentStatus = 1,
                        Mobile = employeeDto.EmployeeAddress.Mobile,
                        Telephone = employeeDto.EmployeeAddress.Telephone,
                        Zone = employeeDto.EmployeeAddress.Zone,
                        Region = employeeDto.EmployeeAddress.Region,
                        Woreda = address.Woreda // since address contains address object value
                    };

                    _dbContext.Addresses.Add(empAdress);
                    _dbContext.SaveChanges();
                    //Converting employee DTO to Employee model 
                    Employee newEmployee = new()
                    {
                        FirstName = employeeDto.FirstName,
                        LastName = employeeDto.LastName,
                        DepartmentId = employeeDto.DepartmentId,
                        Gender = employeeDto.Gender,
                        BirthDate = employeeDto.BirthDate,
                        // AddressId = employeeDto.AddressId
                        AddressId = empAdress.AddressId,
                        CurrentStatus = (int)EmployeeStatus.Employee_Active
                    };
                    // Inserting into employee table
                    _dbContext.Add(newEmployee);
                    _dbContext.SaveChanges();


                    // updating employee Address value with the new Address Id
                    //var employeeToBeUpdated = GetEmployee(newEmployee.Id);

                    //employeeToBeUpdated.AddressId = empAdress.AddressId;
                    //_dbContext.Employees.Update(employeeToBeUpdated);
                    //_dbContext.SaveChangesAsync();

                    response.Success = true;
                    response.Error = null;
                    response.TotalCount = 1;
                    response.Data = new List<Employee>()
                            {
                              GetEmployee(newEmployee.Id)
                            };

                    transaction.Commit();
                    return response;
                }
                catch (Exception ex)
                {
                    transaction.Rollback();
                    throw new Exception(ex.Message);
                }
            }
        }

        public ResponseModel<Employee> Delete(int id)
        {
            var response = new ResponseModel<Employee>();

            if (EmployeeExists(id))
            {
                _dbContext.Employees.Remove(GetEmployee(id));
                _dbContext.SaveChanges();
                response.Success = false;
                response.Error = GetNotFoundError();
                return response;
            }
            var employee = _dbContext.Employees.Find(id);
            response.Data = new List<Employee> { employee };
            response.Success = true;
            return response;
        }

        public ResponseModel<Employee> Get(int id)
        {
            var response = new ResponseModel<Employee>();
            if (!EmployeeExists(id))
            {
                response = new ResponseModel<Employee>()
                {
                    Data = null,
                    Success = false,
                    TotalCount = 0,
                    Error = GetNotFoundError()
                };
                return response;
            }
            response.Data = new List<Employee>() { GetEmployee(id) };
            response.Success = true;
            response.TotalCount = 1;
            response.Error = null;
            return response;
        }

        public ResponseModel<Employee> GetAll()
        {
            var response = new ResponseModel<Employee>
            {
                Data = _dbContext.Employees
                .Include(x => x.VDepartment)
                .Include(x => x.VAddress)
                .ToList()
            };
            response.Success = response.Data.Count > 0;
            return response;
        }

        public async Task<ResponseModel<Employee>> Update(int id, Employee UpdatedData)
        {
            var response = new ResponseModel<Employee>();
            if (!EmployeeExists(id))
            {
                response.Success = false;
                response.Error = GetNotFoundError();
                return response;
            }
            Employee oldData = GetEmployee(id);
            oldData.DepartmentId = UpdatedData.DepartmentId;
            _dbContext.Update(oldData);
            await _dbContext.SaveChangesAsync();
            response.Success = true;
            response.Data = new List<Employee> { GetEmployee(id) };
            return response;
        }

        private static ErrorModel GetNotFoundError()
        {
            return new ErrorModel
            {
                ErrorCode = StatusCodes.Status404NotFound,
                ErrorDescription = "Employee not found",
                ErrorMessage = "Employee with the given Id does not exist"
            };
        }
        private bool EmployeeExists(int id)
        {
            return _dbContext.Employees.Where(x => x.Id == id).Any();
        }
        private Employee GetEmployee(int id)
        {
            return _dbContext.Employees.Find(id);
        }
    }
}
