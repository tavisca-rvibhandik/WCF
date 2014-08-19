using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.ServiceModel.Web;
using System.Text;

namespace EmployeeService
{
    [ServiceContract]
    public interface ICreateEmployeeService
    {
        [OperationContract]
        [FaultContract(typeof(FaultExceptionContract))]
        string CreateEmployee(Employee employee);

        [OperationContract]
        [FaultContract(typeof(FaultExceptionContract))]
        string AddRemarks(int id, string remarks);
    }

    [ServiceContract]
    public interface IRetrieveEmployeeService
    {
        [OperationContract]
        [FaultContract(typeof(FaultExceptionContract))]
        List<Employee> GetEmployees();

        [OperationContract(Name = "SearchById")]
        [FaultContract(typeof(FaultExceptionContract))]
        Employee GetEmployee(int Id);

        [OperationContract(Name = "SearchByName")]
        [FaultContract(typeof(FaultExceptionContract))]
        List<Employee> GetEmployee(string Name);

        [OperationContract]
        [FaultContract(typeof(FaultExceptionContract))]
        List<Employee> GetEmployeesByRemark(string remark);
    }

    [DataContract]
    public class Employee
    {
        public Employee()
        {
            Remarks.Add(System.DateTime.Now, "No Remarks yet");
        }
        

        [DataMember]
        public int Id { get; set; }

        [DataMember]
        public string Name { get; set; }

        [DataMember]
        public Dictionary<DateTime,string> Remarks =
            new Dictionary<DateTime,string>();

    }
}
