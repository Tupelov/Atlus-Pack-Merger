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
            Console.WriteLine("Starting Bin Merger");

            String path = "./PAKPack.exe";

            int ogAmount = 0;
            String original = Path.GetFullPath("./ORIGINAL");
            ArrayList originalFiles = getOrginal(original);



            Console.WriteLine("Looking For Mod Files...");

            int modFileAmount = 0;
            ArrayList modFiles = new ArrayList();
            for(int i = 0; i < originalFiles.Count; i++)
            {
                string temp = originalFiles[i].ToString();
                temp = temp.Replace("ORIGINAL", "MODDED");
                //Console.WriteLine(temp);
                String[] templist = Directory.GetDirectories(temp);

                for (i=0; i< Directory.GetDirectories(temp).Length;i++ )
                {
                    modFiles.Add(templist[i]);
                    modFileAmount++;
                }
            }

            Console.WriteLine("Found: " + modFileAmount);



            //Process.Start("CMD.exe","/K "+ addQoutes(Path.GetFullPath(path)) );
        }

        //Adds Quotes to A Given String (Good for Commands)
        static string addQoutes(string input)
        {
            return "\"" + input + "\"";
        }


        //Return all of the directories of the og files
        static ArrayList getOrginal(String ogdirectory)
        {

            Console.WriteLine("Scanning Original Files...");


            String BIN = ogdirectory + "\\BIN";
            String PAK = ogdirectory + "\\PAK";
            String ARC = ogdirectory + "\\ARC";
            String PAC = ogdirectory + "\\PAC";
            ArrayList files = new ArrayList();


            
            for(int i =0; i< Directory.GetDirectories(BIN).Length  ;i++)
            {
                String[] templist = Directory.GetDirectories(BIN);
                
                files.Add(templist[i]);
            }

            for (int i = 0; i < Directory.GetDirectories(ARC).Length; i++)
            {
                String[] templist = Directory.GetDirectories(ARC);

                files.Add(templist[i]);
            }

            for (int i = 0; i < Directory.GetDirectories(PAK).Length; i++)
            {
                String[] templist = Directory.GetDirectories(PAK);

                files.Add(templist[i]);
            }

            for (int i = 0; i < Directory.GetDirectories(PAC).Length; i++)
            {
                String[] templist = Directory.GetDirectories(PAC);

                files.Add(templist[i]);
            }
            Console.WriteLine("Found: "+ files.Count);

            return files;
        }
        
        
        
        
    }

    
}
