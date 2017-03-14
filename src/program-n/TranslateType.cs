using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace program_n
{
    //类型声明编译过程
    public class TranslateType
    {
        private LifeCycle LifeCycle;
        public TranslateType(LifeCycle LifeCycle)
        {
            this.LifeCycle = LifeCycle;
        }
        public void AllocVar(string varName, int Size)
        {
            Int32 Addr = LifeCycle.MemoryAlloc(varName, Size);
        }
    }
}
