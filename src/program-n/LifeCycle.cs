using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace program_n
{
    public class LifeCycle
    {
        private readonly Int32 StackBaseAddr = 200;
        private Int32 StackTop;
        public LifeCycle()
        {
            StackTop = StackBaseAddr;
        }

        public Int32 MemoryAlloc(Int32 Size)
        {
            Int32 MemorySize = Size / 4 + Size % 4;
            StackTop += MemorySize;
            return StackTop - MemorySize;
        }

        private Dictionary<string, Int32> SymbolMap = new Dictionary<string, int>();

        public void Set(string varName, Int32 Addr)
        {
            SymbolMap.Add(varName, Addr);
        }
        public Int32 Get(string varName)
        {
            if(!SymbolMap.ContainsKey(varName))
            {
                throw new Exception($"变量{varName}未声明");
            }
            return SymbolMap[varName];
        }


        /// <summary>
        /// 保护局部变量现场
        /// </summary>
        /// <param name="BaseDestAddr"></param>
        public Int32 ProtectScence(Int32 BaseDestAddr)
        {
            string OutputStr = ";protect scence" + Environment.NewLine;
            for (Int32 Addr = StackBaseAddr; Addr < StackTop; Addr++)
            {
                OutputStr += $"ldr r0,{Addr}" + Environment.NewLine
                    + $"str r0,{BaseDestAddr}" + Environment.NewLine;
                BaseDestAddr++;
            }
            OutputObject.Out(OutputStr);
            return BaseDestAddr;
        }

        public Int32 RecoveryScence(Int32 TopSrcAddr)
        {
            string OutputStr = ";recovery scence" + Environment.NewLine;
            for (Int32 Addr = StackTop - 1; Addr >= StackBaseAddr; Addr--)
            {
                TopSrcAddr--;
                OutputStr += $"ldr r0,{TopSrcAddr}" + Environment.NewLine
                    + $"str r0,{Addr}" + Environment.NewLine;               
            }
            OutputObject.Out(OutputStr);
            return TopSrcAddr;
        }
    }
}
