using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace program_n
{
    /// <summary>
    /// 变量赋值编译过程
    /// </summary>
    public class TranslateAssignment
    {
        private LifeCycle LifeCycle;
        public TranslateAssignment(LifeCycle LifeCycle)
        {
            this.LifeCycle = LifeCycle;
        }
        public void AssignmentVar(string varName, dynamic Value)
        {
            Int32 Offset = LifeCycle.GetOffset(varName);           
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
            for (int i = 0; i < _Values.Count; i++)
            {
                OutputStr += $"mov r0,{Offset - i}" + Environment.NewLine
                    + $"sub r0,sp,r0" + Environment.NewLine
                    + $"mov r1,{_Values[i]}" + Environment.NewLine
                    + $"str r1,r0" + Environment.NewLine;
            }
            OutputObject.Out(OutputStr);
        }
    }
}
