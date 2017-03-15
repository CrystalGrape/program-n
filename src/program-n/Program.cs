using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace program_n
{
    public class Program
    {
        private static int DataStackBaseAddr = 300;
        
        //流程控制列表
        private static string[] keyword_process = { "if", "else", "for", "while" };
        //类型关键字列表
        private static string[] keyword_type = { "int", "double", "char", "var", "int*", "double*", "char*", "var*" };
        //其它关键字
        private static string[] keyword_other = { "return", "{", "}" };
        //关键字列表
        private static List<string> keyword
        {
            get
            {
                List<string> set = new List<string>();
                set.AddRange(keyword_process);
                set.AddRange(keyword_type);
                set.AddRange(keyword_other);
                return set;
            }
        }

        public static void Main(string[] args)
        {
            if(File.Exists("a.asn"))
            {
                File.Delete("a.asn");
            }
            FileStream fs = new FileStream("demo.pn", FileMode.Open);
            StreamReader sr = new StreamReader(fs);
            List<string> block = new List<string>();            //代码块
            //Function inputFunc;
            while(!sr.EndOfStream)
            {
                string line = sr.ReadLine();
                line = line.TrimStart('\t', ' ', '\r', '\n');
                line = line.TrimEnd('\t', ' ', '\r', '\n');

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
