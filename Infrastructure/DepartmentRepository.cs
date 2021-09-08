﻿using Contracts;
using DataModel;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Infrastructure
{
    public class DepartmentRepository : IDepartment
    {
        private readonly EmployeeDbContext _dbContext;
        public DepartmentRepository(EmployeeDbContext dbContext)
        {
            _dbContext = dbContext;
        }
        public int Create(Department department)
        {
           
            _dbContext.Departments.Add(department);
             _dbContext.SaveChanges();
            return department.DepartmentId;
        }

        public bool Delete(int id)
        {
            var existing = _dbContext.Departments.Find(id);       
                _dbContext.Departments.Remove(existing);
                _dbContext.SaveChanges();
                return true;
        }

        public Department Get(int id)
        {
            throw new NotImplementedException();
        }

        public List<Department> GetAll()
        {
            return _dbContext.Departments.ToList();

        }

        public int Update(int id, Department department)
        {
            throw new NotImplementedException();
        }
    }
}
