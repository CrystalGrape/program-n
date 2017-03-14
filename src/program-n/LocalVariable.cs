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

            OutputObject asnFile = new OutputObject();
            asnFile.Asn($";alloc local variable");
            for (Int32 Index = 0; Index < MemorySize; Index++)
            {
                asnFile.Asn($"mov r0,0");
                asnFile.Asn($"push r0");
            }
            if (SymbolMap.ContainsKey(varName))
                throw new Exception($"该作用域内变量重复定义:{varName}");

            SymbolMap.Add(varName, new VarInfo { Offset = 0, Size = MemorySize });
            string[] keyArray = SymbolMap.Keys.ToArray();
            for (int i = 0; i < keyArray.Length; i++)
            {
                SymbolMap[keyArray[i]].Offset += MemorySize;
            }

            asnFile.Out();
            return MemorySize;
        }

        /// <summary>
        /// 准备函数参数
        /// </summary>
        /// <param name="varName"></param>
        /// <param name="Size"></param>
        public void PrepareParam(List<string> Params)
        {
            OutputObject asnFile = new OutputObject();
            asnFile.Asn($";prepare parameter variabl");
            asnFile.Asn($"mov dptr,sp");
            Params.ForEach(x =>
            {
                VarInfo varInfo = GetVarInfo(x);

                for (int i = 0; i < varInfo.Size; i++)
                {
                    asnFile.Asn($"mov r0,{varInfo.Offset - i}");
                    asnFile.Asn($"sub r0,sp,r0");
                    asnFile.Asn($"ldr r1,r0");
                    asnFile.Asn($"str r1,dptr");
                    asnFile.Asn($"mov r0,1");
                    asnFile.Asn($"add dptr,dptr,r0");
                }
            });
            asnFile.Out();
        }

        /// <summary>
        /// 声明函数参数，不进行压栈，直接对sp进行操作
        /// </summary>
        /// <param name="varName"></param>
        /// <param name="Size"></param>
        /// <returns></returns>
        public Int32 DeclareParam(string varName, Int32 Size)
        {
            Int32 MemorySize = Size / 4 + Size % 4;
            OutputObject asnFile = new OutputObject();
            asnFile.Asn($";declare parameter variable");     
            asnFile.Asn($"mov r0,{MemorySize}");
            asnFile.Asn($"add sp,sp,r0");
            SymbolMap.Add(varName, new VarInfo { Offset = 0, Size = MemorySize });
            string[] keyArray = SymbolMap.Keys.ToArray();
            for (int i = 0; i < keyArray.Length; i++)
            {
                SymbolMap[keyArray[i]].Offset += MemorySize;
            }
            asnFile.Out();

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

            OutputObject asnFile = new OutputObject();
            asnFile.Asn($";free all local variable");
            for (int i = 0; i < StackSize; i++)
            {
                asnFile.Asn($"pop r0");
            }
            asnFile.Out();
        }
    }
}
