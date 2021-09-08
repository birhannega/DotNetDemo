﻿using DataModel;
using System.Collections.Generic;

namespace Contracts
{
    public interface IDepartment

    {
        public int Create(Department department);  
        public int Update(int id,Department department);  
        public  List<Department> GetAll();  
        public Department Get(int id);  
        public bool Delete(int id);  
    }
}
