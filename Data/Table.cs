using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Swove.Data.FS;

namespace Swove.Data
{
    class Table
    {
        public String cd = ""; //column details Name¬0}name¬1}
        public String t = ""; //Rows - Starting from 0+ DATA¬MOREDATA}
        public FileSystem FS = new FileSystem();
        public Data.FS.File FileToSave = null;
        public int rows = 0;

        public Table(FileSystem FileSys)
        {
            FS = FileSys;
        }

        public void importFromFile(File F)
        {
            if (F != null)
            {
                foreach (String line in F.Data)
                {
                    doQuery(line.Replace("~", "¬"));
                }
                FileToSave = F;
            }
        }

        public void exportToFile(File File)
        {
            if (File != null)
            {
                File.Data.Clear();
                String[] d;
                String sb = "column ";
                if (cd.Trim() != "")
                {
                    foreach (String col in cd.Split('}'))
                    {
                        d = col.Split('¬');
                        sb += d[0] + ",";
                    }
                    sb = sb.TrimEnd(',');
                    File.Data.Add(sb);
                }
                if (!t.Equals(""))
                {
                    foreach (String row in t.Split('}'))
                    {
                        if (!row.Equals(""))
                        {
                            sb = "insert ";
                            d = row.Split('¬');
                            foreach (String value in d)
                            {
                                sb += value.Replace("~", "") + "~";
                            }
                            sb = sb.TrimEnd('~');
                            File.Data.Add(sb);
                        }
                    }
                }
            }
        }

        public int getArrayNumber(String name)
        {
            int outp = -1;
            String[] d;
            foreach (String data in cd.Split('}'))
            {
                d = data.Split('¬');
                if (data.StartsWith(name + "¬"))
                {
                    outp = int.Parse(d[1]);
                }
            }
            return outp;
        }

        public bool isTrue(String op, String a, String b)
        {
            bool outp = false;
            switch (op)
            {
                case "%":
                    if (a.Contains(b))
                    {
                        outp = true;
                    }
                    break;
                case "!%":
                    if (!a.Contains(b))
                    {
                        outp = true;
                    }
                    break;
                case "=":
                    if (a.Equals(b))
                    {
                        outp = true;
                    }
                    break;
                case "!":
                    if (Double.Parse(a) != Double.Parse(b))
                    {
                        outp = true;
                    }
                    break;
                case ">":
                    if (Double.Parse(a) > Double.Parse(b))
                    {
                        outp = true;
                    }
                    break;
                case "<":
                    if (Double.Parse(a) < Double.Parse(b))
                    {
                        outp = true;
                    }
                    break;
                case ">=":
                    if (Double.Parse(a) >= Double.Parse(b))
                    {
                        outp = true;
                    }
                    break;
                case "<=":
                    if (Double.Parse(a) <= Double.Parse(b))
                    {
                        outp = true;
                    }
                    break;
            }
            if (b == "*") { outp = true; }
            return outp;
        }

        public object doQuery(String query)
        {
            String[] words = query.Split(' ');
            String[] speech = query.Split('"');
            String[] d;
            int l = 0;
            switch (words[0].ToLower())
            {
                case "select": //SELECT name (=, %) "Liam" || Select COLUMN (=, %) "VALUE"
                    String outps = "";
                    foreach (String row in t.Split('}'))
                    {
                        try
                        {
                            d = row.Split('¬');
                            if (isTrue(words[2], d[getArrayNumber(words[1])], speech[1]))
                            {
                                outps += row + "}";
                            }

                        }
                        catch { }
                    }
                    return outps.Trim();
                case "delete":
                    foreach (String row in t.Split('}'))
                    {
                        try
                        {
                            d = row.Split('¬');
                            if (isTrue(words[2], d[getArrayNumber(words[1])], speech[1]))
                            {
                                t = t.Replace(row + "}", "");
                            }
                        }
                        catch { }
                    }
                    return true;
                case "insert": //insert Liam¬Allan¬1997
                    int a = 0; //Array number
                    String str = "";
                    String w;
                    rows++;
                    foreach (String word in words)
                    {
                        if (a != 0)
                        {
                            w = word.Replace("!AI!", rows.ToString());
                            str += w + " ";
                        }
                        a++;
                    }
                    str = str.Trim();
                    if (!str.EndsWith("¬")) { str += "¬"; }
                    t += str + "}";
                    return true;
                case "row":
                    int outpi = 0;
                    foreach (String row in t.Split('}'))
                    {
                        try
                        {
                            d = row.Split('¬');
                            if (isTrue(words[2], d[getArrayNumber(words[1])], speech[1]))
                            {
                                outpi++;
                            }
                        }
                        catch { }
                    }
                    return outpi;
                case "update":
                    String ns = "";
                    if (speech[1] != "")
                    {
                        try
                        {
                            foreach (String row in t.Split('}'))
                            {
                                d = row.Split('¬');
                                if (isTrue(words[3], d[getArrayNumber(words[2])], speech[1]))
                                {
                                    //Liam¬20¬20¬}
                                    //     ^   ^ - Would replace both 20s because they end in the same number
                                    ns = row.Replace(d[getArrayNumber(words[1])] + "¬", speech[3] + "¬");
                                    t = t.Replace(row, ns);
                                }
                            }
                            return true;
                        }
                        catch { return false; }
                    }
                    else
                    {
                        return false;
                    }
                case "column":
                    l = 0;
                    foreach (String col in words[1].Split(','))
                    {
                        l = cd.Split('}').Length - 1;
                        cd += col + "¬" + l + "}";
                    }
                    return true;
                case "nc":
                    l = 0;
                    foreach (String col in words[1].Split(','))
                    {
                        l = cd.Split('}').Length - 1;
                        cd += col + "¬" + l + "}";
                        if (t != "")
                        {
                            foreach (String row in t.Split('}'))
                            {
                                if (row != "")
                                {
                                    t = t.Replace(row, row + "NULL¬");
                                }
                            }
                        }
                    }
                    return true;
                case "empty":
                    t = "";
                    cd = "";
                    return true;
                default:
                    return false;
            }
        }
    }
}
