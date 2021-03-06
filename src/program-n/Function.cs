﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace program_n
{
    /// <summary>
    /// 函数环境编辑
    /// </summary>
    public class Function
    {
        public class Parameter
        {
            public string ParamName { get; set; }
            public Int32 Size { get; set; }
        }

        //参数列表
        private List<Parameter> Parameters = new List<Parameter>();
        //返回变量
        private Parameter ReturnVariable;
        //函数名
        private string FunctionName;
        //本地变量环境 
        private LocalVariable LocalEnvironment;
        //赋值工具
        private Assignment assignTool;
        public Function(string FunctionName)
        {
            LocalEnvironment = new LocalVariable();
            assignTool = new Assignment(LocalEnvironment);
            this.FunctionName = FunctionName;
        }

        public void AddParameter(string ParamName, int Size)
        {
            Parameters.Add(new Parameter { ParamName = ParamName, Size = Size });
        }
        public void SetReturn(int Size)
        {
            ReturnVariable = new Parameter { ParamName = "__return_parameter", Size = Size };
        }
        /// <summary>
        /// 函数准备编译
        /// 定义函数参数入栈规则
        /// 1、参数从左往右入栈
        /// 2、参数入栈的全部是地址
        /// 3、函数准备时对所有参数进行一次拷贝
        /// </summary>
        public void FuncPrepare()
        {
            OutputObject asnFile = new OutputObject();
            asnFile.Asn($";declare function {FunctionName}");
            asnFile.Asn($"section {FunctionName}:");
            if (!FunctionName.Equals("main"))
            {
                //如果不是主函数，必须保存现场
                asnFile.Asn($"push lr");
                asnFile.Asn($"push cpsr");
            }
            asnFile.Out();
            //按次序实例化参数
            Parameters.ForEach(x =>
            {
                LocalEnvironment.DeclareParam(x.ParamName, x.Size);
            });
        }

        public void FuncReturn(string varName)
        {
            OutputObject asnFile = new OutputObject();
            
            //释放数据栈
            LocalEnvironment.MemoryFree();
            if (FunctionName.Equals("main"))
            {
                asnFile.Asn($"end");
            }
            else
            {
                List<string> ReturnParam = new List<string>();
                ReturnParam.Add(ReturnVariable.ParamName);
                LocalEnvironment.PrepareParam(ReturnParam);
                //恢复现场
                asnFile.Asn($"pop cpsr");
                asnFile.Asn($"pop lr");
                asnFile.Asn($"ret");
            }
            asnFile.Out();
        }
    }
}
