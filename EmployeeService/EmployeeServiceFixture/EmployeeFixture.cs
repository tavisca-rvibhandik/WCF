using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using EmployeeServiceFixture.EmployeeService;
using System.Collections.Generic;
using System.ServiceModel;
using System.Diagnostics;

namespace EmployeeServiceFixture
{
    [TestClass]
    public class EmployeeFixture
    {
        CreateEmployeeServiceClient createClient = new CreateEmployeeServiceClient("BasicHttpBinding_ICreateEmployeeService");
        RetrieveEmployeeServiceClient retrieveClient = new RetrieveEmployeeServiceClient("WSHttpBinding_IRetrieveEmployeeService");
        List<Employee> _List = new List<Employee>();
        Employee emp = new Employee();
        Employee emp1 = new Employee();
        Employee e = new Employee();


        [TestMethod]
        public void CreateEmployeeTest()
        {
            emp1.Id = 1;
            emp1.Name = "a";
            emp1.Remarks = new Dictionary<DateTime, string>();
            emp1.Remarks.Add(System.DateTime.Now, "good");
            createClient.CreateEmployee(emp1);
            _List.Add(emp1);
            Assert.AreEqual(1, _List[0].Id);
        }


        [TestMethod]
        public void RetrieveEmployeeByIdTest()
        {

            emp.Id = 2;
            emp.Name = "b";
            emp.Remarks = new Dictionary<DateTime, string>();
            emp.Remarks.Add(System.DateTime.Now, "good");
            createClient.CreateEmployee(emp);

            Employee e = retrieveClient.SearchById(2);
            Assert.AreEqual(2, e.Id);
        }

        [TestMethod]
        public void RetrieveEmployeeByNameTest()
        {

            emp.Id = 3;
            emp.Name = "c";
            emp.Remarks = new Dictionary<DateTime, string>();
            emp.Remarks.Add(System.DateTime.Now, "good");
            createClient.CreateEmployee(emp);
            emp.Id = 90;
            emp.Name = "c";
            emp.Remarks = new Dictionary<DateTime, string>();
            emp.Remarks.Add(System.DateTime.Now, "good");
            createClient.CreateEmployee(emp);
            List<Employee> empList = new List<Employee>();
            empList = retrieveClient.SearchByName("c");
            Assert.IsTrue(empList.Exists(t => String.Equals(t.Name, "c", StringComparison.OrdinalIgnoreCase) == true));
        }

        [TestMethod]
        public void RetrieveEmployeesByRemarkTest()
        {

            emp.Id = 4;
            emp.Name = "d";
            emp.Remarks = new Dictionary<DateTime, string>();
            emp.Remarks.Add(System.DateTime.Now, "good");
            createClient.CreateEmployee(emp);

            List<Employee> empList = new List<Employee>();
            Employee e = new Employee();
            empList = retrieveClient.GetEmployeesByRemark("good");
            Assert.IsTrue(empList.Exists(t => t.Remarks.ContainsValue("good") == true));
        }

        [TestMethod]
        public void RetrieveAllEmployeesTest()
        {

            emp.Id = 5;
            emp.Name = "e";
            emp.Remarks = new Dictionary<DateTime, string>();
            emp.Remarks.Add(System.DateTime.Now, "good");
            createClient.CreateEmployee(emp);

            List<Employee> empList = new List<Employee>();
            empList = retrieveClient.GetEmployees();
        }

        [TestMethod]
        public void AddRemarkTest()
        {

            emp.Id = 6;
            emp.Name = "f";
            emp.Remarks = new Dictionary<DateTime, string>();
            emp.Remarks.Add(System.DateTime.Now, "good");
            createClient.CreateEmployee(emp);

            createClient.AddRemarks(6,"toogood");
            Employee e = retrieveClient.SearchById(6);
            Assert.IsTrue(e.Remarks.ContainsValue("toogood"));
        }


        [TestMethod]
        [ExpectedException(typeof(FaultException<FaultExceptionContract>))]
        public void CreateDuplicateEmployeeTest()
        {
            emp.Id = 7;
            emp.Name = "h";
            emp.Remarks = new Dictionary<DateTime, string>();
            emp.Remarks.Add(System.DateTime.Now, "good");
            createClient.CreateEmployee(emp);

            emp.Id = 7;
            emp.Name = "g";
            emp.Remarks = new Dictionary<DateTime, string>();
            emp.Remarks.Add(System.DateTime.Now, "good");
            createClient.CreateEmployee(emp);
        }

        [TestMethod]
        [ExpectedException(typeof(FaultException<FaultExceptionContract>))]
        public void AddRemarkForEmployeeNotPresentTest()
        {
            createClient.AddRemarks(8,"toogood");
        }

        [TestMethod]
        [ExpectedException(typeof(FaultException<FaultExceptionContract>))]
        public void RetrieveEmployeeByIdNotPresentTest()
        {
            Employee e = retrieveClient.SearchById(200);
            Assert.IsNull(e);
        }

        [TestMethod]
        [ExpectedException(typeof(FaultException<FaultExceptionContract>))]
        public void RetrieveEmployeeByNameNotPresentTest()
        {
            List<Employee> empList = new List<Employee>();
            empList = retrieveClient.SearchByName("z");
        }

        [TestMethod]
        [ExpectedException(typeof(FaultException<FaultExceptionContract>))]
        public void RetrieveEmployeeByRemarkNotPresentTest()
        {
            List<Employee> empList = new List<Employee>();
            empList = retrieveClient.GetEmployeesByRemark("bad");
        }

        [TestMethod]
        [ExpectedException(typeof(FaultException))]
        public void RemarksShouldContainOnlyAlphabetTest()
        {
            createClient.AddRemarks(6,"toogood!");
        }

        [TestMethod]
        [ExpectedException(typeof(FaultException))]
        public void IdInAddRemarkShouldNotBeNegativeTest()
        {
            createClient.AddRemarks(-6, "toogood");
        }

        [TestMethod]
        [ExpectedException(typeof(FaultException))]
        public void IdShouldNotBeNegativeTest()
        {
            Employee e = retrieveClient.SearchById(-2);
        }

        [TestMethod]
        [ExpectedException(typeof(FaultException))]
        public void GetEmployeesByRemarksShouldContainOnlyAlphabetTest()
        {
            List<Employee> empList = new List<Employee>();
            empList = retrieveClient.GetEmployeesByRemark("b24ad");
        }

        [TestMethod]
        [ExpectedException(typeof(FaultException))]
        public void NameShouldContainOnlyAlphabetTest()
        {
            List<Employee> empList = new List<Employee>();
            empList = retrieveClient.SearchByName("z34");
        }

        //[TestMethod]
        //[ExpectedException(typeof(FaultException<FaultExceptionContract>))]
        //public void RetrieveEmptyEmployeeListTest()
        //{
        //    List<Employee> empList = new List<Employee>();
        //    empList = retrieveClient.GetEmployees();
        //}


    }
}
