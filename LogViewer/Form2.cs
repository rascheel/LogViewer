using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace LogViewer
{
    public partial class Form2 : Form
    {
        private Player user;

        public Form2(Player player)
        {
            InitializeComponent();

            user = player;

            textBoxUserID.Text = player.getUID();
            textBoxNumberOfAliases.Text = player.getNumberOfAliases().ToString();
            textBoxKills.Text = player.getKills().ToString();
            textBoxDeaths.Text = player.getDeaths().ToString();
            textBoxKickBanPolls.Text = player.getKickBanPolls().ToString();
            textBoxMapPolls.Text = player.getMapPolls().ToString();
            textBoxKicks.Text = player.getKicks().ToString();
            textBoxTmpBans.Text = player.getTmpBans().ToString();


            if (player.getPermaBanned())
            {
                textBoxPermaBanned.Text = "Yes";
            }
            else
            {
                textBoxPermaBanned.Text = "No";
            }

            textBoxAliases.Lines = player.getAliases();

            populateLogEvents();

        }

        private void populateLogEvents()
        {
            LogEvent[] logEvents = user.getEvents();


            List<string> events = new List<string>();

            foreach (LogEvent logEvent in logEvents)
            {
                if (logEvent.GetType() == typeof(ChatEvent) && checkBoxChatEvents.Checked)
                {
                    events.Add(logEvent.getEventLine());
                }
                else if (logEvent.GetType() == typeof(LoginEvent) && checkBoxLoginEvents.Checked)
                {
                    events.Add(logEvent.getEventLine());
                }
                else if (logEvent.GetType() == typeof(KickBanPollEvent) && checkBoxKickBanPollEvents.Checked)
                {
                    events.Add(logEvent.getEventLine());
                }
                else if (logEvent.GetType() == typeof(KickEvent) && checkBoxKickEvents.Checked)
                {
                    events.Add(logEvent.getEventLine());
                }
                else if (logEvent.GetType() == typeof(MapPollEvent) && checkBoxMapChangePollEvents.Checked)
                {
                    events.Add(logEvent.getEventLine());
                }
                else if (logEvent.GetType() == typeof(PermaBanEvent) && checkBoxPermaBanEvents.Checked)
                {
                    events.Add(logEvent.getEventLine());
                }
                else if (logEvent.GetType() == typeof(TmpBanEvent) && checkBoxTmpBanEvents.Checked)
                {
                    events.Add(logEvent.getEventLine());
                }
                else if (logEvent.GetType() == typeof(KillEvent) && checkBoxKillEvents.Checked)
                {
                    events.Add(logEvent.getEventLine());
                }
            }


            textBoxLogEvents.Clear();
            textBoxLogEvents.Lines = events.ToArray();
        }

        private void checkBoxs_CheckedChanged(object sender, EventArgs e)
        {
            populateLogEvents();
        }
    }
}
