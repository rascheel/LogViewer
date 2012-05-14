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
            Dictionary<DateTime, string> eachFileRead = new Dictionary<DateTime, string>();
            ClearLogViewerData();
            OpenFileDialog loadLogFilesDialog = InitializeOpenFileDialog();

            if (loadLogFilesDialog.ShowDialog() == DialogResult.OK)
            {
                AttemptToLoadFiles(eachFileRead, loadLogFilesDialog);         
            }
        }

        private void AttemptToLoadFiles(Dictionary<DateTime, string> eachFileRead, OpenFileDialog openFileDialog1)
        {
            try
            {
                SortedDictionary<DateTime, string> logFiles = new SortedDictionary<DateTime, string>();
                List<KeyValuePair<DateTime, string>> individualLines = new List<KeyValuePair<DateTime, string>>();

                VerifyFileNames(openFileDialog1, logFiles);

                ReadFiles(eachFileRead, logFiles);

                Utils.PreviousPath = openFileDialog1.FileNames[0].Substring(0, openFileDialog1.FileNames[0].IndexOf(openFileDialog1.SafeFileNames[0]));

                SeperateEachLine(eachFileRead, individualLines);

                ParseEachLine(individualLines);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error: Could not read file from disk. Original error: " + ex.Message);
            }
        }

        private void ParseEachLine(List<KeyValuePair<DateTime, string>> individualLines)
        {
            InitializeProgressBar();
            for (int i = 0; i < individualLines.Count; i++)
            {
                ParseLog(individualLines.ElementAt(i).Key, individualLines.ElementAt(i).Value);
                UpdateProgressBar(individualLines, i);
            }
            FinishProgress();
        }

        private void FinishProgress()
        {
            loadProgress.Visible = false;
            loadStatusLabel.Text = "Load Completed Successfully!";
            loadStatusLabel.Visible = true;
            loadStatusLabel.Update();
        }

        private void UpdateProgressBar(List<KeyValuePair<DateTime, string>> individualLines, int i)
        {
            loadProgressValue = (float)i / (float)(individualLines.Count - 1);
            loadProgress.Value = (int)(loadProgressValue * 100);
            loadProgress.Update();
        }

        private void InitializeProgressBar()
        {
            loadProgressValue = 0.0f;
            loadProgress.Visible = true;
            loadStatusLabel.Visible = false;
        }

        private static void SeperateEachLine(Dictionary<DateTime, string> eachFileRead, List<KeyValuePair<DateTime, string>> individualLines)
        {
            for (int i = 0; i < eachFileRead.Count; i++)
            {
                foreach (var line in eachFileRead.Values.ElementAt(i).Split('\n'))
                {
                    individualLines.Add(new KeyValuePair<DateTime, string>(eachFileRead.Keys.ElementAt(i), line));
                }
            }
        }

        private static void ReadFiles(Dictionary<DateTime, string> eachFileRead, SortedDictionary<DateTime, string> logFiles)
        {
            for (int i = 0; i < logFiles.Count; i++)
            {

                StreamReader myStreamReader = new StreamReader(logFiles.Values.ElementAt(i));
                eachFileRead.Add(logFiles.Keys.ElementAt(i), myStreamReader.ReadToEnd());
            }
        }

        private void VerifyFileNames(OpenFileDialog openFileDialog1, SortedDictionary<DateTime, string> logFiles)
        {
            for (int i = 0; i < openFileDialog1.FileNames.Length; i++)
            {
                DateTime date;
                try
                {

                    date = parseFileName(openFileDialog1.FileNames[i]);
                }
                catch
                {
                    throw new Exception("You selected a log file with improper syntax. Proper Syntax is 'server_log_04_28_12'.");
                }

                logFiles.Add(date, openFileDialog1.FileNames[i]);
            }
        }

        private void ClearLogViewerData()
        {
            playerTracker.clear();
            CurrentAliases.Clear();
        }

        private OpenFileDialog InitializeOpenFileDialog()
        {
            OpenFileDialog ofd = new OpenFileDialog();

            ofd.InitialDirectory = "c:\\";
            ofd.Filter = "txt files (*.txt)|*.txt";
            ofd.FilterIndex = 2;
            ofd.RestoreDirectory = true;
            ofd.Multiselect = true;

            if (!string.IsNullOrEmpty(Utils.PreviousPath))
            {
                ofd.InitialDirectory = Utils.PreviousPath;
            }

            return ofd;
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
        

        private void ParseLog(DateTime date, string IndividualLine)
        {
            string eventLine = IndividualLine;
            //Console.WriteLine(eventLine);
            LogEvent current = LogParser.parseEvent(date, eventLine);
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
            int ascending = ((comboBoxAscendingDescending.Text == "Descending") ? -1 : 1);
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
            about += string.Format("\n\nCurrent Version = {0}", Utils.PublishVersion);
            MessageBox.Show(about, "About");
        }
    }
}
