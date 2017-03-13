using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace program_n
{
    public class TranslateAssignment
    {
        private LifeCycle LifeCycle;
        public TranslateAssignment(LifeCycle LifeCycle)
        {
            this.LifeCycle = LifeCycle;
        }
        public void AssignmentVar(string varName, dynamic Value)
        {
            Int32 Addr = LifeCycle.Get(varName);           
            byte[] bytes = BitConverter.GetBytes(Value);
            Int32 _Value = BitConverter.ToInt32(bytes, 0);
            string OutputStr = $"mov r0,{_Value}" + Environment.NewLine
                   + $"str r0,{Addr}" + Environment.NewLine;
            OutputObject.Out(OutputStr);
        }
    }
}
