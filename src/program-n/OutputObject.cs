using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace program_n
{
    public class OutputObject
    {
        private static String _objectFile;
        public static String objectFile
        {
            get
            {
                return _objectFile?? "a.asn";
            }
            set
            {
                _objectFile = value;
            }
        }
        public static void Out(string asnStr)
        {
            FileStream fs = new FileStream(objectFile, FileMode.Append);
            byte[] arr = Encoding.ASCII.GetBytes(asnStr);
            fs.Write(arr, 0, arr.Length);
            fs.Flush();
            fs.Dispose();
        }
    }
}
