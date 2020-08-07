using System;
using System.Collections;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Net.Http.Headers;
using System.Security.Cryptography;
using System.Text;

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

            Console.WriteLine("Comparing Checksums and Replacing...");


            //In Hind Sight this entire part looks stupid but it works

            ArrayList changed = new ArrayList();


            for (int i = 0; i < originalFiles.Count; i++)
            {

                Array ogFileArray = listFilesRecursivly(originalFiles[i].ToString());
                for(int k = 0; k< modFiles.Count; k++)
                {
                    
                    string compare = Directory.GetParent(modFiles[k].ToString()).ToString();
                    if (String.Equals(  Path.GetFileName(compare)  ,Path.GetFileName(originalFiles[i].ToString()) ))
                    {
                        
                        string[] ogDirectory =  listFilesRecursivly(originalFiles[i].ToString());
                        string[] modDirectory = listFilesRecursivly(modFiles[k].ToString());
                        for (int l = 0; l < ogDirectory.Length; l++)
                        {
                            
                            
                            if(!String.Equals ( CalculateMD5(ogDirectory[l]),CalculateMD5(modDirectory[l]) ))
                            {
                                Console.WriteLine("Not the same!: " + modDirectory[l]);
                                Console.WriteLine(CalculateMD5(ogDirectory[l]));
                                Console.WriteLine(CalculateMD5(modDirectory[l]));
                                
                            }
                        }
                    }
                    Console.WriteLine("Medium Loop");
                }
                Console.WriteLine("Big Loop");
            }


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
        
        
        static string[] listFilesRecursivly(string directory)
        {

            string[] filePaths = Directory.GetFiles(directory, "",
                                         SearchOption.AllDirectories);
            return filePaths;
        }

        static string CalculateMD5(string filename)
        {
            using var md5 = MD5.Create();
            {
                using var stream = File.OpenRead(filename);
                {
                    var hash = md5.ComputeHash(stream);
                    stream.Close();
                    return BitConverter.ToString(hash).Replace("-", String.Empty).ToLower();
                }
            }
        }




    }

    
}
