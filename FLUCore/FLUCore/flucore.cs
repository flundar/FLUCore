using System;
using System.Windows.Forms;
using System.Runtime.InteropServices;
using System.IO;
using System.Diagnostics;
using System.Threading;
using System.Net;
using JNogueira.Discord.Webhook.Client;
using Newtonsoft.Json;

namespace FLUCore
{
    public partial class FLUCore : Form
    {
        public string localcontrolversion = "2.5.2";
        public string controlversion;
        public string ogrencininismi;
        public string oyunaktif;
        public string uygulamaaktif;
        public string pckapat;
        public string pckitle;
        public string flundar;
        public string url = "https://discordapp.com/api/webhooks/895265805514723359/T2RSJHTSLrcK20zFwzUHejk-ckdj6weFpDziNthU_cRqluhYEXCuhyzSEaDVkuOj6S4l";
        public FLUCore()
        {
            InitializeComponent();
            label2.Text = localcontrolversion;
           // update();
        }


        public void update()
        {
            WebClient wc = new WebClient();
            wc.CachePolicy = new System.Net.Cache.RequestCachePolicy(System.Net.Cache.RequestCacheLevel.NoCacheNoStore);
            TimeSpan t = DateTime.UtcNow - new DateTime(1970, 1, 1);
            int secondsSinceEpoch = (int)t.TotalSeconds;
            try
            {
                controlversion = wc.DownloadString("http://10.10.70.129/controlversion");
            }
            catch
            {
                Process.Start("shutdown", "/s /t 0");
            }

            if (localcontrolversion != controlversion)
            {
                using (var client = new WebClient())
                {
                    try
                    {
                        client.DownloadFile("http://10.10.70.129/update/control.exe", @"C:\Windows\control.exe");
                        Thread.Sleep(1000);
                        Process.Start(@"C:\Windows\control.exe");
                        Close();
                    }
                    catch (Exception e)
                    {
                        Console.WriteLine(e);
                        Console.ReadKey();
                    }
                }
                Unprotect();
                this.Close();
            }

        }
        public void sistemler()
        {
            WebClient wc = new WebClient();
            wc.CachePolicy = new System.Net.Cache.RequestCachePolicy(System.Net.Cache.RequestCacheLevel.NoCacheNoStore);
            TimeSpan t = DateTime.UtcNow - new DateTime(1970, 1, 1);
            int secondsSinceEpoch = (int)t.TotalSeconds;
            try
            {
                oyunaktif = wc.DownloadString("http://10.10.70.129/oyun");
                uygulamaaktif = wc.DownloadString("http://10.10.70.129/uygulama");
                pckapat = wc.DownloadString("http://10.10.70.129/pckapat");
                pckitle = wc.DownloadString("http://10.10.70.129/pckitle");
            }
            catch
            {
                if (flundar == "true"){return;}
                this.Show();
                timer1.Start();
                discordhookAsync("Ana bilgisayar kapalı olduğu için sistemler çalışmıyor öğretmenlere ulaş.","flucore", Environment.MachineName.ToString());
                Process.Start("shutdown", "/s /t 0");
                wc.Dispose();
                throw;
            }
        }

        public static void sendDiscordWebhook(string URL, string escapedjson)
        {
            var wr = WebRequest.Create(URL);
            wr.ContentType = "application/json";
            wr.Method = "POST";
            using (var sw = new StreamWriter(wr.GetRequestStream()))
            sw.Write(escapedjson);
            wr.GetResponse();
        }

        [DllImport("ntdll.dll", SetLastError = true)]
        private static extern void RtlSetProcessIsCritical(UInt32 v1, UInt32 v2, UInt32 v3);

        private static volatile bool s_isProtected = false;

        private static ReaderWriterLockSlim s_isProtectedLock = new ReaderWriterLockSlim();

        public static bool IsProtected
        {
            get
            {
                try
                {
                    s_isProtectedLock.EnterReadLock();

                    return s_isProtected;
                }
                finally
                {
                    s_isProtectedLock.ExitReadLock();
                }
            }
        }

        /// <summary>
        /// If not alreay protected, will make the host process a system-critical process so it
        /// cannot be terminated without causing a shutdown of the entire system.
        /// </summary>
        public static void Protect()
        {
            try
            {
                s_isProtectedLock.EnterWriteLock();

                if (!s_isProtected)
                {
                    System.Diagnostics.Process.EnterDebugMode();
                    RtlSetProcessIsCritical(1, 0, 0);
                    s_isProtected = true;
                }
            }
            finally
            {
                s_isProtectedLock.ExitWriteLock();
            }
        }

        /// <summary>
        /// If already protected, will remove protection from the host process, so that it will no
        /// longer be a system-critical process and thus will be able to shut down safely.
        /// </summary>
        public static void Unprotect()
        {
            try
            {
                s_isProtectedLock.EnterWriteLock();

                if (s_isProtected)
                {
                    RtlSetProcessIsCritical(0, 0, 0);
                    s_isProtected = false;
                }
            }
            finally
            {
                s_isProtectedLock.ExitWriteLock();
            }
        }

        public async void  discordhookAsync(string ogrenciadi, string uygulama, string bilgisayarid)
        {
            var client = new DiscordWebhookClient("https://discordapp.com/api/webhooks/895265805514723359/T2RSJHTSLrcK20zFwzUHejk-ckdj6weFpDziNthU_cRqluhYEXCuhyzSEaDVkuOj6S4l");

            // Create your DiscordMessage with all parameters of your message.
            var message = new DiscordMessage(
                "@everyone" + DiscordEmoji.Grinning,
                username: "ÖĞRENCİ YAKALANDI",
                avatarUrl: "https://avatars.githubusercontent.com/u/39468090?v=4",
                tts: false,
                embeds: new[]
                {
        new DiscordMessageEmbed(
            "Öğrenci bilgileri aşağıdadır." + DiscordEmoji.Thumbsup,
            color: 0,
            author: new DiscordMessageEmbedAuthor("Bilgiler alındı."),
            url: "https://flundar.com",
            description: "Bu bilgiler ile öğrenciyi yakalamak mümkün.",
            fields: new[]
            {
                new DiscordMessageEmbedField("Öğrenci Adı:", ogrenciadi),
                new DiscordMessageEmbedField("Uygulama:", uygulama),
                new DiscordMessageEmbedField("Bilgisayar ID:", bilgisayarid)
            },
            thumbnail: new DiscordMessageEmbedThumbnail("https://avatars.githubusercontent.com/u/39468090?v=4"),
            image: new DiscordMessageEmbedImage("https://thumbs.dreamstime.com/b/restricted-text-written-red-vintage-round-stamp-rubber-214095813.jpg"),
            footer: new DiscordMessageEmbedFooter("made with ♥ by flundar", "https://avatars.githubusercontent.com/u/39468090?v=4")
               )
                }
            );

            // Send the message!
            await client.SendToDiscord(message);
        }

        private void FLUCore_Load(object sender, EventArgs e)
        {
            Protect();
            timer1.Start();
            koruma.Start();
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
           this.TopMost = true;
        }

        private void koruma_Tick(object sender, EventArgs e)
        {
            sistemler();

            if (pckapat == "true")
            {
                Process.Start("shutdown", "/s /t 0");
            }

            if (oyunaktif == "true")
            {
                var json = new WebClient().DownloadString("http://10.10.70.129/oyunveri.json");
                dynamic dynJson = JsonConvert.DeserializeObject(json);
                foreach (Process process2 in Process.GetProcesses())
                {
                    foreach (var item in dynJson)
                    {
                        if (process2.MainWindowTitle.ToLower().Contains(item.oyun.ToString()))
                        {
                            if (String.IsNullOrEmpty(ogrencininismi))
                            {
                                discordhookAsync("Giris yapmamis ogrenci! Acilen ilgilenilmesi gerek (Windows TAB).", process2.MainWindowTitle, Environment.MachineName.ToString());
                            }
                            else
                            {
                                discordhookAsync(ogrencininismi, process2.MainWindowTitle, Environment.MachineName.ToString());
                            }

                            try
                            {
                                process2.Kill();
                            }
                            catch (Exception ex)
                            {
                                MessageBox.Show(ex.ToString());
                            }
                        }
                    }
                }
            }
            if (pckitle == "true")
            {
                Process.Start(@"C:\WINDOWS\system32\rundll32.exe", "user32.dll,LockWorkStation");
            }


            if (uygulamaaktif == "true")
            {
                var json = new WebClient().DownloadString("http://10.10.70.129/uygulamaveri.json");
                dynamic dynJson = JsonConvert.DeserializeObject(json);
                foreach (Process process2 in Process.GetProcesses())
                {
                    foreach (var item in dynJson)
                    {
                        if (process2.MainWindowTitle.ToLower().Contains(item.uygulama.ToString()))
                        {
                            if (String.IsNullOrEmpty(ogrencininismi))
                            {
                                discordhookAsync("Giris yapmamis ogrenci! Acilen ilgilenilmesi gerek (Windows TAB).", process2.MainWindowTitle, Environment.MachineName.ToString());
                            }
                            else
                            {
                                discordhookAsync(ogrencininismi, process2.MainWindowTitle, Environment.MachineName.ToString());
                            }

                            try
                            {
                                process2.Kill();
                            }
                            catch (Exception ex)
                            {
                                MessageBox.Show(ex.ToString());
                            }
                        }
                    }
                }
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (String.IsNullOrEmpty(textBox1.Text))
            {
                MessageBox.Show("Alanları doldurmak zorundasınız!");
                return;
            }
            koruma.Start();
            timer1.Stop();
            this.Hide();
            ogrencininismi = textBox1.Text;
        }

        private void FLUCore_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (e.CloseReason == CloseReason.WindowsShutDown)
            {
                Unprotect();
            }
        }

        private void textBox1_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Home && ModifierKeys == Keys.Control)
            {
                Unprotect();
                MessageBox.Show("Welcome flundar you have logged in admin.");
                this.Close();
            }
        }


    }
}
