using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.IO;
using System.Threading;
using System.Reflection;

namespace LogViewer
{
    public partial class Form1 : Form
    {
        PlayerTracker playerTracker;
        Dictionary<string, string> CurrentAliases;

        float loadProgressValue = 0.0f;

        public Form1()
        {
            InitializeComponent();
            playerTracker = new PlayerTracker();
            CurrentAliases = new Dictionary<string, string>();
        }

        private void UserViewForm(object p)
        {
            Form2 userInfo = new Form2((Player)p);

            userInfo.ShowDialog();
        }

        private void LoadFile()
        {
            playerTracker.clear();
            CurrentAliases.Clear();
            OpenFileDialog openFileDialog1 = new OpenFileDialog();

            openFileDialog1.InitialDirectory = "c:\\";
            openFileDialog1.Filter = "txt files (*.txt)|*.txt";
            openFileDialog1.FilterIndex = 2;
            openFileDialog1.RestoreDirectory = true;
            openFileDialog1.Multiselect = true;

            if (!string.IsNullOrEmpty(PreviousPath))
            {
                openFileDialog1.InitialDirectory = PreviousPath;
            }

            string theWholeFile = "";

            if (openFileDialog1.ShowDialog() == DialogResult.OK)
            {
                try
                {
                    SortedDictionary<DateTime, string> logFiles = new SortedDictionary<DateTime, string>();

                    for (int i = 0; i < openFileDialog1.FileNames.Length; i++)
                    {
                        DateTime date;
                        try
                        {

                            date = parseFileName(openFileDialog1.FileNames[i]);
                        }
                        catch (Exception ex)
                        {
                            throw new Exception("You selected a log file with improper syntax. Proper Syntax is 'server_log_04_28_12'.");
                        }

                        logFiles.Add(date, openFileDialog1.FileNames[i]);
                    }



                    for (int i = 0; i < logFiles.Count; i++)
                    {

                        StreamReader myStreamReader = new StreamReader(logFiles.Values.ElementAt(i));
                        theWholeFile += myStreamReader.ReadToEnd();
                    }

                    PreviousPath = openFileDialog1.FileNames[0].Substring(0, openFileDialog1.FileNames[0].IndexOf(openFileDialog1.SafeFileNames[0]));
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error: Could not read file from disk. Original error: " + ex.Message);
                    return;
                }

                List<string> individualLines = theWholeFile.Split('\n').ToList();
                loadProgressValue = 0.0f;
                loadProgress.Visible = true;
                loadStatusLabel.Visible = false;
                for (int i = 0; i < individualLines.Count; i++)
                {
                    ParseLog(individualLines[i]);
                    loadProgressValue = (float)i / (float)(individualLines.Count - 1);
                    loadProgress.Value = (int)(loadProgressValue * 100);
                    loadProgress.Update();
                }
                loadProgress.Visible = false;
                loadStatusLabel.Text = "Load Completed Successfully!";
                loadStatusLabel.Visible = true;
                loadStatusLabel.Update();

                
            }
        }

        private void UpdateProgress(object sender, System.EventArgs e)
        {
            loadProgress.Value = (int)(loadProgressValue * 100);
            loadProgress.Update();
        }

        private DateTime parseFileName(string p)
        {
            DateTime dateToReturn;

            //server_log_04_28_12.txt 
            //file format ^
            string[] pSplit = p.Split('_');

            int year = int.Parse((pSplit[pSplit.Length - 1].Split('.'))[0]) + 2000;
            int day = int.Parse(pSplit[pSplit.Length - 2]);
            int month = int.Parse(pSplit[pSplit.Length - 3]);

            dateToReturn = new DateTime(year, month, day);

            return dateToReturn;


        }
        

        private void ParseLog(string IndividualLine)
        {
            string eventLine = IndividualLine;
            //Console.WriteLine(eventLine);
            LogEvent current = LogParser.parseEvent(eventLine);
            if (current != null)
            {
                if (current.GetType() == typeof(LoginEvent))
                {
                    //Console.WriteLine("working");
                    if (playerTracker.getPlayer(((LoginEvent)current).getUID()) == null)
                    {
                        //Console.WriteLine("if");
                        Player currentPlayer = new Player(((LoginEvent)current).getUID(), ((LoginEvent)current).getAlias());
                        currentPlayer.addLogEvent(current);
                        playerTracker.addPlayer(currentPlayer);
                    }
                    else
                    {
                        //Console.WriteLine("else");
                        Player currentPlayer = playerTracker.getPlayer(((LoginEvent)current).getUID());
                        currentPlayer.addAlias(((LoginEvent)current).getAlias());
                        currentPlayer.addLogEvent(current);
                    }


                    if (!CurrentAliases.ContainsKey(((LoginEvent)current).getAlias()))
                    {
                        CurrentAliases.Add(((LoginEvent)current).getAlias(), ((LoginEvent)current).getUID());
                    }
                    else
                    {
                        CurrentAliases[((LoginEvent)current).getAlias()] = ((LoginEvent)current).getUID();
                    }
                }
                else if (current.GetType() == typeof(ChatEvent))
                {
                    Player currentPlayer;
                    if (CurrentAliases.ContainsKey(current.getAlias()))
                    {
                        currentPlayer = playerTracker.getPlayer(CurrentAliases[current.getAlias()]);
                    }
                    else
                    {
                        currentPlayer = playerTracker.getPlayer("000000");
                    }
                    currentPlayer.addLogEvent(current);
                }
                else if (current.GetType() == typeof(KillEvent))
                {
                    Player killerPlayer = null;
                    Player deadPlayer;

                    if (current.getAlias().Length != 0) //if not suicide
                    {
                        if (CurrentAliases.ContainsKey(current.getAlias()))
                        {
                            killerPlayer = playerTracker.getPlayer(CurrentAliases[current.getAlias()]);
                        }
                        else
                        {
                            killerPlayer = playerTracker.getPlayer("000000");
                        }
                    }

                    if (CurrentAliases.ContainsKey(((KillEvent)current).getDeathAlias()))
                    {
                        deadPlayer = playerTracker.getPlayer(CurrentAliases[((KillEvent)current).getDeathAlias()]);
                    }
                    else
                    {
                        deadPlayer = playerTracker.getPlayer("000000");
                    }

                    if (killerPlayer != null)
                    {
                        killerPlayer.addLogEvent(current);
                        killerPlayer.incrementKills();
                    }

                    deadPlayer.addLogEvent(current);
                    deadPlayer.incrementDeaths();
                }
                else if (current.GetType() == typeof(KickBanPollEvent))
                {
                    Player currentPlayer;
                    if (CurrentAliases.ContainsKey(current.getAlias()))
                    {
                        currentPlayer = playerTracker.getPlayer(CurrentAliases[current.getAlias()]);
                    }
                    else
                    {
                        currentPlayer = playerTracker.getPlayer("000000");
                    }
                    currentPlayer.addLogEvent(current);
                    currentPlayer.incrementKickBanPolls();
                }
                else if (current.GetType() == typeof(MapPollEvent))
                {
                    Player currentPlayer;
                    if (CurrentAliases.ContainsKey(current.getAlias()))
                    {
                        currentPlayer = playerTracker.getPlayer(CurrentAliases[current.getAlias()]);
                    }
                    else
                    {
                        currentPlayer = playerTracker.getPlayer("000000");
                    }
                    currentPlayer.addLogEvent(current);
                    currentPlayer.incrementMapPolls();
                }
                else if (current.GetType() == typeof(KickEvent))
                {
                    Player currentPlayer;
                    if (CurrentAliases.ContainsKey(current.getAlias()))
                    {
                        currentPlayer = playerTracker.getPlayer(CurrentAliases[current.getAlias()]);
                    }
                    else
                    {
                        currentPlayer = playerTracker.getPlayer("000000");
                    }
                    currentPlayer.addLogEvent(current);
                    currentPlayer.incrementKicks();
                }
                else if (current.GetType() == typeof(TmpBanEvent))
                {
                    Player currentPlayer;
                    if (CurrentAliases.ContainsKey(current.getAlias()))
                    {
                        currentPlayer = playerTracker.getPlayer(CurrentAliases[current.getAlias()]);
                    }
                    else
                    {
                        currentPlayer = playerTracker.getPlayer("000000");
                    }
                    currentPlayer.addLogEvent(current);
                    currentPlayer.incrementTmpBans();
                }
                else if (current.GetType() == typeof(PermaBanEvent))
                {
                    Player currentPlayer;
                    if (CurrentAliases.ContainsKey(current.getAlias()))
                    {
                        currentPlayer = playerTracker.getPlayer(CurrentAliases[current.getAlias()]);
                    }
                    else
                    {
                        currentPlayer = playerTracker.getPlayer("000000");
                    }
                    currentPlayer.addLogEvent(current);
                    currentPlayer.permaBan();
                }

            }
        }

        /*private void searchAlias_Click(object sender, EventArgs e)
        {
            listView1.Items.Clear();
            
            foreach (Player i in playerTracker.getPlayersWithAlias(comboBoxAliasSearch.Text, checkBoxCase.Checked))
            {
                listView1.Items.Add(new ListViewItem(i.getUID()));
            }
            listView1.Update();
            Console.WriteLine("done");
        }*/

        private void comboBox1_TextChanged(object sender, EventArgs e)
        {
            comboBoxAliasSearch.Items.Clear();

            string fakeAlias = comboBoxAliasSearch.Text;

            List<Player> playersWithAliasPart = playerTracker.getPlayersWithAliasContaining(comboBoxAliasSearch.Text, checkBoxCase.Checked);

            List<string> aliases = new List<string>();

            foreach (Player p in playersWithAliasPart)
                foreach (string a in p.getAliases())
                    if (checkBoxCase.Checked ? a.Contains(comboBoxAliasSearch.Text) : a.ToLower().Contains(comboBoxAliasSearch.Text.ToLower()))
                        aliases.Add(a);

            aliases = aliases.Distinct().ToList();

            aliases.Sort();

            comboBoxAliasSearch.Items.Add(fakeAlias);
            comboBoxAliasSearch.Items.AddRange(aliases.ToArray());

            comboBoxAliasSearch.DroppedDown = true;
            comboBoxAliasSearch.Update();

            comboBoxAliasSearch.Text = fakeAlias;
            comboBoxAliasSearch.SelectionStart = comboBoxAliasSearch.Text.Count();            
        }

        private void loadToolStripMenuItem_Click(object sender, EventArgs e)
        {
            LoadFile();
            button1_Click(null, null);
        }

        private void exitToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void button1_Click(object sender, EventArgs e)
        {

            if (comboBoxAliasSearch.Text != "")
            {

                List<Player> players = (from Player p in playerTracker.getPlayers()
                                           where p.hasAliasContaining(comboBoxAliasSearch.Text, checkBoxCase.Checked)
                                           select p).ToList();
                
                players = SortPlayerList(players);
                dataGridView1.DataSource = players;
            }
            else
            {
                List<Player> players = playerTracker.getPlayers();

                players = SortPlayerList(players);
                dataGridView1.DataSource = players;
            }
            
            
            dataGridView1.Update();
        }

        private List<Player> SortPlayerList(List<Player> players)
        {
            int ascending = ((comboBoxAscendingDescending.Text == "Ascending") ? 1 : -1);
            switch (comboBoxSortBy.Text)
            {
                case "UID":
                    players.Sort(new Comparison<Player>((x, y) => x.UID.CompareTo(y.UID) * ascending));
                    break;
                case "Kicks":
                    players.Sort(new Comparison<Player>((x, y) => x.Kicks.CompareTo(y.Kicks) * ascending));
                    break;
                case "Kick/Ban Polls":
                    players.Sort(new Comparison<Player>((x, y) => x.KickBanPolls.CompareTo(y.KickBanPolls) * ascending));
                    break;
                case "K/D Ratio":
                    players.Sort(new Comparison<Player>((x, y) => x.KillDeath.CompareTo(y.KillDeath) * ascending));
                    break;
                case "Number Of Aliases":
                    players.Sort(new Comparison<Player>((x, y) => x.NumberOfAliases.CompareTo(y.NumberOfAliases) * ascending));
                    break;
                case "Perma Banned":
                    players.Sort(new Comparison<Player>((x, y) => x.PermaBanned.CompareTo(y.PermaBanned) * ascending));
                    break;
                case "Temp Bans":
                    players.Sort(new Comparison<Player>((x, y) => x.TmpBans.CompareTo(y.TmpBans) * ascending));
                    break;
                case "Alias"://sort by alias name is default
                default:
                    players.Sort(new Comparison<Player>((x, y) => x.PrimaryAlias.CompareTo(y.PrimaryAlias) * ascending));
                    break;
            }

            return players;
            
        }

        private void dataGridView1_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            Thread t = new Thread(new ParameterizedThreadStart(UserViewForm));

            if (dataGridView1.SelectedRows.Count > 0)
                t.Start(playerTracker.getPlayer(dataGridView1.SelectedRows[0].Cells[0].Value.ToString()));
        }

        private void aboutToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            string about = "";
            about += "Log Viewer was created by POM_Wind with help from Bellicosity. Send any questions, comments, or bug reports to scheel.ryan@gmail.com.";
            about += string.Format("\n\nCurrent Version = {0}",PublishVersion);
            MessageBox.Show(about, "About");
        }

        private string PublishVersion
        {
            get
            {
                if (System.Deployment.Application.ApplicationDeployment.IsNetworkDeployed)
                {
                    Version ver = System.Deployment.Application.ApplicationDeployment.CurrentDeployment.CurrentVersion;
                    return string.Format("{0}.{1}.{2}.{3}", ver.Major, ver.Minor, ver.Build, ver.Revision);
                }
                else
                    return "Not Published";
            }
        }

        private string PreviousPath
        {
            get
            {
                return Properties.Settings.Default.PreviousPath;
            }

            set
            {
                Properties.Settings.Default.PreviousPath = value;
                Properties.Settings.Default.Save();
            }
        }

     
        
    }
}
