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

            try
            {
                Console.WriteLine("Starting Bin Merger");

                String path = "./PAKPack.exe";


                String original = Path.GetFullPath("./ORIGINAL");
                ArrayList originalFiles = getOrginal(original);



                Console.WriteLine("Looking For Mod Files...");

                int modFileAmount = 0;
                ArrayList modFiles = new ArrayList();
                //Finds mods files by using original directory and swapping ORIGInal with MODDING
                for (int i = 0; i < originalFiles.Count; i++)
                {
                    string temp = originalFiles[i].ToString();
                    temp = temp.Replace("ORIGINAL", "MODDED");

                    String[] templist = Directory.GetDirectories(temp);

                    for (i = 0; i < Directory.GetDirectories(temp).Length; i++)
                    {
                        modFiles.Add(templist[i]);
                        modFileAmount++;
                    }
                }

                Console.WriteLine("Found: " + modFileAmount);

                Console.WriteLine("Comparing Checksums and Replacing...");


                //In Hind Sight this entire part looks stupid but it works

                ArrayList changed = new ArrayList();

                //Goes and iterates through the mod files and original files and compares checksums add those that are replaced to changed array for comparison
                
                for (int i = 0;  originalFiles.Count> i ; i++)
                {

                    Array ogFileArray = listFilesRecursivly(originalFiles[i].ToString());
                    for (int k = 0; modFiles.Count  > k; k++)
                    {



                        string compare = Directory.GetParent(modFiles[k].ToString()).ToString();
                        if (String.Equals(Path.GetFileName(compare), Path.GetFileName(originalFiles[i].ToString())))
                        {

                            string[] ogDirectory = listFilesRecursivly(originalFiles[i].ToString());
                            string[] modDirectory = listFilesRecursivly(modFiles[k].ToString());


                            for (int l = 0; ogDirectory.Length >l; l++)
                            {

                                if (!changed.Contains(ogDirectory[l]) && !String.Equals(CalculateMD5(ogDirectory[l]), CalculateMD5(modDirectory[l])))
                                { 
                                    changed.Add(ogDirectory[l]);
                                    File.Delete(ogDirectory[l]);
                                    File.Move(modDirectory[l], ogDirectory[l], true);
                                }

                            }

                        }
 
                    }
                }

                

                Console.WriteLine("Select your packed file version (v1; v2; v2be; v3; v3be)");




                String ver = Console.ReadLine();

                //Console.WriteLine(Path.GetFullPath(path));
                for (int i = 0; i < originalFiles.Count; i++)
                {
                    PackFolder(Path.GetFullPath(originalFiles[i].ToString()), (originalFiles[i].ToString()).ToString() + "." + Path.GetFileName(Directory.GetParent(originalFiles[i].ToString()).ToString()).ToLower() , ver , Path.GetFullPath(path));
                }

                Console.WriteLine("Done Merging!");
                Console.ReadLine();
            }
            catch (Exception e)
            {
                Console.WriteLine("The process failed: {0}", e.ToString());
            }
        }

        //Adds a grave to A Given String
        static string addGrave(string input)
        {
            return "`" + input + "`";
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


        static void PackFolder(string folderDir, string outputFile,string ver, string path)
        {
            ProcessStartInfo pakPack = new ProcessStartInfo();
            pakPack.FileName = path;

            pakPack.Arguments = $@"pack ""{folderDir}"" {ver} ""{outputFile}""";

            Console.WriteLine("PAKPack packing...");
            Process.Start(pakPack).WaitForExit();

            Process.Start(pakPack).WaitForExit();
        }

    }

    
}
