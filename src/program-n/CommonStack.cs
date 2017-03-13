using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace program_n
{
    /// <summary>
    /// 公有栈
    /// </summary>
    public class CommonStack
    {
        private readonly Int32 BaseAddr = 600;
        private Int32 StackTopAddr;
        public static CommonStack Instance = new CommonStack();
        private CommonStack()
        {
            StackTopAddr = BaseAddr;
        }

        public void Push(LifeCycle lifeCycle)
        {
            StackTopAddr = lifeCycle.ProtectScence(StackTopAddr);
        }

        public void Pop(LifeCycle lifeCycle)
        {
            StackTopAddr = lifeCycle.RecoveryScence(StackTopAddr);
        }
    }
}
