using System;
using System.ComponentModel.DataAnnotations;
using System.Runtime.CompilerServices;

namespace T5
{
    public class EmployeeManager : BaseManager
    {
        //Varriable
        private Employee[] employees;
        string filePath;
        // Contructor
        public EmployeeManager() : base()
        {
            this.filePath = @"T5.data";
            if (!File.Exists(filePath)) { File.WriteAllText(filePath, "EmpNo,EmpName,EmpEmail,true,EmpPassword,false"); }
            string[] content = File.ReadAllLines(this.filePath);
            this.employees = new Employee[]
            {
                new Employee("EmpNo", "EmpName", "EmpEmail",true,"EmpPassword",false)
            };
            if (content.Length > 1)
            {
                for (int i = 1; i < content.Length; i++)
                { 
                string[] cell = content[i].Split(',');
                employees = employees.Concat(new Employee[] { new Employee(Convert.ToString(cell[0]), Convert.ToString(cell[1]), Convert.ToString(cell[2]), Convert.ToBoolean(cell[3]), Convert.ToString(cell[4]), Convert.ToBoolean(cell[5])) }).ToArray();
                }
            } else
            {
                Console.WriteLine("No base data!");
                Console.WriteLine("Creating base data!");
                employees = employees.Concat(new Employee[] { new Employee("admin", "admin","admin", false, "admin", true) }).ToArray();
                PrintToFile(this.filePath);                
            }            
        }

        // Void
        public override void Show()
        {
            PrintList(employees);
        }
        public override void AddNew()
        {
            Console.Write("Enter EmpNo: ");
            String EmpNo = Console.ReadLine();
            Console.Write("Enter EmpName: ");
            String EmpName = Console.ReadLine();
            Console.Write("Enter EmpEmail: ");
            String EmpEmail = Console.ReadLine();
            Console.Write("Enter EmpPassword: ");
            String EmpPassword = Console.ReadLine();
            Console.Write("Enter EmpisManager (True/False): ");
            bool EmpIsManager = Convert.ToBoolean(Console.ReadLine());
        
            if (!this.isValid(EmpName))
            {
                employees = employees.Concat(new Employee[] { new Employee(EmpNo, EmpName, EmpEmail, false, EmpPassword, EmpIsManager) }).ToArray();
            }
            else
            {
                Console.Write("Valid EmpNo! ");
                Console.ReadLine();
            }
            PrintToFile(this.filePath);
            PrintList(employees);
        }

        public override void Update()
        {
            Console.Write("Enter EmpNo or EmpName for update: ");
            String searchKey = Console.ReadLine();
            foreach (Employee emp in employees)
            {
                if (((emp.GetNo().Equals(searchKey) || emp.GetName().Equals(searchKey)) && emp.GetDeleted().Equals(false)))
                {
                    Console.WriteLine("Found a Employee have EmpNo or Emp Name is:" + searchKey);
                    Console.WriteLine("Ready for update");
                    Console.Write ("Enter EmpNo: ");
                    emp.SetNo (Console.ReadLine());
                    Console.Write("Enter EmpName: ");
                    emp.SetName (Console.ReadLine());
                    Console.Write("Enter EmpEmail: ");
                    emp.SetEmail (Console.ReadLine());
                }
            }
            PrintToFile(this.filePath);
            PrintList(employees);
        }

        public override void Delete()
        {
            Console.Write("Enter EmpNo or EmpName for delete: ");
            String searchKey = Console.ReadLine();
            foreach (Employee emp in employees)
            {
                if (emp.GetNo().Equals(searchKey) || emp.GetName().Equals(searchKey))
                {
                    Console.WriteLine("Found a Employee have EmpNo or Emp Name is:" + searchKey);
                    Console.WriteLine("Ready for delete");
                    Console.Write("Yes(Y) or No(any other):");
                    if (Console.ReadLine().ToUpper() == "Y") { emp.SetDeleted(true); }
                    else { Console.WriteLine("Nothing happen!"); }
                }
            }
            PrintToFile(this.filePath);
            PrintList(employees);
        }

        public override void Find()
        {
            Console.Write("Enter EmpNo or Name: ");
            String searchKey = Console.ReadLine();

            // search            
            Employee[] result = new Employee[this.getLength()];
            int count = 0;
            foreach (Employee emp in employees)
            {
                if ((emp.GetNo().Equals(searchKey) || emp.GetName().Equals(searchKey)) && emp.GetDeleted().Equals(0))
                {
                    result[count++] = emp;
                    //count++;
                }
            }

            // print
            if (count > 0)
            {
                PrintList(result);
            } else
            {
                Console.WriteLine("Not Found!");
            }
        }
        private void PrintList(Employee[] arr)
        {
            Console.Clear();
            foreach (Employee item in arr)
            {                
                if (item != null && item.GetDeleted().Equals(false))
                {
                    Console.WriteLine(item);
                }
            }
            Console.WriteLine("---------------------------------");
        }
        public void PrintToFile(string filePath)
        {
            string[] content = new string[this.getLength()];
            int i = 0;
            foreach (Employee item in employees)
            {
                content[i++] = item.ToStringForFile();
            }
            File.WriteAllLines(filePath, content);
            Console.Write("Data is saved!");
            Console.ReadLine();
        }
        //Checker
        public int getLength()
        {
            int employeeLenght = 0;
            foreach (Employee emp in employees) employeeLenght++;
            return employeeLenght;
        }
        public bool isValid(string EmpNo)
        {
            foreach (Employee emp in employees)
            {
                if (emp.GetNo().Equals(EmpNo))
                { return true; }
            }
            return false;
        }
        public bool isPassword(string EmpNo,string EmpPassword)
        {
            foreach (Employee emp in employees)
            {
                if (emp.GetNo().Equals(EmpNo) && emp.GetPassword().Equals(EmpPassword))
                { return true; }
            }
            return false;
        }
        public bool isManager(string EmpNo)
        {
            foreach (Employee emp in employees)
            {
                if (emp.GetNo().Equals(EmpNo) && emp.GetIsManager().Equals(true))
                { return true; }
            }
            return false;
        }
                
	}
}

