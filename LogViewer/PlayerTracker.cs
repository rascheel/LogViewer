using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace LogViewer
{
    class PlayerTracker
    {
        private List<Player> players;


        public PlayerTracker()
        {
            players = new List<Player>();
            players.Add(new Player("000000")); //userid for unknown users is 0, as that is an impossible uid for something to have
        }


        public void addPlayer(Player _player)
        {
            if(!players.Contains(_player))
            {
                players.Add(_player);
            }
        }

        public void clear()
        {
            players.Clear();
            players.Add(new Player("000000")); //userid for unknown users is 0, as that is an impossible uid for something to have
        }

        public Player getPlayer(string uid)
        {
            Player playerToFind = new Player(uid);
            if (players.Contains(playerToFind))
            {
                return players[players.IndexOf(playerToFind)];
            }
            else
            {
                return null;
            }
        }

        public List<Player> getPlayersWithAlias(string alias, bool caseSensitive)
        {
            List<Player> playersToFind = new List<Player>();

            foreach (Player player in players)
            {
                if (player.hasAlias(alias, caseSensitive))
                {
                    playersToFind.Add(player);
                }
            }

            return playersToFind;
        }

        public List<Player> getPlayersWithAliasContaining(string aliasPart, bool caseSensitive)
        {

            List<Player> playersToFind = new List<Player>();

            foreach (Player p in players)
            {
                if(p.hasAliasContaining(aliasPart, caseSensitive))
                {
                    playersToFind.Add(p);
                }
            }

            return playersToFind;
        }

        public List<Player> getPlayers()
        {

            return (from Player p in players
                    where p.UID != 0
                    select p).ToList();
        }

    }
}
