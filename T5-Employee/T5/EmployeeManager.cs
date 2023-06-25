using System;
using System.ComponentModel.DataAnnotations;
using System.Runtime.CompilerServices;

namespace T6
{
    public class EmployeeManager : BaseManager
    {
        //Varriable
        private Employee[] employees;
        private string filePath = @"T6.dat";
        private bool loged = false;
        private string empNoLogin;
        private string role;
        
        // Contructor
        public EmployeeManager() : base()
        {
            // Create template file if not exist
            if (!File.Exists(filePath)) { File.WriteAllText(filePath, "EmpNo,EmpName,EmpEmail,true,EmpPassword,false"); }
            
            // Read file thisPath
            string[] content = File.ReadAllLines(this.filePath);
            
            // Create template file for first item 
            this.employees = new Employee[]
            {
                new Employee("EmpNo", "EmpName", "EmpEmail",true,"EmpPassword",false)
            };
            
            // Set data from file to cache data. 
            if (content.Length > 1)
            {
                for (int i = 1; i < content.Length; i++)
                { 
                string[] cell = content[i].Split(',');
                employees = employees.Concat(new Employee[] { new Employee(cell[0], cell[1], cell[2], Convert.ToBoolean(cell[3]), cell[4], Convert.ToBoolean(cell[5])) }).ToArray();
                }
            } else
            {
                // Set admin user for first time or no base data
                Console.WriteLine("No base data!");
                Console.WriteLine("Creating base data!");
                employees = employees.Concat(new Employee[] { new Employee("admin", "admin","admin", false, "admin", true) }).ToArray();
                PrintToFile(this.filePath);                
            }            
        }
        
        //Getter
        public string GetRole() 
        {
            return role;
        }
        public bool GetLoged()
        {
            return loged;
        }
                
        // Void        
        // Module Add new a employee
        public override void AddNew()
        {
                       
            Console.Write("Enter EmpNo:");
            string empNo = (Console.ReadLine());
            Console.Write("Enter EmpName:");
            string empName = Console.ReadLine();
            Console.Write("Enter EmpEmail:");
            string empEmail = Console.ReadLine();
            Console.Write("Enter EmpPassword:");
            string empPassword = Console.ReadLine();
            Console.Write("Enter EmpIsManager (True/Any else):");
            bool empIsManager;
            if (Console.ReadLine() == "True")
            {
                empIsManager = true;
            }
            else
            {
                empIsManager = false;
            }              
                   
            // Check not duplicate Emp No
            if (!this.isValid(empNo))
            {
                employees = employees.Concat(new Employee[] { new Employee(empNo, empName, empEmail, false, empPassword, empIsManager) }).ToArray();
                WriteLogFile(empNoLogin + " add new " + empNo + "," + empName + "," + empEmail);
            }
            else
            {
                Console.Write("Valid EmpNo! ");
                Console.ReadLine();
            }
            PrintToFile(this.filePath);
            PrintList(employees);
        }

        // Module Update a employee
        public override void Update()
        {
            Console.Write("Enter EmpNo or EmpName for update: ");
            string searchKey = Console.ReadLine();
            foreach (Employee emp in employees)
            {
                if (((emp.GetNo().Equals(searchKey) || emp.GetName().Equals(searchKey)) && emp.GetDeleted().Equals(false)))
                {
                    Console.WriteLine("Found a Employee have EmpNo or Emp Name is:" + searchKey);
                    Console.WriteLine(emp.GetNo() + "," + emp.GetName() + "," + emp.GetEmail());
                    Console.WriteLine("Ready for update!");
                    Console.Write ("Enter EmpNo: ");
                    emp.SetNo (Console.ReadLine());
                    Console.Write("Enter EmpName: ");
                    emp.SetName (Console.ReadLine());
                    Console.Write("Enter EmpEmail: ");
                    emp.SetEmail (Console.ReadLine());
                    WriteLogFile(empNoLogin + " update " + searchKey + "," + emp.GetNo + "," + emp.GetName + "," + emp.GetEmail);                    
                }
            }
            PrintToFile(this.filePath);
            PrintList(employees);            
        }
        
        // Module Delete a employee
        public override void Delete()
        {
            Console.Write("Enter EmpNo or EmpName for delete: ");
            String searchKey = Console.ReadLine();
            foreach (Employee emp in employees)
            {
                if (emp.GetNo().Equals(searchKey) || emp.GetName().Equals(searchKey))
                {
                    Console.WriteLine("Found a Employee have EmpNo or Emp Name is:" + searchKey);
                    Console.WriteLine(emp.GetNo() + "," + emp.GetName() + "," + emp.GetEmail());
                    Console.WriteLine("Ready for delete");
                    Console.Write("Yes(Y) or No(any other):");
                    if (Console.ReadLine().ToUpper() == "Y") 
                    { 
                        emp.SetDeleted(true);
                        WriteLogFile(empNoLogin + " delete " + searchKey);
                    }
                    else { Console.WriteLine("Nothing happen!"); }
                }
            }
            PrintToFile(this.filePath);
            PrintList(employees);            
        }
        
        // Module Find employee
        public override void Find()
        {
            Console.Write("Enter EmpNo or Name: ");
            String searchKey = Console.ReadLine();
            // search            
            Employee[] result = new Employee[this.dataGetLength()];
            int count = 0;
            foreach (Employee emp in employees)
            {
                if ((emp.GetNo().Equals(searchKey) || emp.GetName().Equals(searchKey)) && emp.GetDeleted().Equals(0))
                {
                    result[count++] = emp;
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

        // Module Show List of employee
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

        // Module Write to file filePath all data 
        public void PrintToFile(string filePath)
        {
            string[] content = new string[this.dataGetLength()];
            int i = 0;
            foreach (Employee item in employees)
            {
                content[i++] = item.ToStringForFile();
            }
            File.WriteAllLines(filePath, content);
            Console.Write("Data is saved!");
            Console.ReadLine();
        }

        // Module Login
        public void Login()
        {
            string empPassword = "Nopassword";
            Console.WriteLine("***EMPLOYEE MANAGER***");
            Console.WriteLine("***  LOGIN SCREEN  ***");
            do
            {
                Console.Write("EmpNo:");
                empNoLogin = Console.ReadLine();
                Console.Write("Password:");
                empPassword = Console.ReadLine();
                if (!this.isValid(empNoLogin))
                {
                    Console.WriteLine("Invalid username");
                    Console.ReadLine();
                    continue;
                }
                if (!this.isPassword(empNoLogin, empPassword))
                {
                    Console.WriteLine("Wrong password");
                    Console.ReadLine();
                    continue;
                }
                if (this.isDeleted(empNoLogin))
                {
                    Console.WriteLine("Employee deleted!");
                    Console.ReadLine();
                    continue;
                }
                this.loged = true;
                WriteLogFile(empNoLogin + " log in ");

                if (isManager(empNoLogin)) role = "manager";
                if (!isManager(empNoLogin)) role = "user";
                Console.WriteLine("Login succesful");
                Console.ReadLine();
                Console.Clear();
            }
            while (!loged);
        }

        // Module Manager Screen
        public void ManagerScreen()
        {
            int selected;
            do
            {
                Console.WriteLine("***EMPLOYEE MANAGER***");
                Console.WriteLine("EmpNo: {0}", this.empNoLogin);
                Console.WriteLine("Role = Manager");
                Console.WriteLine("1. Search Employee by Name or EmpNo");
                Console.WriteLine("2. Add New Employee");
                Console.WriteLine("3. Update Employee");
                Console.WriteLine("4. Delete Employee");
                Console.WriteLine("5. Show all Employee");
                Console.WriteLine("6. Logout");
                Console.WriteLine("7. Exit");
                Console.Write("   Select (1-7): ");
                selected = Convert.ToInt16(Console.ReadLine());
                switch (selected)
                {
                    case 1:
                        this.Find();
                        break;
                    case 2:
                        this.AddNew();
                        break;
                    case 3:
                        this.Update();
                        break;
                    case 4:
                        this.Delete();
                        break;
                    case 5:
                        this.PrintList(employees);
                        break;
                    case 6:
                        loged = false;
                        Console.WriteLine("Logging out");
                        WriteLogFile(empNoLogin + " log out");
                        break;
                    case 7:
                        WriteLogFile(empNoLogin + " log out");
                        Console.WriteLine("-------- END ---------");
                        break;
                    default:
                        Console.WriteLine("Invalid");
                        break;
                }
            } while (selected != 7 && loged);            
        }

        // Module User Screen
        public void UserScreen()
        {
            int selected;
            do
            {
                Console.WriteLine("***EMPLOYEE MANAGER***");
                Console.WriteLine("EmpNo: {0}", empNoLogin);
                Console.WriteLine("Role = User");
                Console.WriteLine("1. Search Employee by Name or EmpNo");
                Console.WriteLine("2. Show all Employee");
                Console.WriteLine("3. Log out");
                Console.WriteLine("4. Exit");
                Console.Write("   Select (1-4): ");
                selected = Convert.ToInt16(Console.ReadLine());
                switch (selected)
                {
                    case 1:
                        this.Find();
                        break;
                    case 2:
                        this.PrintList(employees);
                        break;
                    case 3:
                        loged = false;
                        Console.WriteLine("Logging out");
                        WriteLogFile(empNoLogin + " log out");
                        break;
                    case 4:
                        Console.WriteLine("-------- END ---------");
                        WriteLogFile(empNoLogin + " log out");
                        break;
                    default:
                        Console.WriteLine("Invalid");
                        break;
                }
            } while (selected != 4 && loged);
        }

        // Module Write log file
        private void WriteLogFile(string content) 
        {
            File.AppendAllTextAsync("T6.log", DateTime.Now + " : " + content + "\n");            
        }


        //Checker
        // Check how much item of data 
        private int dataGetLength()
        {
            int employeeLenght = 0;
            foreach (Employee emp in employees) employeeLenght++;
            return employeeLenght;
        }

        // Check empNo valid in data
        private bool isValid(string empNo)
        {
            foreach (Employee emp in employees)
            {
                if (emp.GetNo().Equals(empNo))
                { return true; }
            }
            return false;
        }

        // Check correct passwword
        private bool isPassword(string empNo,string empPassword)
        {
            foreach (Employee emp in employees)
            {
                if (emp.GetNo().Equals(empNo) && emp.GetPassword().Equals(empPassword))
                { return true; }
            }
            return false;
        }

        // Check user is manager
        private bool isManager(string empNo)
        {
            foreach (Employee emp in employees)
            {
                if (emp.GetNo().Equals(empNo) && emp.GetIsManager().Equals(true))
                { return true; }
            }
            return false;
        }
        
        // Check empNo is deleted
        private bool isDeleted(string empNo)
        {
            bool value = false;
            foreach (Employee emp in employees)
            {
                if (emp.GetNo().Equals(empNo) && emp.GetDeleted().Equals(true))
                { value = true; }
            }
            return value;
        }
    }
}

