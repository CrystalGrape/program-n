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
            LocalVariable cycle = new LocalVariable();
            TranslateAssignment assign = new TranslateAssignment(cycle);
            cycle.MemoryAlloc("sex", sizeof(byte));
            assign.AssignmentVar("sex", 'a');
            cycle.MemoryAlloc("sex2", sizeof(byte));
            assign.VarAsignVar("sex2", "sex");
        }
    }
}
