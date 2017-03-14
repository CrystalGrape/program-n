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
            Assignment assign = new Assignment(cycle);
            cycle.MemoryAlloc("a", 1);
            cycle.MemoryAlloc("b", 4);
            cycle.MemoryAlloc("c", 8);
            cycle.MemoryFree();
        }
    }
}
