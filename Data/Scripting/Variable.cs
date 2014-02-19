using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LScript
{
    class Variable
    {
        public String Name;
        public String Value;

        public static String getVariable(String Name)
        {
            foreach (Variable v in Engine.variables)
            {
                if (v.Name.Equals(Name))
                {
                    return v.Value;
                }
            }
            return "";
        }
        public static Variable getVClass(String Name)
        {
            foreach (Variable v in Engine.variables)
            {
                if (v.Name.Equals(Name))
                {
                    return v;
                }
            }
            return null;
        }
    }
}
