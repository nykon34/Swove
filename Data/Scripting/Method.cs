using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LScript
{
    class Method
    {
        public String Name;
        public List<String> querys;

        public static bool methodExists(String Name)
        {
            foreach (Method m in Engine.methods)
            {
                if (m.Name.Equals(Name)) return true;
            }
            return false;
        }
        public static void doMethod(String Name)
        {
            foreach (Method method in Engine.methods)
            {
                if (method.Name.Equals(Name))
                {
                    int l = 1;
                    foreach (String query in method.querys)
                    {
                        Engine.doQuery(query, l + " (inside method '" + Name + "')");
                        l++;
                    }
                }
            }
        }
    }
}
