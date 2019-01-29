using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

using Acapulco_Bot.Forms;
using Acapulco_Bot.Bot;
using Acapulco_Bot.Utils.Hash;

namespace Acapulco_Bot
{
    public partial class LoginForm : Form
    {
        public LoginForm()
        {
            InitializeComponent();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private async void button1_ClickAsync(object sender, EventArgs e)
        {
            bool state = await AcapulcoBot.GetInstance.RequestLogin(textBox1.Text, textBox2.Text);
          
            if (state)
            {
                AcapulcoBot.GetInstance.FindCharacters();

                CharacterForm characterForm = new CharacterForm();
                characterForm.Show();

                Hide();
            }
            else
            {
                MessageBox.Show("Zły login lub hasło.", "Acapulco Bot", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void LoginForm_Load(object sender, EventArgs e)
        {

        }
    }
}
