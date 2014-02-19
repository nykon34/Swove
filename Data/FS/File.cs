using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Swove.Data.FS
{
    class File
    {
        public String Name;
        public Boolean ReadOnly;
        public String Permission;
        public String Extension;
        public List<String> Data = new List<String>();
        public Folder Parent;
    }
}
