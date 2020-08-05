using System;
using System.Diagnostics;
using System.IO;

namespace Bin_Merger
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Hello World!");

            String path = "./PAKPack.exe";
            Console.WriteLine("Path: " + Path.GetFullPath(path));
            Process.Start("CMD.exe","/K "+ addQoutes(Path.GetFullPath(path)) );
        }


        static string addQoutes(string input)
        {
            return "\"" + input + "\"";
        }


    }
}
