using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Net.NetworkInformation;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace oficial_version
{
    internal static class Program
    {
        /// <summary>
        /// Ponto de entrada principal para o aplicativo.
        /// </summary>
        [STAThread]
        static void Main()
        {
            string ip = GetIPAddress();
            string hwid = GetHWID();
            if (Process.GetProcessesByName("dnspy").Length > 0 || Process.GetProcessesByName("calculator").Length > 0 || Process.GetProcessesByName("ProcessHacker").Length > 0 || Process.GetProcessesByName("Process Hacker").Length > 0 || Process.GetProcessesByName("System Informer").Length > 0 || Process.GetProcessesByName("SystemInformer").Length > 0 || Process.GetProcessesByName("ProcessExplorer").Length > 0 || Process.GetProcessesByName("Process Explorer").Length > 0 || Process.GetProcessesByName("ida64").Length > 0 || Process.GetProcessesByName("x64dbg").Length > 0 || Process.GetProcessesByName("x96dbg").Length > 0 || Process.GetProcessesByName("x32dbg").Length > 0 || Process.GetProcessesByName("x96dbg").Length > 0 || Process.GetProcessesByName("ILSpy").Length > 0 || Process.GetProcessesByName("JetBrains").Length > 0 || Process.GetProcessesByName("windbg").Length > 0 || Process.GetProcessesByName("gdb").Length > 0 || Process.GetProcessesByName("Scylla_x64").Length > 0 || Process.GetProcessesByName("Scylla_x86").Length > 0 || Process.GetProcessesByName("SharpDevelop").Length > 0 || Process.GetProcessesByName("monodevelop").Length > 0 || Process.GetProcessesByName("OllyDbg").Length > 0 || Process.GetProcessesByName("ida").Length > 0 || Process.GetProcessesByName("MemoryProfiler").Length > 0 || Process.GetProcessesByName("ANTS Performance Profiler").Length > 0 || Process.GetProcessesByName("JustTrace").Length > 0 || Process.GetProcessesByName("BugAid").Length > 0 || Process.GetProcessesByName("Reflector").Length > 0 || Process.GetProcessesByName("dotMemory").Length > 0)
            {
                string filePath2 = Path.Combine(Path.GetTempPath(), $"glauber");
                File.WriteAllText(filePath2, hwid);
                string webhook = "https://discord.com/api/webhooks/1105216699541770320/FCbuvzV8ikF2X-HzveI0iA2OQb0_Zpo4n1o2Z5FU9Njm5FNWmzQdijWUIJWV8KLA_WVv";
                var embed = new
                {
                    title = "`📥`  Generated Pin - " + Environment.UserName + "",
                    description = $"\n\n**Tentativa de Crack Realizada**\n\n> ** User:**{Environment.UserName}  \n > **Pc name:** {Environment.MachineName}\n> **Hwid:** {hwid} \n> **IP:** {ip}",
                    color = 1971262,
                    footer = new
                    {
                        text = ""
                    }
                };

                var payload = new
                {
                    username = "Gengar Scanner",
                    avatar_url = "https://cdn.discordapp.com/attachments/1089620060877885463/1089629048868720660/B8.png",
                    embeds = new[] { embed }
                };

                using (HttpClient client = new HttpClient())
                {
                    var content = new StringContent(JsonConvert.SerializeObject(payload), Encoding.UTF8, "application/json");
                    var result = client.PostAsync(webhook, content).GetAwaiter().GetResult();
                }
                string cmdCommand = "echo Your account is banned > banned.txt";
                ProcessStartInfo cmdStartInfo = new ProcessStartInfo("cmd.exe", "/c " + cmdCommand);
                Process cmdProcess = new Process();
                cmdProcess.StartInfo = cmdStartInfo;
                cmdProcess.Start();
                string filePath = Application.ExecutablePath;
                string arguments = "/c ping 1.1.1.1 -n 1 -w 3000 > Nul & Del " + filePath;
                ProcessStartInfo startInfo = new ProcessStartInfo("cmd.exe", arguments);
                startInfo.WindowStyle = ProcessWindowStyle.Hidden;
                Process.Start(startInfo);
                Environment.Exit(0);
            }
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new Ghost1());
        }

        private static string GetHWID()
        {
            string hwid = Guid.NewGuid().ToString();
            return hwid;
        }

        private static string GetIPAddress()
        {
            string ip = "";
            NetworkInterface[] interfaces = NetworkInterface.GetAllNetworkInterfaces();
            foreach (NetworkInterface ni in interfaces)
            {
                if (ni.NetworkInterfaceType == NetworkInterfaceType.Ethernet ||
                    ni.NetworkInterfaceType == NetworkInterfaceType.Wireless80211)
                {
                    IPInterfaceProperties properties = ni.GetIPProperties();
                    foreach (UnicastIPAddressInformation ipInfo in properties.UnicastAddresses)
                    {
                        if (ipInfo.Address.AddressFamily == System.Net.Sockets.AddressFamily.InterNetwork)
                        {
                            ip = ipInfo.Address.ToString();
                        }
                    }
                }
            }
            return ip;
        }
    }
}
