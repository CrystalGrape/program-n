using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace program_n
{
    /// <summary>
    /// 生存周期(局部变量)
    /// </summary>
    public class LifeCycle
    {
        private Dictionary<string, Int32> SymbolMap = new Dictionary<string, int>();
        /// <summary>
        /// 声明一个局部变量
        /// </summary>
        /// <param name="Size"></param>
        /// <returns></returns>
        public Int32 MemoryAlloc(string varName, Int32 Size)
        {
            Int32 MemorySize = Size / 4 + Size % 4;
            string OutputStr = ";alloc local variable" + Environment.NewLine;
            for (Int32 Index = 0; Index < MemorySize; Index++)
            {
                OutputStr += "mov r0,0" + Environment.NewLine
                    + $"push r0" + Environment.NewLine;
            }
            SymbolMap.Add(varName, 0);
            string[] keyArray = SymbolMap.Keys.ToArray();
            for (int i = 0; i < keyArray.Length; i++)
            {
                SymbolMap[keyArray[i]] += MemorySize;
            }
            OutputObject.Out(OutputStr);

            return MemorySize;
        }

        /// <summary>
        /// 获取变量相对于栈顶的偏移量
        /// </summary>
        /// <param name="varName"></param>
        /// <returns></returns>
        public Int32 GetOffset(string varName)
        {
            if(!SymbolMap.ContainsKey(varName))
            {
                throw new Exception($"变量{varName}未声明");
            }
            return SymbolMap[varName];
        }
    }
}
