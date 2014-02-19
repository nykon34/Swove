using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LScript
{
    class Array
    {
        public String Name;
        public List<String> ArrayContent;

        public static Array getAClass(String Name)
        {
            foreach (Array a in Engine.arrays)
            {
                if (a.Name.Equals(Name))
                {
                    return a;
                }
            }
            return null;
        }

        public static String getArrayVar(String Name, int loc)
        {
            try
            {
                foreach (Array a in Engine.arrays)
                {
                    if (a.Name.Equals(Name))
                    {
                        return a.ArrayContent[loc];
                    }
                }
                return "";
            }
            catch
            {
                return "-Outside-";
            }
        }
    }
}
