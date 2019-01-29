using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

using Acapulco_Bot.Bot;
using Acapulco_Bot.Game;

namespace Acapulco_Bot.Forms
{
    public partial class InventoryForm : Form
    {
        public InventoryForm()
        {
            InitializeComponent();
        }

        private void InventoryForm_Load(object sender, EventArgs e)
        {
            foreach (Values item in AcapulcoBot.GetInstance.GetEngine().GetItems().item.Values)
            {
                dataGridView1.Rows.Add(new string[] { item.name });
            }
        }
    }
}
