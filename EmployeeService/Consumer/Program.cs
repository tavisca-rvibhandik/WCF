using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Consumer.EmployeeService;
using System.ServiceModel;

namespace Consumer
{
    class Program
    {
        static void Main(string[] args)
        {
            var client = new CreateEmployeeServiceClient("BasicHttpBinding_ICreateEmployeeService");
            var retrieveClient = new RetrieveEmployeeServiceClient("WSHttpBinding_IRetrieveEmployeeService");
                
            try
            {
                int choice = 0;
                Employee e = new Employee();
                do
                {
                    Console.WriteLine("1) Add Employee\n2) Add remarks\n3) Get Employee Details by id\n4) Get Employee Details by Name\n5) Get All Employees Details\nEnter Your Choice: ");
                    choice = Convert.ToInt32(Console.ReadLine());

                    switch (choice)
                    {
                        case 1:

                            Console.WriteLine("Enter Employee Id: ");
                            e.Id = Convert.ToInt32(Console.ReadLine());
                            Console.WriteLine("Enter Employee Name: ");
                            e.Name = Console.ReadLine();
                            Console.WriteLine(client.CreateEmployee(e));
                            break;

                        case 2:

                            Console.WriteLine("Enter the id: ");
                            int id = Convert.ToInt32(Console.ReadLine());
                            Console.WriteLine("Enter the Remark: ");
                            Console.WriteLine(client.AddRemarks(id, Console.ReadLine()));
                            break;

                        case 3:
                            Console.WriteLine("Enter the id: ");
                            id = Convert.ToInt32(Console.ReadLine());
                            Employee emp = new Employee();
                            emp = retrieveClient.SearchById(id);
                            Console.WriteLine("\n" + emp.Id + "\n" + emp.Name + "\n");
                            Console.WriteLine("Remarks:");
                            foreach (var item in emp.Remarks)
                            {
                                Console.WriteLine(item.Key + "\t" + item.Value + "\n");
                            }
                            break;

                        case 4:
                            Console.WriteLine("Enter the Name: ");
                            Employee emp1 = new Employee();
                            emp1 = retrieveClient.SearchByName(Console.ReadLine());
                            Console.WriteLine("\n" + emp1.Id + "\n" + emp1.Name + "\n");
                            Console.WriteLine("Remarks:");
                            foreach (var item in emp1.Remarks)
                            {
                                Console.WriteLine(item.Key + "\t" + item.Value + "\n");
                            }
                            break;

                        case 5:
                            List<Employee> list = new List<Employee>();
                            list.AddRange(retrieveClient.GetEmployees());
                            foreach (var item in list)
                            {
                                Console.WriteLine("\n" + item.Id + "\n" + item.Name + "\n");
                                Console.WriteLine("Remarks:");
                                foreach (var remarksItem in item.Remarks)
                                {
                                    Console.WriteLine(remarksItem.Key + "\t" + remarksItem.Value + "\n");
                                }
                            }
                            break;

                        case 6:
                            break;
                    }
                } while (choice != 6);
            }

            catch (FaultException<FaultExceptionContract> greetingFault)
            {
                Console.WriteLine("An Error Occured : " + greetingFault.Detail.Message);
                Console.ReadLine();
                client.Abort();
            }            
        }        
    }
}