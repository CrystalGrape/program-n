using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace program_n
{
    public class Program
    {
        private static int DataStackBaseAddr = 300;
        public static void Main(string[] args)
        {
            if(File.Exists("a.asn"))
            {
                File.Delete("a.asn");
            }
            OutputObject asnFile = new OutputObject();
            asnFile.Asn($"mov dptr,{DataStackBaseAddr}");
            asnFile.Asn($"jmp main");
            asnFile.Out();

            Function func = new Function("test");
            func.FuncPrepare();
            func.FuncReturn("data");
        }
    }
}
