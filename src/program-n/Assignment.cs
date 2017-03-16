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
            for (int i = 0; i < 4 - ((bytes.Length % 4 == 0) ? 4 : (bytes.Length % 4)); i++)
            {
                tmpByteList.Add(0x00);
            }
            bytes = tmpByteList.ToArray();
            List<Int32> _Values = new List<Int32>();
            for (int i = 0; i < bytes.Length / 4 + bytes.Length % 4; i++)
            {
                _Values.Add(BitConverter.ToInt32(bytes, i * 4));
            }

            OutputObject asnFile = new OutputObject();
            asnFile.Asn($";set variable value");
            for (int i = 0; i < (varInfo.Size > _Values.Count ? _Values.Count : varInfo.Size); i++)
            {
                asnFile.Asn($"mov r0,{varInfo.Offset - i}");
                asnFile.Asn($"sub r0,dptr,r0");
                asnFile.Asn($"mov r1,{_Values[i]}");
                asnFile.Asn($"str r1,r0");
            }
            asnFile.Out();
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

            OutputObject asnFile = new OutputObject();
            asnFile.Asn($";set variable value");
            for (int i = 0; i < destVarInfo.Size; i++)
            {
                asnFile.Asn($"mov r0,{srcVarInfo.Offset - i}");
                asnFile.Asn($"sub r0,dptr,r0");
                asnFile.Asn($"ldr r1,r0");
                asnFile.Asn($"mov r0,{destVarInfo.Offset - i}");
                asnFile.Asn($"sub r0,dptr,r0");
                asnFile.Asn($"str r1,r0");
            }
            asnFile.Out();
        }

        /// <summary>
        /// 将函数返回值存储到变量varName中
        /// </summary>
        /// <param name="varName"></param>
        public void ReturnAsignVar(string varName)
        {
            LocalVariable.VarInfo varInfo = LifeCycle.GetVarInfo(varName);
            OutputObject asnFile = new OutputObject();
            asnFile.Asn($";set return variable value");

            for (int i = 0; i < varInfo.Size; i++)
            {
                asnFile.Asn($"mov r0,{varInfo.Offset - i}");
                asnFile.Asn($"sub r0,dptr,r0");
                asnFile.Asn($"ldr r1,r0");
                asnFile.Asn($"mov r0,{i}");
                asnFile.Asn($"add r0,dptr,r0");
                asnFile.Asn($"str r1,r0");
            }
        }
    }
}
