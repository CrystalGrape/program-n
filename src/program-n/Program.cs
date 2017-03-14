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
            LifeCycle cycle = new LifeCycle();
            TranslateType type = new TranslateType(cycle);
            TranslateAssignment assign = new TranslateAssignment(cycle);
            type.AllocVar("age", sizeof(int));
            type.AllocVar("grade", sizeof(double));
            type.AllocVar("sex", sizeof(byte));
            assign.AssignmentVar("age", 20);
            assign.AssignmentVar("grade", 12.4);
            assign.AssignmentVar("sex", 'a');
        }
    }
}
