using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace program_n
{
    /// <summary>
    /// 生存周期(局部变量)
    /// </summary>
    public class LocalVariable
    {
        public class VarInfo
        {
            public Int32 Offset { get; set; }
            public Int32 Size { get; set; }
        }
        private Dictionary<string, VarInfo> SymbolMap = new Dictionary<string, VarInfo>();

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
            if (SymbolMap.ContainsKey(varName))
                throw new Exception($"该作用域内变量重复定义:{varName}");
            SymbolMap.Add(varName, new VarInfo { Offset = 0, Size = MemorySize });
            string[] keyArray = SymbolMap.Keys.ToArray();
            for (int i = 0; i < keyArray.Length; i++)
            {
                SymbolMap[keyArray[i]].Offset += MemorySize;
            }
            OutputObject.Out(OutputStr);

            return MemorySize;
        }

        /// <summary>
        /// 获取变量相对于栈顶的偏移量
        /// </summary>
        /// <param name="varName"></param>
        /// <returns></returns>
        public VarInfo GetVarInfo(string varName)
        {
            if(!SymbolMap.ContainsKey(varName))
            {
                throw new Exception($"变量{varName}未声明");
            }
            return SymbolMap[varName];
        }

        /// <summary>
        /// 释放局部变量
        /// </summary>
        public void MemoryFree()
        {
            Int32 StackSize = 0;
            string[] keyArray = SymbolMap.Keys.ToArray();
            for (int i = 0; i < keyArray.Length; i++)
            {
                StackSize += SymbolMap[keyArray[i]].Size;
            }

            string OutputStr = ";free all local variable" + Environment.NewLine;
            for (int i = 0; i < StackSize; i++)
            {
                OutputStr += "pop r0" + Environment.NewLine;
            }
            OutputObject.Out(OutputStr);
        }
    }
}
