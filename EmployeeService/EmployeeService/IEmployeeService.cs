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
        string CreateEmployee(Employee e);

        [OperationContract]
        string AddRemarks(int id, string remarks);       
    }

    [ServiceContract]
    public interface IRetrieveEmployeeService
    {
        [OperationContract]
        List<Employee> GetEmployees();

        [OperationContract(Name = "SearchById")]
        Employee GetEmployee(int Id);

        [OperationContract(Name = "SearchByName")]
        Employee GetEmployee(string Name);
    }
   
    [DataContract]
    public class Employee
    {
        //public Employee()
        //{
        //    Id = 1;
        //    Name = "emp1";
        //    RemarkDate = System.DateTime.Now;
        //    RemarkText.Add("vry bad");
        //}
        public Employee(int id,String name,DateTime remarkDate,String remarkText)
        {
            Id = id;
            Name = name;
            RemarkDate = remarkDate;
            RemarkText.Add(remarkText);
        }
        [DataMember]
        public int Id { get; set; }

        [DataMember]
        public string Name { get; set; }

        [DataMember]
        public DateTime RemarkDate { get; set; }

        [DataMember]
        public List<string> RemarkText=new List<string>();
        
    }
}
