using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace program_n
{
    public class TranslateType
    {
        private LifeCycle LifeCycle;
        public TranslateType(LifeCycle LifeCycle)
        {
            this.LifeCycle = LifeCycle;
        }
        public void TranslateInt(string varName)
        {
            Int32 Addr = LifeCycle.MemoryAlloc(4);
            LifeCycle.Set(varName, Addr);
        }

        public void TranslateChar(string varName)
        {
            Int32 Addr = LifeCycle.MemoryAlloc(1);
            LifeCycle.Set(varName, Addr);
        }
        public void TranslateFloat(string varName)
        {
            Int32 Addr = LifeCycle.MemoryAlloc(4);
            LifeCycle.Set(varName, Addr);
        }

        public void TranslateDouble(string varName)
        {
            Int32 Addr = LifeCycle.MemoryAlloc(8);
            LifeCycle.Set(varName, Addr);
        }
    }
}
