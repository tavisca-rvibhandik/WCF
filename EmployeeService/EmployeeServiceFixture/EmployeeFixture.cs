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
        [ExpectedException(typeof(FaultException<FaultExceptionContract>))]
        public void RetrieveEmptyEmployeeListTest()
        {
            List<Employee> empList = new List<Employee>();
            empList = retrieveClient.GetEmployees();
        }

        [TestMethod]
        public void CreateEmployeeTest()
        {
            int id = 1;
            string name = "a";
            createClient.CreateEmployee(id, name);
        }


        [TestMethod]
        public void RetrieveEmployeeByIdTest()
        {

            int id = 2;
            string name = "b";
            createClient.CreateEmployee(id, name);

            Employee e = retrieveClient.SearchById(2);
            Assert.AreEqual(2, e.Id);
        }

        [TestMethod]
        public void RetrieveEmployeeByNameTest()
        {

            int id = 3;
            string name = "c";
            createClient.CreateEmployee(id, name);
            id = 90;
            name = "c";
            createClient.CreateEmployee(id, name);
            List<Employee> empList = new List<Employee>();
            empList = retrieveClient.SearchByName("c");
            Assert.IsTrue(empList.Exists(t => String.Equals(t.Name, "c", StringComparison.OrdinalIgnoreCase) == true));
        }

        [TestMethod]
        public void RetrieveEmployeesByRemarkTest()
        {

            int id = 4;
            string name = "d";
            createClient.CreateEmployee(id, name);

            createClient.AddRemarks(id, "good");
            List<Employee> empList = new List<Employee>();
            empList = retrieveClient.GetEmployeesByRemark("good");
            Assert.IsTrue(empList.Exists(t => t.Remarks.ContainsValue("good") == true));
        }

        [TestMethod]
        public void RetrieveAllEmployeesTest()
        {

            int id = 5;
            string name = "e";
            createClient.CreateEmployee(id, name);

            List<Employee> empList = new List<Employee>();
            empList = retrieveClient.GetEmployees();
        }

        [TestMethod]
        public void AddRemarkTest()
        {

            int id = 6;
            string name = "f";
            createClient.CreateEmployee(id, name);

            createClient.AddRemarks(6,"toogood");
            Employee e = retrieveClient.SearchById(6);
            Assert.IsTrue(e.Remarks.ContainsValue("toogood"));
        }


        [TestMethod]
        [ExpectedException(typeof(FaultException<FaultExceptionContract>))]
        public void CreateDuplicateEmployeeTest()
        {
            int id = 7;
            string name = "g";
            createClient.CreateEmployee(id, name);

            id = 7;
            name = "h";
            createClient.CreateEmployee(id, name);
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

        [TestMethod]
        [ExpectedException(typeof(FaultException))]
        public void CreateEmployeeIdShouldNotBeNegativeTest()
        {
            int id = -1;
            string name = "a";
            createClient.CreateEmployee(id, name);
        }

        [TestMethod]
        [ExpectedException(typeof(FaultException))]
        public void CreateEmployeeNameShouldContainOnlyAlphabetTest()
        {
            int id = 1;
            string name = "a86";
            createClient.CreateEmployee(id, name);
        }
    }
}
