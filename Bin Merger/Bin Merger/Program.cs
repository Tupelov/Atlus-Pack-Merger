using System;
using System.Collections;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Net.Http.Headers;

namespace Bin_Merger
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Hello World!");

            String path = "./PAKPack.exe";
            //Console.WriteLine("Path: " + Path.GetFullPath(path));

            String original = Path.GetFullPath("./original");
            getOrginal(original);
            //Process.Start("CMD.exe","/K "+ addQoutes(Path.GetFullPath(path)) );
        }


        static string addQoutes(string input)
        {
            return "\"" + input + "\"";
        }

        static ArrayList getOrginal(String ogdirectory)
        {
            String bin = ogdirectory + "/BIN";
            String PAK = ogdirectory + "/PAK";
            String ARC = ogdirectory + "/ARC";
            String PAC = ogdirectory + "/PAC";
            ArrayList files = new ArrayList();
            

            for(int i =0; i< Directory.GetFiles(bin).Length  ;i++)
            {
                String[] templist = Directory.GetFiles(bin);

                files.Add(templist[i]);
            }
            
            for(int i = 0; i< files.Count; i++)
            {
                Console.WriteLine(files[i]);
            }


            return files;
        }
    }

    
}
