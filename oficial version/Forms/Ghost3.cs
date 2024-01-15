using KeyAuth;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace oficial_version
{
    public partial class Ghost3 : Form
    {
        public Ghost3()
        {
            InitializeComponent();
        }

        public static api KeyAuthApp = new api(
name: "Bypass tool",
ownerid: "oPy9xx1KTm",
secret: "0ba9db0592df607dbef77ae1742cdce2e277a989d9433b4362cf1cf13698a6b5",
version: "1.0"
);

        private void label2_Click(object sender, EventArgs e)
        {

        }

        private void siticoneButton1_Click(object sender, EventArgs e)
        {
            KeyAuthApp.register(username.Text, senha.Text, key.Text);
            if (KeyAuthApp.response.success)
            {
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

        [DllImport("user32.dll", SetLastError = true)]
        public static extern bool GetAsyncKeyState(int vKey);
        [DllImport("user32.dll", CharSet = CharSet.Auto, ExactSpelling = true)]
        private static extern short GetKeyState(int keyCode);
        [DllImport("user32.dll")]
        public static extern uint SetWindowDisplayAffinity(IntPtr hwnd, uint dwAffinity);
        private bool visivel = true;
        const uint WDA_EXCLUDEFROMCAPTURE = 0x00000011; //ativa

        private void Ghost3_Load(object sender, EventArgs e)
        {
            SetWindowDisplayAffinity(this.Handle, WDA_EXCLUDEFROMCAPTURE);
            KeyAuthApp.init();
        }
    }
}
