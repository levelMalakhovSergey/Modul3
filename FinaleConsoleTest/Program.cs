using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using SystemFileReader2;

namespace FinaleConsoleTest
{
    class Program
    {
       
        static void Main(string[] args)
        {
            SystemFileReader systemFileReader = new SystemFileReader();
            systemFileReader.ReadFolder(@"D:\Video");
            Console.WriteLine(systemFileReader);
            Console.ReadKey();
        }
    }
}
