using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel;
using System.Diagnostics;
using System.IO;

namespace crypthc
{
    class Program
    {
         
        static void Proc(string directory, string checksum, string file)
        {
            try
            {
                using (Process myProc = new Process())
                {
                    myProc.StartInfo.UseShellExecute = false;
                    myProc.StartInfo.FileName = "bash.exe"; // başlayacak proc adı
                    myProc.StartInfo.WorkingDirectory = directory; // proc'un işlem yapacağı dizin
                    myProc.StartInfo.RedirectStandardOutput = true; // çıkış 
                    myProc.StartInfo.RedirectStandardInput = true; // giriş
                    myProc.StartInfo.CreateNoWindow = true; // tek cmd ekranı

                    myProc.Start(); // proc koş

                    myProc.StandardInput.Write("{0} {1}", checksum, file); // komut satırına yaz
                    myProc.StandardInput.Close(); // girişi kapat
                    myProc.WaitForExit(); // bitişi bekle

                    // kodun tek çıktısı oluyor o yüzden başta yazdırmam lazım. galiba..

                    Console.WriteLine(">>");
                    Console.Write(myProc.StandardOutput.ReadToEnd()); // bashten dönen output'u yazdır
                    Console.WriteLine("<<");

                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Sisteminizde Linux Subsystem kurulu olmayabilir. Lütfen hata mesajını kontrol edin = " + ex.ToString());
            }
        }
        static void Main(string[] args)
        {
            Console.WriteLine("--------------------------------------------------------------------------");
            Console.WriteLine("| Author: M.Emre Nefesli 'jasyuiop' | The GNU General Public License v3.0 |");
            Console.WriteLine("--------------------------------------------------------------------------");
            Console.Write("Checksum yapacağınız dosyanın tam yolunu giriniz : ");
            string directory = @Console.ReadLine();

            string[] filesplt = directory.Split('\\'); // directory bilgisinden file'ımı aldırıyorum
            string file = filesplt[filesplt.Length - 1];

            directory = directory.Replace(file, ""); // directory bilgisinden file'ı siliyorum.

            Console.WriteLine("Hangi hash tipinin Checksum'unu yapmak istersiniz ?");
            Console.Write("(SHA1, SHA256, SHA512, MD5) ? = ");
            string checksum = Console.ReadLine();
            if (checksum == "SHA1" || checksum == "SHA256" || checksum == "SHA512" || checksum == "MD5" ||
                checksum == "sha1" || checksum == "sha256" || checksum == "sha512" || checksum == "md5")
            {
                if (checksum == "SHA1" || checksum == "sha1")
                {
                    checksum = "sha1sum ";                   
                    Program.Proc(directory, checksum, file);

                }
                else if (checksum == "SHA256" || checksum == "sha256")
                {
                    checksum = "sha256sum";
                    Program.Proc(directory, checksum, file);

                }
                else if (checksum == "SHA512" || checksum == "sha512")
                {
                    checksum = "sha512sum";
                    Program.Proc(directory, checksum, file);

                }
                else if (checksum == "MD5" || checksum == "md5")
                {
                    checksum = "md5sum";
                    Program.Proc(directory, checksum, file);

                }
            }
            else
            {
                Console.WriteLine("Geçerli bir hash tipi giriniz ! > (SHA1, SHA256, SHA512, MD5)");
            }

            Console.Write("Oluşturulan hash ve size verilen hash'i karşılaştırmak istermisiniz ? E/H = ");

            //char gverify = Convert.ToChar(Console.ReadLine());
            //if (gverify == 'E' || gverify == 'e')
            //{
            //    Console.Write("Hash'i Giriniz = ");
            //    string ghash = Console.ReadLine();
            //    ghash.ToLower();
            //    Program p = new Program();
            //    string cnhash = Sr.ReadLine();
            //    Console.WriteLine(cnhash);
                
            //}

                //    // geriye dönen hash içerisinde boşluk ve file bilgisi de yer alıyor bunları atmam lazım
                //    GlobalHash g = new GlobalHash();
                //    Console.WriteLine(g.globald);
                //    string[] th = g.globald.Split(' ');
                //    string truehash = "";
                //    for (int i = 0; i < th.Length; i++)
                //    {
                //        if (th[i] != " ")
                //        {
                //            truehash = truehash + th[i];
                //        }
                //        else break;
                //    }

                //    if (ghash == truehash)
                //        Console.WriteLine("Hash'ler karşılaştırıldı: HASH DOĞRU!!");
                //    else Console.WriteLine("Hash'ler karşılaştırıldı: HASH YANLIŞ!!");

                Console.Write("Çıkmak için bir tuşa basın..");
                Console.ReadKey();
        }
    }
}
