using System.Runtime.CompilerServices;
using System.Security.Cryptography.X509Certificates;
using System.Security.Principal;
using System.Xml.Linq;

namespace T6;

class Program
{
    static void Main(string[] args)
    {
        //Variable
              
        EmployeeManager manager = new EmployeeManager();
        //Mainstream
        do
        {
            manager.Login();
            switch (manager.GetRole())
            {
                case "manager":
                    manager.ManagerScreen();
                    break;
                case "user":
                    manager.UserScreen();
                    break;
            }
        }
        while (!manager.GetLoged());        
        Console.ReadLine();
    }
    
}

