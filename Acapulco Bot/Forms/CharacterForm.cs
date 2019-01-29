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

using Newtonsoft.Json;

namespace Acapulco_Bot.Forms
{
    public partial class CharacterForm : Form
    {
        private List<string> _serverList = new List<string>
        {
            "Aldous",
            "Berufs",
            "Brutal",
            "Classic",
            "Gefion",
            "Hutena",
            "Jaruna",
            "Katahha",
            "Lelwani",
            "Majuna",
            "Nomada",
            "Perkun",
            "Tarhuna",
            "Telawel",
            "Tempest",
            "Zemyna",
            "Zorza",
            "Aequus",
            "Albatros",
            "Astraja",
            "Ataentsic",
            "Avalon",
            "Badzior",
            "Concordia",
            "Dionizos",
            "Dream",
            "Eden",
            "Erebos",
            "Ertill",
            "Experimental",
            "Febris",
            "Helios",
            "Inferno",
            "Infinity",
            "Invisible",
            "Legion",
            "Majorka",
            "Mordor",
            "Narwhals",
            "Nerthus",
            "Nexos",
            "Odysea",
            "Orchidea",
            "Orvidia",
            "Pandora",
            "Prosperity",
            "Speranza",
            "Stark",
            "Stoners",
            "Syberia",
            "Thantos",
            "Unia",
            "Virtus",
            "Zefira"
        };

        private string _character;
        private string _server;
        private string _init;
        private string _id;

        public CharacterForm()
        {
            InitializeComponent();
        }

        private void CharacterForm_Load(object sender, EventArgs e)
        {
            foreach (Character info in AcapulcoBot.GetInstance.GetCharacters())
            {
                comboBox1.Items.Add($"{info.name}");
            }

            foreach (string server in _serverList)
            {
                comboBox2.Items.Add($"{server}");
            }
        }

        private async void button1_Click(object sender, EventArgs e)
        {
            _character = comboBox1.Text;
            _server = comboBox2.Text;

            foreach (Character character in AcapulcoBot.GetInstance.GetCharacters())
            {
                if (character.name == _character)
                {
                    _id = character.id;
                }
            }

            _init = await AcapulcoBot.GetInstance.GetEngine().Initialize(_server, _id, 1);
            AcapulcoBot.GetInstance.GetPlayer().InitializeStatistics(_init);

            _init = await AcapulcoBot.GetInstance.GetEngine().Initialize(_server, _id, 2, true);
            AcapulcoBot.GetInstance.GetPlayer().InitializeMaps(_init);

            _init = await AcapulcoBot.GetInstance.GetEngine().Initialize(_server, _id, 3, true);
            AcapulcoBot.GetInstance.GetEngine().InitializeItems(_init);

            _init = await AcapulcoBot.GetInstance.GetEngine().Initialize(_server, _id, 4, true);

            MainForm mainForm = new MainForm();
            mainForm.Show();

            Hide();
        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
          
        }

        private void comboBox2_SelectedIndexChanged(object sender, EventArgs e)
        {

        }
    }
}
