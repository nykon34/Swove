using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LScript;

namespace Swove
{
    class Program
    {
        public static Data.Login Login = new Data.Login();
        public static Data.FileSystem Files = new Data.FileSystem();
        public static String cDir = "Main Drive";
        static void Main(string[] args)
        {
            LScript.Engine.FS = Files;
            Files.createFolder("Main Drive", null, true, "");
            Files.createFolder("System Files", Files.getFolder("Main Drive"), true, "");
            Files.createFolder("Temp", Files.getFolder("Main Drive"), true, "");

            Files.createFile("SystemData", false, "", "sdt", Files.getFolder("System Files"));
            Files.getFile("SystemData").Data.Add("column Data,Value");
            Files.getFile("SystemData").Data.Add("insert SystemName¬Swove");
            LScript.Engine.Table = new Swove.Data.Program.SDT(Files, Files.getFile("SystemData"));

            Files.createFile("TestScript", false, "", "ls", Files.getFolder("Main Drive"));
            Files.getFile("TestScript").Data.Add("array DATA ¬ \"[select Data = \"SystemName\"]\"");
            Files.getFile("TestScript").Data.Add("show %DATA|1%");

            bool LoggedIn = false;
            String Username = "";
            String Password = "";
            String inp = "";
            while (true) //Login screen
            {
                Login.addLogin("Liam", "Test");
                Console.Write("Username: "); Username = Console.ReadLine();
                Console.Write("Password: "); Password = Console.ReadLine();
                if (Login.doLogin(Username, Password))
                {
                    LoggedIn = true;
                    cDir = "Main Drive";
                }
                else
                {
                    Console.WriteLine("False login.");
                }

                while (LoggedIn) //When logged in, do computer stuff here
                {
                    Console.Clear();
                    Console.WriteLine("\n  Swove Menu\n");
                    Console.WriteLine("    1  > Explorer");
                    Console.WriteLine("    2  > Editor");
                    Console.WriteLine("    3  > Sharp Data Table");
                    Console.WriteLine();
                    Console.Write("Option: "); inp = Console.ReadLine();
                    switch (inp)
                    {
                        case "1":
                            Data.Program.Explorer.Start(Files, Username, "");
                            break;
                        case "2":
                            Data.Program.Editor.Start(null, Files);
                            break;
                        case "3":
                            Data.Program.SDT ta = new Data.Program.SDT(Files, null);
                            ta.Start();
                            break;
                        default:
                            Console.WriteLine("Unknown item in menu.");
                            break;
                    }
                }
            }
        }
    }
}
