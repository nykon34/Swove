using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Swove.Data.FS;

namespace Swove.Data.Program
{
    class Editor
    {
        public static void Start(File File, FileSystem FS)
        {
            String name = "";
            Boolean quit = false;
            if (FS.doesExist("Temp") == 0) FS.createFolder("Temp", FS.getFolder("Main Drive"), true, "");
            if (File == null)
            {
                while ((File == null || FS.doesExist(name) != 2) && quit == false)
                {
                    Console.Clear();
                    if (name != "" && FS.doesExist(name) != 2) Console.WriteLine("That file does not exist.");
                    Console.Write("File to load: "); name = Console.ReadLine();
                    if (name == "!") quit = true;
                    if (FS.doesExist(name) == 2)
                    {
                        File = FS.getFile(name); quit = false;
                    }
                }
            }
            if (quit == false)
            {
                Console.Clear();
                int ln = 1;
                String inp = "";
                Console.WriteLine(" - Editor - \n");
                if (File.Data == null) File.Data.Add("");
                foreach (String line in File.Data)
                {
                    Console.WriteLine("> " + line);
                }
                while (inp != "!")
                {
                    Console.Write("> "); inp = Console.ReadLine();
                    if (inp != "!" && File.ReadOnly != true) File.Data.Add(inp);
                }
            }
        }
    }
}
