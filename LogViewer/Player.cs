using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace LogViewer
{
    public class Player
    {
        private string userID;
        private List<string> aliases;
        private List<string> lowerCaseAliases;
        private List<LogEvent> logEvents;
        private bool isAdmin;
        private bool isOnWatchList;
        private bool isPOM;
        private bool isPermaBanned;
        private int kills;
        private int deaths;
        private int numberOfKickBanPollsStarted;
        private int numberOfMapPollsStarted;
        private int numberOfKicks;
        private int numberOfTmpBans;

        public Player(string _userID)
        {
            userID = _userID;
            aliases = new List<string>();
            lowerCaseAliases = new List<string>();
            logEvents = new List<LogEvent>();
            isAdmin = false;
            isOnWatchList = false;
            isPOM = false;
            isPermaBanned = false;
            kills = 0;
            deaths = 1;
            numberOfKickBanPollsStarted = 0;
            numberOfMapPollsStarted = 0;
            numberOfKicks = 0;
            numberOfTmpBans = 0;
        }

        public Player(string _userID, string initialAlias)
        {
            userID = _userID;
            aliases = new List<string>();
            lowerCaseAliases = new List<string>();
            logEvents = new List<LogEvent>();
            aliases.Add(initialAlias);
            lowerCaseAliases.Add(initialAlias.ToLower());
            isAdmin = false;
            isOnWatchList = false;
            isPOM = false;
            isPermaBanned = false;
            kills = 0;
            deaths = 1;
            numberOfKickBanPollsStarted = 0;
            numberOfMapPollsStarted = 0;
            numberOfKicks = 0;
            numberOfTmpBans = 0;
        }

        public string getUID()
        {
            return userID;
        }

        public void permaBan()
        {
            isPermaBanned = true;
        }

        public void incrementKicks()
        {
            numberOfKicks++;
        }

        public void incrementTmpBans()
        {
            numberOfTmpBans++;
        }

        public void incrementKickBanPolls()
        {
            numberOfKickBanPollsStarted++;
        }

        public void incrementMapPolls()
        {
            numberOfMapPollsStarted++;
        }

        public void incrementKills()
        {
            kills++;
        }

        public void incrementDeaths()
        {
            deaths++;
        }

        public void addLogEvent(LogEvent logEvent)
        {
            logEvents.Add(logEvent);
        }

        public void addAlias(string alias)
        {
            if (!aliases.Contains(alias))
            {
                aliases.Add(alias);
                lowerCaseAliases.Add(alias.ToLower());
            }
        }

        public bool hasAlias(string alias, bool caseSensitive)
        {
            if (caseSensitive)
            {
                return aliases.Contains(alias);
            }
            else
            {
                return lowerCaseAliases.Contains(alias.ToLower());
            }
        }

        public int getNumberOfAliases()
        {
            return aliases.Count;
        }

        public string[] getAliases()
        {
            return aliases.ToArray();
        }

        public LogEvent[] getEvents()
        {
            return logEvents.ToArray();
        }

        public override bool Equals(object obj)
        {
            return ((Player)obj).userID.Equals(this.userID);
        }

        public override int GetHashCode()
        {
            // TODO: Update get hash code to properly call all parts of the Player class
            return base.GetHashCode();
        }

        public int getKills()
        {
            return kills;
        }

        public int getDeaths()
        {
            return deaths - 1;
        }

        public int getKickBanPolls()
        {
            return numberOfKickBanPollsStarted;
        }

        public int getMapPolls()
        {
            return numberOfMapPollsStarted;
        }

        public int getKicks()
        {
            return numberOfKicks;
        }

        public int getTmpBans()
        {
            return numberOfTmpBans;
        }

        public bool getPermaBanned()
        {
            return isPermaBanned;
        }

        internal bool hasAliasContaining(string aliasPart, bool caseSensitive)
        {
            return aliases.Any(x => caseSensitive ? x.Contains(aliasPart) : x.ToLower().Contains(aliasPart.ToLower()));
        }

        public string getPrimaryAlias()
        {
            List<string> tempAliases = new List<string>();
            foreach (LogEvent le in logEvents)
            {
                if (le.GetType() == typeof(KillEvent))
                {
                    KillEvent ke = (KillEvent)le;

                    if (aliases.Contains(ke.getAlias()))
                        tempAliases.Add(ke.getAlias());
                    else
                        tempAliases.Add(ke.getDeathAlias());
                }
                else
                {
                    tempAliases.Add(le.getAlias());
                }
            }

            int count = 0;
            string bestAlias = "";
            foreach (string alias in aliases)
            {
                int currentCount = tempAliases.Count(x => x == alias);
                if (currentCount > count)
                {
                    bestAlias = alias;
                    count = currentCount;
                }
            }

            return bestAlias;
        }

        public int UID { get { return int.Parse(userID); } }

        public string PrimaryAlias { get { return getPrimaryAlias(); } }

        public float KillDeath { get { return ((float)kills)/((float)deaths); } }

        public int KickBanPolls { get { return numberOfKickBanPollsStarted; } }

        public int Kicks { get { return numberOfKicks; } }

        public int TmpBans { get { return numberOfTmpBans; } }

        public string PermaBanned { get { return isPermaBanned.ToString(); } }

        public int NumberOfAliases { get { return getNumberOfAliases(); } }

        public bool IsAdmin { get { return isAdmin; } }

        public bool IsOnWatchList { get { return isOnWatchList; } }

        public bool IsPom { get { return isPOM; } }
    }
}
