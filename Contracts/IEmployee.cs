using DataModel;
using DataModel.Common;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Contracts
{
    public interface IEmployee
    {
        public ResponseModel<Employee> Create(Employee Employee);
        public List<Employee> GetAll();
        public Employee Get(int id);
        public bool Update(int id,Employee employee);
        public bool Delete(int id);
    }
}
