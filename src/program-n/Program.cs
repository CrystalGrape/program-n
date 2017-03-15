using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace program_n
{
    public class FuncParser
    {
        public string funcName = "";
        public int returnType { get; set; }
        public List<Function.Parameter> Args = new List<Function.Parameter>();
        public int GetType(string type)
        {
            switch (type)
            {
                case "void":
                    return 0;
                case "int":
                    return 4;
                case "char":
                    return 1;
                case "double":
                    return 8;
                default:
                    throw new Exception($"未知类型:{type}");
            }
        }
        public void Parse(string line)
        {
            List<char> Declare = line.ToList();
            int flag = 0;
            string ArgsDeclare = "";
            string ReturnType = "";
            Declare.ForEach(p => {
                switch (flag)
                {
                    case 0:
                        //函数名状态
                        if (p == '(')
                        {
                            flag = 1;
                            break;
                        }
                        funcName += p;
                        break;
                    case 1:
                        if (p == ')')
                        {
                            flag = 2;
                            break;
                        }
                        ArgsDeclare += p;
                        break;
                    case '2':
                        if(p=='-')
                        {
                            flag = 3;
                            break;
                        }
                        break;
                    case '3':
                        if (p == '>')
                        {
                            flag = 4;
                            break;
                        }
                        break;
                    case '4':
                        ReturnType += p;
                        break;
                }
            });
            returnType = GetType(ReturnType);

            List<string> _Args = ArgsDeclare.Split(',').ToList();
            _Args.ForEach(x =>
            {
                x = x.Trim();
                string[] arg = x.Split(' ');
                Function.Parameter param= new Function.Parameter();
                param.ParamName = arg[1];
                param.Size = GetType(arg[0]);
                Args.Add(param);
            });
        }
    }
    public class Program
    {
        private static int DataStackBaseAddr = 300;
        
        public static void Main(string[] args)
        {
            if(File.Exists("a.asn"))
            {
                File.Delete("a.asn");
            }
            FileStream fs = new FileStream("demo.pn", FileMode.Open);
            StreamReader sr = new StreamReader(fs);
            List<string> block = new List<string>();            //代码块

            while(!sr.EndOfStream)
            {
                string line = sr.ReadLine();
                line = line.TrimStart('\t', ' ', '\r', '\n');
                line = line.TrimEnd('\t', ' ', '\r', '\n');
                if(line.StartsWith("@"))
                {
                    line = line.Substring(1);
                    //函数开始
                    FuncParser parser = new FuncParser();
                    parser.Parse(line);
                    Console.WriteLine(line);
                }            
            }
            

            //OutputObject asnFile = new OutputObject();
            //asnFile.Asn($"mov dptr,{DataStackBaseAddr}");
            //asnFile.Asn($"jmp main");
            //asnFile.Out();

            //Function func = new Function("test");
            //func.FuncPrepare();
            //func.FuncReturn("data");
        }
    }
}
