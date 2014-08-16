using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EmployeeService;
using Consumer.EmployeesService;

namespace Consumer
{
    class Program
    {
        static void Main(string[] args)
        {
            var client = new CreateEmployeeServiceClient("BasicHttpBinding_ICreateEmployeeService");
            var retrieveClient = new RetrieveEmployeeServiceClient("WSHttpBinding_IRetrieveEmployeeService");
            Console.WriteLine("1 Add Employee\n2 Add remarks\n3 Get Employee Details by id\n4 Get Employee Details by Name\n5 Get All Employees Details\nEnter Your Choice: ");
            int choice = Convert.ToInt32(Console.ReadLine());
            
            switch (choice)
            {
                case 1:
                    Console.WriteLine("Enter Employee Id: ");
                    int id = Convert.ToInt32(Console.ReadLine());
                    Console.WriteLine("Enter Employee Name: ");
                    String name =Console.ReadLine();
                    Console.WriteLine("Enter Employee RemarkText: ");
                    //String remarkText =Console.ReadLine();
                    Employee e = new Employee(id, name, System.DateTime.Now, Console.ReadLine());
                    Console.WriteLine(client.CreateEmployee(e));
                    break;
                case 2: 
                    Console.WriteLine("Enter the id: ");
                    id =Convert.ToInt32(Console.ReadLine());
                    Console.WriteLine("Enter the Remark: ");
                    Console.WriteLine(client.AddRemarks(id, Console.ReadLine()));
                    break;
                case 3:
                    Console.WriteLine("Enter the id: ");
                    id =Convert.ToInt32(Console.ReadLine());
                    Console.WriteLine(retrieveClient.SearchById(id));
                    break;
                case 4:
                    Console.WriteLine("Enter the Name: ");
                    Console.WriteLine(retrieveClient.SearchByName(Console.ReadLine()));
                    break;
                case 5:
                    List<Employee> list = new List<Employee>();
                    list.AddRange(retrieveClient.GetEmployees());
                    foreach (var item in list)
                    {
                        Console.WriteLine(item.Id + item.Name);
                    }

            }
           
             Console.ReadKey();
        }
    }
}