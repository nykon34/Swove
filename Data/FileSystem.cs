using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Swove.Data.FS;

namespace Swove.Data
{
    class FileSystem
    {
        public List<Folder> Folders = new List<Folder>();
        public List<File> Files = new List<File>();

        public int doesExist(String N)
        {
            foreach (Folder folder in Folders)
            {
                if (folder.Name == N) return 1;
            }
            foreach (File file in Files)
            {
                if (file.Name == N) return 2;
            }
            return 0;
        }

        public bool createFolder(String N, Folder P, Boolean RO, String Perm)
        {
            if (doesExist(N) == 0)
            {
                Folders.Add(new Folder { Name = N, Parent = P, ReadOnly = RO, Permission = Perm });
                return true;
            }
            else
            {
                return false;
            }
        }

        public Folder getFolder(String N)
        {
            foreach (Folder folder in Folders)
            {
                if (folder.Name == N) return folder;
            }
            return null;
        }

        public bool createFile(String N, Boolean RO, String Perm, String Ext, Folder P)
        {
            if (doesExist(N) == 0)
            {
                Files.Add(new File { Name = N, Parent = P, ReadOnly = RO, Permission = Perm, Extension = Ext, Data = new List<string>() });
                return true;
            }
            else
            {
                return false;
            }
        }

        public File getFile(String N)
        {
            foreach (File file in Files)
            {
                if (file.Name == N) return file;
            }
            return null;
        }

        public bool hasPermission(String N, Folder Fo, File Fi)
        {
            if (Fo != null)
            {
                if (Fo.Permission == N || Fo.Permission == "")
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
            else if (Fi != null)
            {
                if (Fi.Permission == N || Fi.Permission == "")
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
            else
            {
                return false;
            }
        }

        //is readonly function

    }
}
