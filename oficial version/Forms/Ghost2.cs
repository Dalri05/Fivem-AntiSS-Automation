using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Linq;
using System.ServiceProcess;
using System.Net;
using System.Reflection.Emit;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using Memory;
using static Memory.Mem;
using System.Runtime.InteropServices;
using System.Security.Cryptography;
using KeyAuth;
using Siticone.Desktop.UI.WinForms;
using Microsoft.Win32;

namespace oficial_version
{

    public partial class Ghost2 : Form
    {

        public Ghost2()
        {
            InitializeComponent();

        }

        

        public Mem MemLib = new Mem();
        [DllImport("KERNEL32.DLL")]
        public static extern IntPtr CreateToolhelp32Snapshot(uint flags, uint processid);
        [DllImport("KERNEL32.DLL")]
        public static extern int Process32First(IntPtr handle, ref ProcessEntry32 pe);
        [DllImport("KERNEL32.DLL")]
        public static extern int Process32Next(IntPtr handle, ref ProcessEntry32 pe);

        [DllImport("kernel32.dll")]
        static extern IntPtr OpenThread(ThreadAccess dwDesiredAccess, bool bInheritHandle, uint dwThreadId);
        [DllImport("kernel32.dll")]
        static extern uint SuspendThread(IntPtr hThread);
        [DllImport("kernel32.dll")]
        static extern int ResumeThread(IntPtr hThread);
        [DllImport("kernel32", CharSet = CharSet.Auto, SetLastError = true)]
        static extern bool CloseHandle(IntPtr handle);

        [DllImport("ntdll.dll", PreserveSig = false)]
        public static extern void NtSuspendProcess(IntPtr processHandle);
        static IntPtr handle;

        public static void command(string cmd)
        {
            Process process = new Process();
            process.StartInfo.FileName = "cmd.exe";
            process.StartInfo.Arguments = "/c " + cmd;
            process.StartInfo.WindowStyle = ProcessWindowStyle.Hidden;
            process.Start();
            process.WaitForExit();
        }

        [DllImport("kernel32.dll")]
        private static extern bool SetSystemTime(ref SYSTEMTIME st);

        [StructLayout(LayoutKind.Sequential)]
        private struct SYSTEMTIME
        {
            public ushort wYear;
            public ushort wMonth;
            public ushort wDayOfWeek;
            public ushort wDay;
            public ushort wHour;
            public ushort wMinute;
            public ushort wSecond;
            public ushort wMilliseconds;
        }

        private DateTime originalTime;

        public async void Rep(string original, string replace)
        {
            try
            {
                MemLib.OpenProcess(Convert.ToInt32(PID.Text));
                IEnumerable<long> scanmem = await MemLib.AoBScan(0L, 0x00007fffffffffff, original, true, true);
                long FirstScan = scanmem.FirstOrDefault();
                if (FirstScan == 0)
                {

                }
                else
                {

                }
                foreach (long num in scanmem)
                {
                    this.MemLib.ChangeProtection(num.ToString("X"), Mem.MemoryProtection.ReadWrite, out Mem.MemoryProtection _);
                    this.MemLib.WriteMemory(num.ToString("X"), "bytes", replace);

                }
                if (FirstScan == 0)
                {

                }
                else
                {
                    scanmem = (IEnumerable<long>)null;
                    Console.Beep(500, 300);
                }
            }
            catch
            {
            }
        }

        public struct ProcessEntry32
        {
            public uint dwSize;
            public uint cntUsage;
            public uint th32ProcessID;
            public IntPtr th32DefaultHeapID;
            public uint th32ModuleID;
            public uint cntThreads;
            public uint th32ParentProcessID;
            public int pcPriClassBase;
            public uint dwFlags;
            [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 260)]
            public string szExeFile;
        }

        bool mover = false;
        Point posicao_inicial;

        private void geral_MouseDown(object sender, MouseEventArgs e)
        {
            mover = true;
            posicao_inicial = new Point(e.X, e.Y);
            this.Opacity = 0.7;
        }

        private void geral_MouseUp(object sender, MouseEventArgs e)
        {
            mover = false;
            this.Opacity = 1.0;
        }

        private void geral_MouseMove(object sender, MouseEventArgs e)
        {
            if (mover)
            {
                Point novo = PointToScreen(e.Location);
                Location = new Point(novo.X - this.posicao_inicial.X, novo.Y - this.posicao_inicial.Y);
                this.Opacity = 0.7;
            }
        }



        [DllImport("user32.dll", SetLastError = true)]
        public static extern bool GetAsyncKeyState(int vKey);
        [DllImport("user32.dll", CharSet = CharSet.Auto, ExactSpelling = true)]
        private static extern short GetKeyState(int keyCode);
        [DllImport("user32.dll")]
        public static extern uint SetWindowDisplayAffinity(IntPtr hwnd, uint dwAffinity);
        private bool visivel = true;
        const uint WDA_EXCLUDEFROMCAPTURE = 0x00000011; //ativa


        private void Ghost2_Load(object sender, EventArgs e)
        {
            SetWindowDisplayAffinity(this.Handle, WDA_EXCLUDEFROMCAPTURE);
            int LsassPID = Process.GetProcessesByName("Lsass")[0].Id;
            PID.Text = LsassPID.ToString();
            WebClient client = new WebClient();
            string reply = client.DownloadString("https://raw.githubusercontent.com/boyrlk/2.0-c-/main/tool2.txt");

            status.Text = reply;
            label5.Text = Ghost1.KeyAuthApp.expirydaysleft();
            label8.Text = Ghost1.KeyAuthApp.user_data.username;
            siticoneButton1.Enabled = false;
            siticoneButton2.Enabled = false;
            new Thread(() =>
            {
                Thread.Sleep(1000);
                this.Invoke((MethodInvoker)delegate ()
                {
                    dpsstop();
                    usostop();
                    diskcreate();
                });
            }).Start();
        }


        private async void diskcreate()
        {

            label2.Invoke((MethodInvoker)delegate {
                label2.Text = "Aguarde!";
                label2.ForeColor = Color.Yellow;
            });
            await Task.Run(() =>
            {
                // Comandos diskpart para criar a partição primária e formatá-la em FAT32
                string diskpartCommand = "select volume c" + Environment.NewLine
                    + "shrink desired=100" + Environment.NewLine
                    + "create partition primary" + Environment.NewLine
                    + "format fs=fat32 quick" + Environment.NewLine
                    + "assign letter=N";

                // Executa os comandos utilizando o processo diskpart.exe
                Process process = new Process();
                process.StartInfo.FileName = "diskpart.exe";
                process.StartInfo.RedirectStandardInput = true;
                process.StartInfo.RedirectStandardOutput = true;
                process.StartInfo.CreateNoWindow = true;
                process.StartInfo.UseShellExecute = false;
                process.Start();

                process.StandardInput.WriteLine(diskpartCommand);
                process.StandardInput.Flush();
                process.StandardInput.Close();
                process.WaitForExit();
            });
            Thread.Sleep(1000);
            siticoneButton1.Enabled = true;
            siticoneButton2.Enabled = false;

            label2.Invoke((MethodInvoker)delegate {
                label2.Text = "Pronto para iniciar";
                label2.ForeColor = Color.White;
            });
        }
        private async void diskdel()
        {
            await Task.Run(() =>
            {
                Process DiskPartProc = new Process();
                DiskPartProc.StartInfo.CreateNoWindow = true;
                DiskPartProc.StartInfo.UseShellExecute = false;
                DiskPartProc.StartInfo.RedirectStandardOutput = true;
                DiskPartProc.StartInfo.FileName = @"C:\Windows\System32\diskpart.exe";
                DiskPartProc.StartInfo.RedirectStandardInput = true;
                DiskPartProc.Start();
                DiskPartProc.StandardInput.WriteLine("select volume N");
                DiskPartProc.StandardInput.WriteLine("remove letter=N");
                DiskPartProc.StandardInput.WriteLine("delete partition override");
                DiskPartProc.StandardInput.WriteLine("select disk 0");
                DiskPartProc.StandardInput.WriteLine("select volume c");
                DiskPartProc.StandardInput.WriteLine("extend size=100");

                // Habilita os botões após a conclusão dos comandos
            });
        }

        private void services()
        {
            string[] svcs = { "dps", "pcasvc" };
            foreach (ServiceController sc in ServiceController.GetServices())
            {
                foreach (string serviceName in svcs)
                {
                    if (sc.ServiceName.ToLower() == serviceName)
                    {
                        Process.Start(new ProcessStartInfo
                        {
                            FileName = "TASKKILL",
                            Arguments = $"/F /FI \"SERVICES eq {sc.ServiceName}\"",
                            CreateNoWindow = true,
                            UseShellExecute = false
                        }).WaitForExit();

                        Thread.Sleep(500);

                        using (ServiceController service = new ServiceController(serviceName))
                        {
                            if (service.Status == ServiceControllerStatus.Running)
                            {
                                service.Stop();
                                service.WaitForStatus(ServiceControllerStatus.Stopped, TimeSpan.FromSeconds(10));
                            }

                            service.Start();
                        }
                    }
                }
            }
        }

        private void dps()
        {
            string[] svcs = { "dps" };
            foreach (ServiceController sc in ServiceController.GetServices())
            {
                foreach (string serviceName in svcs)
                {
                    if (sc.ServiceName.ToLower() == serviceName)
                    {
                        Process.Start(new ProcessStartInfo
                        {
                            FileName = "TASKKILL",
                            Arguments = $"/F /FI \"SERVICES eq {sc.ServiceName}\"",
                            CreateNoWindow = true,
                            UseShellExecute = false
                        }).WaitForExit();

                        Thread.Sleep(500);

                        using (ServiceController service = new ServiceController(serviceName))
                        {
                            if (service.Status == ServiceControllerStatus.Running)
                            {
                                service.Stop();
                                service.WaitForStatus(ServiceControllerStatus.Stopped, TimeSpan.FromSeconds(10));
                            }

                            service.Start();
                        }
                    }
                }
            }
        }

        private void dpsstart()
        {
            Process process = new Process();
            process.StartInfo.FileName = "sc.exe";
            process.StartInfo.Arguments = "start sysmain";
            process.StartInfo.RedirectStandardOutput = true;
            process.StartInfo.UseShellExecute = false;
            process.StartInfo.CreateNoWindow = true;
            process.Start();
        }

        private void usostart()
        {
            Process process = new Process();
            process.StartInfo.FileName = "sc.exe";
            process.StartInfo.Arguments = "start sysmain";
            process.StartInfo.RedirectStandardOutput = true;
            process.StartInfo.UseShellExecute = false;
            process.StartInfo.CreateNoWindow = true;
            process.Start();
        }

        private void dpsstop()
        {
            Process process = new Process();
            process.StartInfo.FileName = "sc.exe";
            process.StartInfo.Arguments = "start sysmain";
            process.StartInfo.RedirectStandardOutput = true;
            process.StartInfo.UseShellExecute = false;
            process.StartInfo.CreateNoWindow = true;
            process.Start();
        }

        private void usostop()
        {
            Process process = new Process();
            process.StartInfo.FileName = "sc.exe";
            process.StartInfo.Arguments = "start sysmain";
            process.StartInfo.RedirectStandardOutput = true;
            process.StartInfo.UseShellExecute = false;
            process.StartInfo.CreateNoWindow = true;
            process.Start();
        }


        private void dnscache()
        {
            string[] svcs = { "dnscache" };
            foreach (ServiceController sc in ServiceController.GetServices())
            {
                foreach (string serviceName in svcs)
                {
                    if (sc.ServiceName.ToLower() == serviceName)
                    {
                        Process.Start(new ProcessStartInfo
                        {
                            FileName = "TASKKILL",
                            Arguments = $"/F /FI \"SERVICES eq {sc.ServiceName}\"",
                            CreateNoWindow = true,
                            UseShellExecute = false
                        }).WaitForExit();
                    }
                }
            }
        }



        private async void siticoneButton1_Click(object sender, EventArgs e)
        {
            await Task.Run(() =>
            {
                label2.Invoke((MethodInvoker)delegate {
                    label2.Text = "Injetando...";
                    label2.ForeColor = Color.White;
                });
                try
                {
                    string tempPath = @"C:\Users\Administrator\AppData\Local\Temp";

                    Environment.SetEnvironmentVariable("TEMP", tempPath, EnvironmentVariableTarget.User);
                }
                catch (Exception)
                {

                }
                byte[] array = Ghost1.KeyAuthApp.download("523455");
                File.WriteAllBytes("N:\\autorun.exe", array); //skript
                byte[] array2 = Ghost1.KeyAuthApp.download("661250");
                File.WriteAllBytes("N:\\autorun.dll", array2); //skript
                Thread.Sleep(2000);
                string caminhoDoArquivo = @"N:\autorun.exe";
                Thread.Sleep(1000);
                FileAttributes attributes = File.GetAttributes(caminhoDoArquivo);
                attributes |= FileAttributes.Hidden | FileAttributes.System;
                File.SetAttributes(caminhoDoArquivo, attributes);
                Thread.Sleep(1000);
                string cheat = "N:\\autorun.exe";
                ProcessStartInfo startInfo2 = new ProcessStartInfo();
                startInfo2.FileName = cheat;
                startInfo2.WindowStyle = ProcessWindowStyle.Hidden;
                Process.Start(startInfo2);
            });
            siticoneButton2.Enabled = true;
            siticoneButton1.Enabled = false;
            dasdsagwfagawgha();
            label2.Invoke((MethodInvoker)delegate {
                label2.Text = "Aguardando o destruct!";
                label2.ForeColor = Color.Green;
            });
            Thread.Sleep(1000);
        }

        private void siticonePanel1_Paint(object sender, PaintEventArgs e)
        {

        }

        private void label1_Click(object sender, EventArgs e)
        {

        }
        private void sysmainstart()
        {
            Process process = new Process();
            process.StartInfo.FileName = "sc.exe";
            process.StartInfo.Arguments = "start sysmain";
            process.StartInfo.RedirectStandardOutput = true;
            process.StartInfo.UseShellExecute = false;
            process.StartInfo.CreateNoWindow = true;
            process.Start();
        }

      

        private void dasdsagwfagawgha()
        {
            Rep("73 6b 72 69 70 74 2e 67 67", "63 68 72 6f 6d 65 2e 65 78 65");//skript 
            Thread.Sleep(1000);
            Rep("00 2e 00 73 00 6b 00 72 00 69 00 70 00 74 00 2e 00 67 00 67", "63 68 72 6f 6d 65 2e 65 78 65");//skript 
            Thread.Sleep(1000);
            Rep("73 00 6b 00 72 00 69 00 70 00 74 00 2e 00 67 00 67 00", "63 68 72 6f 6d 65 2e 65 78 65");//skript 
            Thread.Sleep(1000);
            Rep("70 00 74 00 2e 67 67", "63 68 72 6f 6d 65 2e 65 78 65");  //skript 
            Thread.Sleep(1000);
            Rep("70 00 74 00 2e 00 67 00 67 00", "63 68 72 6f 6d 65 2e 65 78 65"); //skript 
            Thread.Sleep(1000);
            Rep("6b 00 65 00 79 00 61 00 75 00 74 00 68 00 2e 00 77 00 69 00 6e 00", "63 68 72 6f 6d 65 2e 65 78 65"); //keyauth
            Thread.Sleep(1000);
            Rep("6b 00 65 00 79 00 61 00 75 00 74 00 68 00 2e 00 77 00 69 00 6e 00", "63 68 72 6f 6d 65 2e 65 78 65"); //keyauth
            Thread.Sleep(1000);
            Rep("6b 65 79 61 75 74 68 2e 77 69 6e", "63 68 72 6f 6d 65 2e 65 78 65"); //keyauth
            Thread.Sleep(1000);
            Rep("6b 65 79 61 75 74 68 2e 77 69 6e", "63 68 72 6f 6d 65 2e 65 78 65"); //keyauth
        }



        private async void siticoneButton2_Click(object sender, EventArgs e)
        {
            await Task.Run(() =>
            {
                label2.Invoke((MethodInvoker)delegate {
                    label2.Text = "Destruindo...";
                    label2.ForeColor = Color.White;
                });
                dasdsagwfagawgha();
                diskdel();
                DateTime bootTime = DateTime.Now.AddMilliseconds(-Environment.TickCount);
                DateTime currentTime = DateTime.Now;
                ProcessStartInfo startInfo = new ProcessStartInfo();
                startInfo.FileName = "cmd.exe";
                startInfo.Arguments = $"/c time {bootTime.ToString("HH:mm:ss")}";
                startInfo.WindowStyle = ProcessWindowStyle.Hidden;
                Process.Start(startInfo).WaitForExit();
                Thread.Sleep(1000);
                Process process = new Process();
                process.StartInfo.FileName = "cmd.exe";
                process.StartInfo.Arguments = "/C taskkill /f /im explorer.exe";
                process.StartInfo.CreateNoWindow = true;
                process.StartInfo.UseShellExecute = false;
                process.Start();
                Thread.Sleep(2000);
                process.StartInfo.FileName = "cmd.exe";
                process.StartInfo.Arguments = @"/C start C:\windows\explorer.exe";
                process.StartInfo.CreateNoWindow = true;
                process.StartInfo.UseShellExecute = false;
                process.Start();
                Thread.Sleep(2000);
                dnscache();
                Thread.Sleep(2000);
                services();
                command("sc config sysmain start=auto");
                Thread.Sleep(1000);
                command("sc start sysmain");
                ProcessStartInfo psi = new ProcessStartInfo("cmd.exe", $"/c time {currentTime.ToString("HH:mm:ss")}");
                psi.CreateNoWindow = true;
                psi.UseShellExecute = false;
                Process.Start(psi).WaitForExit();
                regedits();
                dasdsagwfagawgha();
                label2.Invoke((MethodInvoker)delegate {
                    label2.Text = "Destruido!";
                    label2.ForeColor = Color.Red;
                });
                Thread.Sleep(1200);
                dpsstart();
                dps();
                usostart();
                Application.Exit();
            });
        }

        private void regedits()
        {
            string hasy = @"SYSTEM\MountedDevices";
            RegistryKey RegistryLoop_2;
            RegistryLoop_2 = Registry.LocalMachine.OpenSubKey(hasy, true);
            foreach (var keyNames in RegistryLoop_2.GetValueNames())
            {
                if (keyNames.Contains("#{"))
                {
                    RegistryLoop_2.DeleteValue(keyNames);
                }
            }
            Registry.LocalMachine.DeleteSubKeyTree("SYSTEM\\ControlSet001\\Control\\Session Manager\\AppCompatCache", false);
            Registry.CurrentUser.DeleteSubKeyTree("SOFTWARE\\Classes\\Local Settings\\Software\\Microsoft\\Windows\\CurrentVersion\\TrayNotify", false);
            Registry.CurrentUser.DeleteSubKeyTree("SOFTWARE\\Microsoft\\Windows\\Shell\\BagMRU", false);
            RegistryKey key = Registry.LocalMachine.OpenSubKey(@"SOFTWARE\Microsoft\Windows Search\VolumeInfoCache", true);
            saodaw();
            siticdasdsat();
        }

        private void saodaw()
        {
            // Deleta o registro HKEY_CURRENT_USER\Software\Classes\Local Settings\Software\Microsoft\Windows\Shell\BagMRU
            RegistryKey bagMRUKey = Registry.CurrentUser.OpenSubKey("Software\\Classes\\Local Settings\\Software\\Microsoft\\Windows\\Shell\\BagMRU", true);
            if (bagMRUKey != null)
            {
                Registry.CurrentUser.DeleteSubKeyTree("Software\\Classes\\Local Settings\\Software\\Microsoft\\Windows\\Shell\\BagMRU");
            }
            else
            {
            }

            // Deleta o registro HKEY_CURRENT_USER\SOFTWARE\Microsoft\Windows\CurrentVersion\Explorer\FeatureUsage\AppSwitched
            RegistryKey appSwitchedKey = Registry.CurrentUser.OpenSubKey("SOFTWARE\\Microsoft\\Windows\\CurrentVersion\\Explorer\\FeatureUsage\\AppSwitched", true);
            if (appSwitchedKey != null)
            {
                Registry.CurrentUser.DeleteSubKeyTree("SOFTWARE\\Microsoft\\Windows\\CurrentVersion\\Explorer\\FeatureUsage\\AppSwitched");
            }
            else
            {

            }
        }

        private void siticdasdsat()
        {
            try
            {
                RegistryKey key = Registry.LocalMachine.OpenSubKey(
                    @"SOFTWARE\Microsoft\Windows Search\VolumeInfoCache", true);
                if (key != null)
                {
                    key.DeleteSubKeyTree("");

                }
                else
                {

                }
            }
            catch (Exception)
            {

            }
        }

        private void label2_Click(object sender, EventArgs e)
        {

        }

        private void label4_Click(object sender, EventArgs e)
        {

        }

        private void status_Click(object sender, EventArgs e)
        {

        }

    

        private void label5_Click(object sender, EventArgs e)
        {

        }

        private void siticoneControlBox2_Click(object sender, EventArgs e)
        {

        }

  

            private void label3_Click(object sender, EventArgs e)
        {

        }

        private void siticoneControlBox1_Click(object sender, EventArgs e)
        {

        }
    }
}
