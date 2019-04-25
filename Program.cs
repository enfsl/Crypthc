using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Diagnostics;
using System.ComponentModel;
using System.IO;

namespace crypthc
{
    class Program
    {
         
        static void Proc(string directory, string checksum, string file)
        {
            try
            {
                    Process myProc = new Process();
                    myProc.StartInfo.UseShellExecute = false; // for IO process 
                    myProc.StartInfo.FileName = "bash.exe";

                    myProc.StartInfo.WorkingDirectory = directory;
                    myProc.StartInfo.RedirectStandardInput = true; // input redirect cmd window
                    myProc.StartInfo.RedirectStandardOutput = true; // output redirect cmd window
                    myProc.StartInfo.CreateNoWindow = true; // Do not create any other window in operations
                 
                    myProc.Start();

                    myProc.StandardInput.Write("{0} {1}", checksum, file); // write to input window
                    myProc.StandardInput.Close(); 
                    myProc.WaitForExit();

                    Console.WriteLine(">>\n");
                    Console.Write(myProc.StandardOutput.ReadToEnd()); // redirect command to cmd
                    Console.WriteLine("\n<<");
            }
            catch (Exception ex)
            {
                Console.WriteLine("Your system may not have Linux Subsystem installed. Please check the error messages = " + ex.ToString());
            }
        }
        static void Main(string[] args)
        {
            Console.WriteLine("--------------------------------------------------------------------------");
            Console.WriteLine("| Author: M.Emre Nefesli 'jasyuiop' | The GNU General Public License v3.0 |");
            Console.WriteLine("--------------------------------------------------------------------------");
            Console.Write("Enter the full path of the file you want to do checksum : ");
            string directory = @Console.ReadLine();

            string[] filesplit = directory.Split('\\'); // get "file" from "directory" info
            string file = filesplit[filesplit.Length - 1];

            directory = directory.Replace(file, ""); // remove "file" from "directory" info

            Console.WriteLine("You want to do the Checksum of which type of hash ?");
            Console.Write("(SHA1, SHA256, SHA512, MD5) ? = ");
            string checksum = Console.ReadLine();
            if (checksum == "SHA1" || checksum == "SHA256" || checksum == "SHA512" || checksum == "MD5" ||
                checksum == "sha1" || checksum == "sha256" || checksum == "sha512" || checksum == "md5")
            {
                checksum += "sum";
                Program.Proc(directory, checksum, file);
            }
            else
            {
                Console.WriteLine("Enter a valid hash type! > (SHA1, SHA256, SHA512, MD5)");
            }

            Console.Write("Press one key to exit..");
            Console.ReadKey();
        }
    }
}
