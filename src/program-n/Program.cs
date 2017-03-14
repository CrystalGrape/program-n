using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace program_n
{
    public class Program
    {
        public static void Main(string[] args)
        {
            if(File.Exists("a.asn"))
            {
                File.Delete("a.asn");
            }
            Function func = new Function("main");
            func.AddParameter("argc", 4);
            func.AddParameter("args", 8);
            func.FuncPrepare();
        }
    }
}
