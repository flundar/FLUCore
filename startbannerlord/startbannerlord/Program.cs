using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Diagnostics;
using System.Threading;

namespace startbannerlord
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.Title = "Bannerlord Dedicated Client";
            Console.WriteLine("Starting Bannerlord");
            Console.Write(".");
            Thread.Sleep(500);
            Console.Write(".");
            Thread.Sleep(500);
            Console.Write(".");
            Thread.Sleep(500);
            Console.Write(".");
            Thread.Sleep(500);
            Console.Write(".");
            Thread.Sleep(2000);
            ProcessStartInfo startInfo = new System.Diagnostics.ProcessStartInfo();
            startInfo.FileName = @"Bannerlord.exe";
            startInfo.Arguments = "/singleplayer _MODULES_*MentalrobClient*Native*SandBoxCore*CustomBattle*SandBox*StoryMode*_MODULES_";
            Process.Start(startInfo);
        }
    }
}
