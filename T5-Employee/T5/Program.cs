using System.Runtime.CompilerServices;
using System.Security.Cryptography.X509Certificates;
using System.Security.Principal;
using System.Xml.Linq;

namespace T5;

class Program
{
    static void Main(string[] args)
    {
        string EmpNo = "Noname";
        bool isManager = false;
        bool loged = false;
        EmployeeManager manager = new EmployeeManager();
        do
        {
            login();
            if (isManager) { ManagerScreen(); }
            if (!isManager) { UserScreen(); }
        }
        while (!loged);
        
        void login() 
        {
            string EmpPassword = "Nopassword";
            Console.WriteLine("***EMPLOYEE MANAGER***");
            Console.WriteLine("***  LOGIN SCREEN  ***");
            do
            {
                Console.Write("EmpNo:");
                if (Console.ReadLine != null) { EmpNo = Console.ReadLine(); }
                Console.Write("Password:");
                if (Console.ReadLine != null) { EmpPassword = Console.ReadLine(); }
                if (!manager.isValid(EmpNo)) 
                {
                    Console.WriteLine("Invalid username");
                    Console.ReadLine();
                    continue;
                }
                if (!manager.isPassword(EmpNo, EmpPassword))
                {
                    Console.WriteLine("Wrong password");
                    Console.ReadLine();
                    continue;
                }
                isManager = manager.isManager(EmpNo);
                loged = true;
                Console.WriteLine("Login succesful");
                Console.ReadLine() ;
                Console.Clear();
            }
            while (!loged);
        }
        void ManagerScreen() 
        {
            int selected;
            do
            {
                Console.WriteLine("***EMPLOYEE MANAGER***");
                Console.WriteLine("EmpNo: {0}", EmpNo);
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
                        manager.Find();
                        break;
                    case 2:
                        manager.AddNew();
                        break;
                    case 3:
                        manager.Update();
                        break;
                    case 4:
                        manager.Delete();
                        break;
                    case 5:
                        manager.Show();
                        break;
                    case 6:
                        loged = false;
                        Console.WriteLine("Logging out");
                        break;
                    case 7:
                        Console.WriteLine("-------- END ---------");
                        break;
                    default:
                        Console.WriteLine("Invalid");
                        break;
                }
            } while (selected != 7 && loged);
        }
        void UserScreen()
        {
            int selected;
            do
            {
                Console.WriteLine("***EMPLOYEE MANAGER***");
                Console.WriteLine("EmpNo: {0}",EmpNo);
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
                        manager.Find();
                        break;
                    case 2:
                        manager.Show();
                        break;
                    case 3:
                        loged=false;
                        Console.WriteLine("Logging out");
                        break;
                    case 4:
                        Console.WriteLine("-------- END ---------");
                        break;
                    default:
                        Console.WriteLine("Invalid");
                        break;
                }
            } while (selected != 4 && loged);
        }
        Console.ReadLine();
    }
    
}

