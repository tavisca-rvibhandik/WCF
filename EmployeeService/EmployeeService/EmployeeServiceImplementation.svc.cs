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
        private static List<Employee> _list = new List<Employee>();

        public string CreateEmployee(Employee e)
        {
            try
            {
                //Check if employee with specified id is already present
                var employee = _list.Where(t => t.Id == e.Id).FirstOrDefault();
                if (employee != null)
                    throw new Exception();
                _list.Add(e);
                return "Record successfully added";
            }
            catch
            {
                FaultExceptionContract faultcontract = new FaultExceptionContract();
                faultcontract.Message = "Record Already Present with same ID";
                throw new FaultException<FaultExceptionContract>(faultcontract, new FaultReason("Record Already Present with same ID"));
            }

        }

        public string AddRemarks(int id, string remarks)
        {
            try
            {
                //Check if employee with specified id is present
                var employee = _list.Where(t => t.Id == id).FirstOrDefault();
                if (employee != null)
                {
                    employee.Remarks.Add(System.DateTime.Now, remarks);
                    return "Remark added successfully";
                }

                throw new Exception();
            }
            catch
            {
                FaultExceptionContract faultcontract = new FaultExceptionContract();
                faultcontract.Message = "Record not found";
                throw new FaultException<FaultExceptionContract>(faultcontract, new FaultReason("Record not found"));
            }
        }

        public List<Employee> GetEmployees()
        {
            try
            {
                //Check if employee list is empty
                if (_list.Count == 0)
                    throw new Exception();
                return _list;
            }
            catch
            {
                FaultExceptionContract faultcontract = new FaultExceptionContract();
                faultcontract.Message = "No employees added";
                throw new FaultException<FaultExceptionContract>(faultcontract, new FaultReason("No employees added"));
            }
        }

        public Employee GetEmployee(int id)
        {
            try
            {
                var employee = _list.Where(t => t.Id == id).FirstOrDefault();
                if (employee != null)
                    return employee;
                throw new Exception();
            }
            catch
            {
                FaultExceptionContract faultcontract = new FaultExceptionContract();
                faultcontract.Message = "No record found for specified Id";
                throw new FaultException<FaultExceptionContract>(faultcontract, new FaultReason("No record found for specified Id"));
            }
        }

        public List<Employee> GetEmployee(string name)
        {
            List<Employee> employeeList = new List<Employee>();
            int flag = 0;
            try
            {
                //Check if employees with specified name are present
                employeeList.AddRange(_list.FindAll(t => String.Equals(t.Name, name, StringComparison.OrdinalIgnoreCase)));
                if (employeeList.Count != 0)
                {
                    flag = 1;
                }
                if (flag == 0)
                    throw new Exception();
                return employeeList;
            }
            catch
            {
                FaultExceptionContract faultcontract = new FaultExceptionContract();
                faultcontract.Message = "No record found for specified Name";
                throw new FaultException<FaultExceptionContract>(faultcontract, new FaultReason("No record found for specified Name"));
            }
        }


        public List<Employee> GetEmployeesByRemark(string remark)
        {
            List<Employee> remarkEmployeeList = new List<Employee>();
            int flag = 0;
            try
            {
                //Check if employees with specified remark are present
                remarkEmployeeList.AddRange(_list.Where(t => t.Remarks.ContainsValue(remark)));
                if (remarkEmployeeList.Count != 0)
                {
                    flag = 1;
                }
                if (flag == 0)
                    throw new Exception();
                return remarkEmployeeList;
            }
            catch
            {
                FaultExceptionContract faultcontract = new FaultExceptionContract();
                faultcontract.Message = "No record found for specified Remark";
                throw new FaultException<FaultExceptionContract>(faultcontract, new FaultReason("No record found for specified Remark"));
            }
        }
    }
}
