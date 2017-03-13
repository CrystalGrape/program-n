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
            LifeCycle LifeCycle = new LifeCycle();
            TranslateType type = new TranslateType(LifeCycle);
            TranslateAssignment assign = new TranslateAssignment(LifeCycle);
            type.TranslateInt("age");
            type.TranslateFloat("grade");

            assign.AssignmentVar("age", 10);
            assign.AssignmentVar("grade", 10.6);
            CommonStack.Instance.Push(LifeCycle);
            CommonStack.Instance.Pop(LifeCycle);
        }
    }
}
