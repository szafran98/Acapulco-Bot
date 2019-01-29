using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

using Acapulco_Bot.Bot;

namespace Acapulco_Bot.Forms
{
    public partial class LogsForm : Form
    {
        public LogsForm()
        {
            InitializeComponent();
        }

        private Color _getColour(int type)
        {
            switch (type)
            {
                case 1: return Color.Green;
                case 2: return Color.Red;
                case 3: return Color.Blue;
                case 4: return Color.Black;
                case 5: return Color.Orange;
                default: return Color.Black;
            }
        }

        private void LogsForm_Load(object sender, EventArgs e)
        {
            foreach (Log log in AcapulcoBot.GetInstance.GetLogger().GetLogs())
            {
                richTextBox1.SuspendLayout();
                richTextBox1.SelectionColor = _getColour(log.type);
                richTextBox1.AppendText($"{log.content}\n");
                richTextBox1.ScrollToCaret();
                richTextBox1.ResumeLayout();
            }
        }

        private void richTextBox1_TextChanged(object sender, EventArgs e)
        {
            
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
           
        }
    }
}
