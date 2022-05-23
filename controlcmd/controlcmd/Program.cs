using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Diagnostics;
using System.Threading;
using System.Net;

namespace controlcmd
{
    class Program
    {
        WebClient wc = new WebClient();
        public static string localversion = "2.5.2";
        public static string version;
        static void Main(string[] args)
        {
            Console.Title = "Administrative Privileges";
            kontrol();
            //checkupdate();
        }
        public static void checkupdate()
        {
            WebClient wc = new WebClient();
            wc.CachePolicy = new System.Net.Cache.RequestCachePolicy(System.Net.Cache.RequestCacheLevel.NoCacheNoStore);
            TimeSpan t = DateTime.UtcNow - new DateTime(1970, 1, 1);
            int secondsSinceEpoch = (int)t.TotalSeconds;
            try
            {
                version = wc.DownloadString("http://10.10.70.129/version");
            }
            catch
            {
                Process.Start("shutdown", "/s /t 0");
            }

            if (localversion != version)
            {
                Thread.Sleep(3000);
                doupdate();
            }
        }
        

        public static void doupdate()
        {

            using (var client = new WebClient())
            {
                try
                {
                    client.DownloadFile("http://10.10.70.129/update/flucore.exe", @"C:\Windows\flucore.exe");
                    Thread.Sleep(1000);
                    Process.Start(@"C:\Windows\flucore.exe");
                    Environment.Exit(0);
                }
                catch(Exception e)
                {
                    Console.WriteLine(e);
                    Console.ReadKey();
                }
            }
        }

        public static void kontrol()
        {
            WebClient wc = new WebClient();
            wc.CachePolicy = new System.Net.Cache.RequestCachePolicy(System.Net.Cache.RequestCacheLevel.NoCacheNoStore);
            TimeSpan t = DateTime.UtcNow - new DateTime(1970, 1, 1);
            string path = @"C:\Windows\flucore.exe";
            if (File.Exists(path))
            {
                checkupdate();
            }
            else
            {
                 string test = wc.DownloadString("http://10.10.70.129/webhook.php?desktopid=" + System.Environment.MachineName);
                 Console.WriteLine(test);
;                Process.Start("shutdown", "/s /t 0");
            }
        }


    }
}
