using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

using Acapulco_Bot.Game.Player;
using Acapulco_Bot.Utils;
using Acapulco_Bot.Bot;
using System.Diagnostics;

namespace Acapulco_Bot.Forms
{
    public partial class MainForm : Form
    {
        private int _stamina;
        private bool _attacking;

        private int _startExp;
        private int _startGold;

        Stopwatch _stopwatch = new Stopwatch();

        public MainForm()
        {
            InitializeComponent();
        }

        private void MainForm_Load(object sender, EventArgs e)
        {
            button1.Enabled = false;
            button2.Enabled = false;

            numericUpDown1.Maximum = 60000;
            numericUpDown1.Minimum = 5000;

            AcapulcoBot.GetInstance.GetLogger().Append("================================", 3);
            AcapulcoBot.GetInstance.GetLogger().Append("Acapulco Bot by Rivendell", 3);
            AcapulcoBot.GetInstance.GetLogger().Append("Version: 1.0", 3);
            AcapulcoBot.GetInstance.GetLogger().Append("================================\n", 3);

            timer1.Start();
            timer3.Start();
            timer4.Start();

            _stamina = int.Parse(AcapulcoBot.GetInstance.GetPlayer().GetStatistics().h.stamina);

            foreach (KeyValuePair<string, Default> map in AcapulcoBot.GetInstance.GetPlayer().GetMaps().mobile_maps)
            {
                comboBox1.Items.Add($"{map.Value.name}");
            }
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            if (_stamina >= 50)
                return;
            else
                _stamina++;
        }

        private async void timer2_Tick(object sender, EventArgs e)
        {
            if (_stamina > 0)
            {
                timer3.Stop();

                _attacking = true;

                await AcapulcoBot.GetInstance.GetPlayer().GetFight().Attack(AcapulcoBot.GetInstance.GetPlayer().GetMaps().mobile_maps.Keys.ElementAt(comboBox1.SelectedIndex));
                await Task.Delay(1000);
                await AcapulcoBot.GetInstance.GetPlayer().GetFight().AutoAttack();
                await Task.Delay(1000);
                await AcapulcoBot.GetInstance.GetPlayer().GetFight().Close();

                if (AcapulcoBot.GetInstance.GetDrops().item != null && AcapulcoBot.GetInstance.GetDrops().item.Keys.Count != 0)
                {
                    string lootID = AcapulcoBot.GetInstance.GetDrops().item.Keys.FirstOrDefault<string>();
                    await AcapulcoBot.GetInstance.GetPlayer().GetLoot().Take(lootID);
                }

                if (Settings.AutoSell && AcapulcoBot.GetInstance.GetDropsToSell().item != null && AcapulcoBot.GetInstance.GetDrops().item.Keys.Count != 0)
                {
                    foreach (KeyValuePair<string, Game.Values> item in AcapulcoBot.GetInstance.GetDropsToSell().item.ToList<KeyValuePair<string, Game.Values>>())
                    {
                        if (item.Value.loc.Contains("l") && !item.Value.stat.Contains("stamina"))
                        {
                            await AcapulcoBot.GetInstance.GetPlayer().GetShop().Sell(int.Parse(item.Key));
                            AcapulcoBot.GetInstance.GetDropsToSell().item.Remove(item.Key);
                        }
                        break;
                    }
                }

                _attacking = false;
                _stamina--;

                timer3.Start();
            }
        }

        private async void timer3_Tick(object sender, EventArgs e)
        {
            label2.Text = $"Stamina: {_stamina}/50";
            label3.Text = $"Nick: {AcapulcoBot.GetInstance.GetPlayer().GetStatistics().h.nick}";
            label4.Text = $"Profesja: {Classes.GetClass(AcapulcoBot.GetInstance.GetPlayer().GetStatistics().h.prof)}";
            label5.Text = $"Level: {AcapulcoBot.GetInstance.GetPlayer().GetStatistics().h.lvl}";
            label6.Text = $"Exp: {AcapulcoBot.GetInstance.GetPlayer().GetStatistics().h.exp}/{(10 + Math.Pow(int.Parse(AcapulcoBot.GetInstance.GetPlayer().GetStatistics().h.lvl) - 1, 4))} ({((int.Parse(AcapulcoBot.GetInstance.GetPlayer().GetStatistics().h.exp) - (10 + Math.Pow(int.Parse(AcapulcoBot.GetInstance.GetPlayer().GetStatistics().h.lvl) - 1, 4))) / Math.Abs((10 + Math.Pow(int.Parse(AcapulcoBot.GetInstance.GetPlayer().GetStatistics().h.lvl) - 1, 4)))) * 100}%)";
            label7.Text = $"Gold: {AcapulcoBot.GetInstance.GetPlayer().GetStatistics().h.gold}";
            label8.Text = $"Health: {AcapulcoBot.GetInstance.GetPlayer().GetStatistics().h.warrior_stats.hp}/{AcapulcoBot.GetInstance.GetPlayer().GetStatistics().h.warrior_stats.maxhp}";
            label13.Text = $"Wyczerpanie: {AcapulcoBot.GetInstance.GetPlayer().GetStatistics().h.ttl}";

            label14.Text = $"Zdobyto expa: {int.Parse(AcapulcoBot.GetInstance.GetPlayer().GetStatistics().h.exp) - _startExp}";
            label15.Text = $"Zdobyto golda: {int.Parse(AcapulcoBot.GetInstance.GetPlayer().GetStatistics().h.gold) - _startGold}";

            await AcapulcoBot.GetInstance.GetEngine().RefreshEvent();
        }

        private async void timer4_Tick(object sender, EventArgs e)
        {
            label12.Text = $"Czas pracy: {_stopwatch.Elapsed.Hours}:{_stopwatch.Elapsed.Minutes}:{_stopwatch.Elapsed.Seconds}";
            if (Settings.AutoHeal && !_attacking)
            {
                await AcapulcoBot.GetInstance.GetPlayer().GetHeal().Request();
            }

            if (Settings.RandomAttack)
            {
                timer2.Interval = 6746; // magiczne numerki
            }
            else
            {
                timer2.Interval = 5001;
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            label1.Text = "Status: Działam...";
            button1.Enabled = false;
            button2.Enabled = true;

            _stopwatch.Start();

            _startExp = int.Parse(AcapulcoBot.GetInstance.GetPlayer().GetStatistics().h.exp);
            _startGold = int.Parse(AcapulcoBot.GetInstance.GetPlayer().GetStatistics().h.gold);

            timer2.Start();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            timer2.Stop();

            button1.Enabled = true;
            button2.Enabled = false;

            _stopwatch.Stop();

            label1.Text = "Status: Oczekiwanie...";
        }

        private void button4_Click(object sender, EventArgs e)
        {
            LogsForm logsForm = new LogsForm();
            logsForm.Show();
        }

        private void checkBox1_CheckedChanged(object sender, EventArgs e)
        {
            Settings.RandomAttack = !Settings.RandomAttack;
        }

        private void checkBox2_CheckedChanged(object sender, EventArgs e)
        {
            Settings.AutoHeal = !Settings.AutoHeal;
        }

        private void checkBox3_CheckedChanged(object sender, EventArgs e)
        {
            Settings.MinimalPrice = !Settings.MinimalPrice;
        }

        private void checkBox4_CheckedChanged(object sender, EventArgs e)
        {
            Settings.AutoHealOnDie = !Settings.AutoHealOnDie;
        }

        private void numericUpDown1_ValueChanged(object sender, EventArgs e)
        {
            Settings.RandomAttackDelay = (int)numericUpDown1.Value;
        }

        private void numericUpDown2_ValueChanged(object sender, EventArgs e)
        {
            Settings.AutoHealHP = (int)numericUpDown2.Value;
        }

        private void numericUpDown3_ValueChanged(object sender, EventArgs e)
        {
            Settings.MinimalPriceG = (int)numericUpDown3.Value;
        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            button1.Enabled = true;
        }

        private void comboBox2_SelectedIndexChanged(object sender, EventArgs e)
        {
            button1.Enabled = true;
        }

        private void checkBox5_CheckedChanged(object sender, EventArgs e)
        {
            Settings.AutoSell = !Settings.AutoSell;
        }

        private void button3_Click(object sender, EventArgs e)
        {
            InventoryForm inventoryForm = new InventoryForm();
            inventoryForm.Show();
        }
    }
}
