using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Swove.Data.FS
{
    class Folder
    {
        public Folder Parent;
        public String Name;
        public Boolean ReadOnly;
        public String Permission; //If blank, everyone can use
    }
}
