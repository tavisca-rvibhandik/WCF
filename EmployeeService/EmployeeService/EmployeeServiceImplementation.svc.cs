using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.ServiceModel.Web;
using System.Text;

namespace EmployeeService
{
    public class EmployeeServiceImplementation : ICreateEmployeeService, IRetrieveEmployeeService
    {
        static List<Employee> _List = new List<Employee>();

        public string CreateEmployee(Employee e)
        {
            foreach (var item in _List)
            {
                if (item.Id == e.Id)
                    return "Record Already Present with same ID";
            }
            _List.Add(e);
            return "Record successfully added";
        }

        public string AddRemarks(int id, string remarks)
        {
            foreach (var item in _List)
            {
                if (item.Id == id)
                {
                    item.RemarkDate = System.DateTime.Now;
                    item.RemarkText.Add(remarks);
                    return "Remark added successfully";
                }
            }
            return "Record Not Found";
        }

        public List<Employee> GetEmployees()
        {
            return _List;
        }

        public Employee GetEmployee(int Id)
        {
            foreach (var item in _List)
            {
                if (item.Id == Id)
                    return item;
            }
            return null;
        }

        public Employee GetEmployee(string Name)
        {

            foreach (var item in _List)
            {
                if (item.Name == Name)
                    return item;
            }
            return null;
        }
    }
}
