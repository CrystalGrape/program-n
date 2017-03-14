using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace program_n
{
    /// <summary>
    /// 变量赋值编译过程
    /// </summary>
    public class Assignment
    {
        private LocalVariable LifeCycle;
        public Assignment(LocalVariable LifeCycle)
        {
            this.LifeCycle = LifeCycle;
        }
        /// <summary>
        /// 变量赋值
        /// </summary>
        /// <param name="varName"></param>
        /// <param name="Value"></param>
        public void AssignmentVar(string varName, dynamic Value)
        {
            LocalVariable.VarInfo varInfo = LifeCycle.GetVarInfo(varName);  
            byte[] bytes = BitConverter.GetBytes(Value);
            List<byte> tmpByteList = bytes.ToList();
            for (int i = 0; i < 4 - bytes.Length % 4; i++)
            {
                tmpByteList.Add(0x00);
            }
            bytes = tmpByteList.ToArray();
            List<Int32> _Values = new List<Int32>();
            for (int i = 0; i < bytes.Length / 4 + bytes.Length % 4; i++)
            {
                _Values.Add(BitConverter.ToInt32(bytes, i * 4));
            }
            string OutputStr = $";set variable value" + Environment.NewLine;
            for (int i = 0; i < varInfo.Size; i++)
            {
                OutputStr += $"mov r0,{varInfo.Offset - i}" + Environment.NewLine
                    + $"sub r0,sp,r0" + Environment.NewLine
                    + $"mov r1,{_Values[i]}" + Environment.NewLine
                    + $"str r1,r0" + Environment.NewLine;
            }
            OutputObject.Out(OutputStr);
        }

        /// <summary>
        /// 变量对变量赋值
        /// </summary>
        /// <param name="destName"></param>
        /// <param name="srcName"></param>
        public void VarAsignVar(string destName, string srcName)
        {
            LocalVariable.VarInfo destVarInfo = LifeCycle.GetVarInfo(destName);
            LocalVariable.VarInfo srcVarInfo = LifeCycle.GetVarInfo(srcName);

            string OutputStr = $";set variable value" + Environment.NewLine;
            for (int i = 0; i < destVarInfo.Size; i++)
            {
                OutputStr += $"mov r0,{srcVarInfo.Offset - i}" + Environment.NewLine
                    + $"sub r0,sp,r0" + Environment.NewLine
                    + $"ldr r1,r0" + Environment.NewLine
                    + $"mov r0,{destVarInfo.Offset - i}" + Environment.NewLine
                    + $"sub r0,sp,r0" + Environment.NewLine
                    + $"str r1,r0" + Environment.NewLine;
            }
            OutputObject.Out(OutputStr);
        }
    }
}
