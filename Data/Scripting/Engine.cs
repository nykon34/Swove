using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Swove.Data;

namespace LScript
{
    class Engine
    {
        public static FileSystem FS;
        public static Swove.Data.Program.SDT Table;
        public static bool METHOD_ADD = false;
        public static bool IF_TRUE = false;
        public static bool FOR_TRUE = false;
        public static bool IFELSE_ACTIVE = false;
        public static List<Variable> variables = new List<Variable>();
        public static List<Array> arrays = new List<Array>();

        public static List<Method> methods = new List<Method>();
        public static List<String> cmethod = new List<String>();
        public static String mname = "";

        public static String forArray = "";
        public static int forIndex = 0;
        public static List<String> fore = new List<String>();

        public static Double doMath(String sum)
        {
            Double outp = 0;
            String op = "+";
            foreach (String c in sum.Split(' '))
            {
                switch (c)
                {
                    case "+":
                        op = "+";
                        break;
                    case "-":
                        op = "-";
                        break;
                    case "/":
                        op = "/";
                        break;
                    case "*":
                        op = "*";
                        break;
                    default:
                        switch (op)
                        {
                            case "+":
                                outp += Double.Parse(c);
                                break;
                            case "-":
                                outp -= Double.Parse(c);
                                break;
                            case "/":
                                outp /= Double.Parse(c);
                                break;
                            case "*":
                                outp *= Double.Parse(c);
                                break;
                        }
                        break;
                }
            }
            return outp;
        }

        public static void doQuery(String inp, String Line)
        {
            bool ACTIVE = true;
            if (METHOD_ADD)
            {
                if (inp.StartsWith("-"))
                {
                    cmethod.Add(inp.Remove(0, 1));
                    ACTIVE = false;
                }
            }
            if (IFELSE_ACTIVE)
            {
                if (inp.StartsWith("-"))
                {
                    inp = inp.Remove(0, 1);
                }
            }
            if (FOR_TRUE)
            {
                if (inp.StartsWith("-"))
                {
                    fore.Add(inp.Remove(0, 1));
                    ACTIVE = false;
                }
            }
            if (ACTIVE)
            {
                String c = "";
                String c1 = "";
                String sum = "";
                String qur = "";
                Boolean co = false;
                Boolean co1 = false;
                Boolean math = false;
                Boolean tab = false;
                foreach (Char ch in inp.ToCharArray())
                {
                    //VARIABLES AGAIN WITH !>
                    if (ch == '$')
                    {
                        if (co1 == false)
                        {
                            co1 = true;
                        }
                        else
                        {
                            co1 = false;
                            c1 = c1.Replace("$", "");
                            inp = inp.Replace("$" + c1 + "$", Variable.getVariable(c1));
                        }
                    }
                    if (co1)
                    {
                        if (ch != '$') c1 += ch;
                    }
                }



                foreach (Char ch in inp.ToCharArray())
                {
                    //VARIABLES>
                    if (ch == '%')
                    {
                        if (co == false)
                        {
                            co = true;
                        }
                        else
                        {
                            co = false;
                            c = c.Replace("%", "");
                            if (c.Contains("|"))
                            {
                                //vara Names "Liam,Sophie"
                                //show %Names|1%
                                String[] values = c.Split('|');
                                inp = inp.Replace("%" + c + "%", Array.getArrayVar(values[0], int.Parse(values[1])));
                            }
                            else
                            {
                                inp = inp.Replace("%" + c + "%", Variable.getVariable(c));
                            }
                        }
                    }
                    if (co)
                    {
                        if (ch != '%') c += ch;
                    }
                }

                foreach (Char ch in inp.ToCharArray())
                {
                    //MATH>
                    if (ch == '{')
                    {
                        math = true;
                    }
                    else if (ch == '}')
                    {
                        math = false;
                        inp = inp.Replace("{" + sum.Replace("{", "") + "}", doMath(sum.Replace("{", "")).ToString());
                    }
                    if (math) sum += ch;
                }

                foreach (Char ch in inp.ToCharArray())
                {
                    //SYSTEM DATABASE
                    if (ch == '[')
                    {
                        tab = true;
                    }
                    else if (ch == ']')
                    {
                        tab = false;
                        inp = inp.Replace("[" + qur.Replace("[", "") + "]", Table.t.doQuery(qur.Replace("[", "")).ToString());
                    }
                    if (tab) qur += ch;
                }
            }

            String[] args = inp.Split(' ');
            String[] speech = inp.Split('"');

            try
            {
                switch (args[0].ToLower())
                {
                    case "show": //show Text goes here
                        int n = 0;
                        foreach (String word in args)
                        {
                            if (word == "\\n")
                            {
                                Console.WriteLine();
                            }
                            else
                            {
                                if (n > 0) Console.Write(word + " ");
                                if (n == 0) n++;
                            }
                        }
                        break;
                    case "if": //if blah == blah
                        IF_TRUE = false;
                        IFELSE_ACTIVE = false;
                        switch (args[2])
                        {
                            case "==":
                                if (args[1] == args[3])
                                {
                                    IF_TRUE = true;
                                }
                                break;
                            case "!=":
                                if (args[1] != args[3])
                                {
                                    IF_TRUE = true;
                                }
                                break;
                            case ">":
                                if (Double.Parse(args[1]) > Double.Parse(args[3]))
                                {
                                    IF_TRUE = true;
                                }
                                break;
                            case "<":
                                if (Double.Parse(args[1]) < Double.Parse(args[3]))
                                {
                                    IF_TRUE = true;
                                }
                                break;
                            case ">=":
                                if (Double.Parse(args[1]) >= Double.Parse(args[3]))
                                {
                                    IF_TRUE = true;
                                }
                                break;
                            case "<=":
                                if (Double.Parse(args[1]) <= Double.Parse(args[3]))
                                {
                                    IF_TRUE = true;
                                }
                                break;
                            case "is_num":
                                try
                                {
                                    Double.Parse(args[1]);
                                    IF_TRUE = true;
                                }
                                catch { }
                                break;
                        }
                        if (IF_TRUE)
                        {
                            IFELSE_ACTIVE = true;
                        }
                        break;
                    case "else": //else
                        IFELSE_ACTIVE = false;
                        if (IF_TRUE == false)
                        {
                            IFELSE_ACTIVE = true;
                        }
                        break;
                    case "end": //end
                        IFELSE_ACTIVE = false;
                        if (FOR_TRUE)
                        {
                            foreach (String value in Array.getAClass(forArray).ArrayContent)
                            {
                                if (value != "")
                                {
                                    foreach (String query in fore)
                                    {
                                        if (query != "")
                                        {
                                            doQuery(query.Replace("%value%", value), "Foreach");
                                        }
                                    }
                                }
                            }
                            FOR_TRUE = false;
                        }
                        if (METHOD_ADD)
                        {
                            methods.Add(new Method { Name = mname, querys = new List<String>(cmethod) });
                            cmethod.Clear();
                        }
                        METHOD_ADD = false;
                        break;
                    case "method": //method Method name
                        if (!Method.methodExists(args[1]))
                        {
                            cmethod.Clear();
                            METHOD_ADD = true;
                            mname = args[1];
                        }
                        break;
                    case "methods": //methods
                        foreach (Method method in methods)
                        {
                            Console.WriteLine("\n" + method.Name);
                            foreach (String query in method.querys)
                            {
                                Console.WriteLine("   > " + query);
                            }
                        }
                        break;
                    case "vars": //methods
                        foreach (Variable v in variables)
                        {
                            Console.WriteLine(" " + v.Name + " > " + v.Value);
                        }
                        break;
                    case "arrays": //methods
                        foreach (Array a in arrays)
                        {
                            Console.WriteLine("\n " + a.Name);
                            foreach (String v in a.ArrayContent)
                            {
                                Console.WriteLine("   > " + v);
                            }
                        }
                        break;
                    case "var": //var NAME "Value"
                        if (Variable.getVClass(args[1]) == null)
                        {
                            variables.Add(new Variable { Name = args[1], Value = speech[1] });
                        }
                        else
                        {
                            Variable.getVClass(args[1]).Value = speech[1];
                        }
                        break;
                    case "array": //array Names CHAR "Liam,Sophie"
                        if (Array.getAClass(args[1]) == null)
                        {
                            arrays.Add(new Array { Name = args[1], ArrayContent = speech[1].Split(args[2].ToCharArray()[0]).ToList() });
                        }
                        else
                        {
                            Array.getAClass(args[1]).ArrayContent = speech[1].Split(',').ToList();
                        }
                        break;
                    case "input": //input VARNAME "Disp text"
                        Console.Write(speech[1]); String input = Console.ReadLine();
                        if (Variable.getVClass(args[1]) == null)
                        {
                            variables.Add(new Variable { Name = args[1], Value = input });
                        }
                        else
                        {
                            Variable.getVClass(args[1]).Value = input;
                        }
                        break;
                    case "include": //include "File"
                        foreach (String line in FS.getFile(speech[1]).Data)
                        {
                            doQuery(line, "");
                        }
                        break;
                    case "loop": //loop #times METHODNAME
                        for (int la = 0; la < int.Parse(args[1]); la++)
                        {
                            Method.doMethod(args[2]);
                        }
                        break;
                    case "trim": //trim VAR char
                        Variable.getVClass(args[1]).Value = Variable.getVariable(args[1]).Trim(char.Parse(args[2]));
                        break;
                    case "foreach": //foreach ARRAY
                        forArray = args[1];
                        forIndex = 0;
                        FOR_TRUE = true;
                        break;
                    default:
                        Method.doMethod(args[0]);
                        break;
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(" - Error -");
                Console.WriteLine(e.Message);
                Console.WriteLine(e.StackTrace);
                Console.WriteLine(e.Source);
                Console.WriteLine(e.Data);
                Console.WriteLine("Error is on line " + Line + ": " + inp);
            }
            finally { }
        }
    }
}
