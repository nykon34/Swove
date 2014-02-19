using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Swove.Data.FS;
using LScript;

namespace Swove.Data.Program
{
    class Explorer
    {
        public static void Start(FileSystem Files, String Username, String Directory)
        {
            String cDir = "Main Drive";
            if (Directory != "") cDir = Directory;
            String inp = "";
            String[] data;
            String[] speech;
            Boolean open = true;
            while (open)
            {
                Console.Clear();
                Console.WriteLine("Files for " + cDir);
                Console.WriteLine();
                foreach (Data.FS.Folder folder in Files.Folders)
                {
                    if (folder.Parent == Files.getFolder(cDir) && Files.hasPermission(Username, folder, null)) Console.WriteLine("  > " + folder.Name);
                }
                foreach (Data.FS.File file in Files.Files)
                {
                    if (file.Parent == Files.getFolder(cDir) && Files.hasPermission(Username, null, file)) Console.WriteLine("  - " + file.Name + "." + file.Extension.ToUpper());
                }
                Console.Write("\n > "); inp = Console.ReadLine();
                data = inp.Split(' ');
                speech = inp.Split('"');
                switch (data[0].ToLower())
                {
                    case "!": case "exit":
                        open = false;
                        break;
                    case "^":
                        if (Files.getFolder(cDir).Parent != null) cDir = Files.getFolder(cDir).Parent.Name;
                        break;
                    case "create":
                        if (Files.doesExist(speech[1]) == 0)
                        {
                            switch (data[1])
                            {
                                case "1": //Folder
                                    Files.createFolder(speech[1], Files.getFolder(cDir), false, "");
                                    break;
                                case "2": //File
                                    //create "NAME" "EXT"
                                    Files.createFile(speech[1], false, "", speech[3], Files.getFolder(cDir));
                                    break;
                            }
                        }
                        break;
                    case "delete":
                        switch (Files.doesExist(speech[1]))
                        {
                            case 1:
                                if (Files.hasPermission(Username, Files.getFolder(speech[1]), null) && Files.getFolder(speech[1]).ReadOnly == false) Files.Folders.Remove(Files.getFolder(speech[1]));
                                break;
                            case 2:
                                if (Files.hasPermission(Username, null, Files.getFile(speech[1])) && Files.getFile(speech[1]).ReadOnly == false) Files.Files.Remove(Files.getFile(speech[1]));
                                break;
                        }
                        break;
                    case "rename":
                        switch (Files.doesExist(speech[1]))
                        {
                            case 1:
                                Files.createFolder(speech[3], Files.getFolder(speech[1]).Parent, Files.getFolder(speech[1]).ReadOnly, Files.getFolder(speech[1]).Permission);
                                foreach (Folder folder in Files.Folders)
                                {
                                    if (folder.Parent == Files.getFolder(speech[1])) folder.Parent = Files.getFolder(speech[3]);
                                }
                                if (Files.hasPermission(Username, Files.getFolder(speech[1]), null) && Files.getFolder(speech[1]).ReadOnly == false) Files.Folders.Remove(Files.getFolder(speech[1]));
                                break;
                            case 2:
                                if (Files.hasPermission(Username, null, Files.getFile(speech[1])) && Files.getFile(speech[1]).ReadOnly == false) Files.getFile(speech[1]).Name = speech[3];
                                break;
                        }
                        break;
                    default:
                        switch (Files.doesExist(inp))
                        {
                            case 1:
                                cDir = inp;
                                break;
                            case 2:
                                switch (Files.getFile(inp).Extension.ToLower()) {
                                    case "txt":
                                        Data.Program.Editor.Start(Files.getFile(inp), Files);
                                        break;
                                    case "sdt":
                                        SDT ta = new SDT(Files, Files.getFile(inp));
                                        ta.Start();
                                        break;
                                    case "ls":
                                        Console.Clear();
                                        LScript.Engine.doQuery("include \"" + inp + "\"", "File: " + inp);
                                        Console.ReadKey(); //ADD EDITOR FOR SCRIPTING LANGUAGE
                                        break;
                                }
                                break;
                        }
                        break;
                }
            }
            //Add file view
        }
    }
}
