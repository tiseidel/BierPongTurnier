using BierPongTurnier.Model;
using System.Collections.Generic;

namespace BierPongTurnier
{
    public class TournamentCreator
    {
        public static List<Group> FromCount(int teamCount, int groupCount)
        {
            var groups = new List<Group>();

            for (int i = 0; i < groupCount;)
            {
                groups.Add(new Group() { Name = "Tisch " + ++i });
            }

            for (int i = 0; i < teamCount; i++)
            {
                int grNr = i % groupCount;
                groups[grNr].Teams.Add(new Team() { Name = "Team " + (i + 1) });
            }

            foreach (Group group in groups)
            {
                TournamentCreator.CreateRoundRobin(group);
            }
            return groups;
        }

        public static List<Group> FromPlayers(List<string> players, int groupCount, bool lastAlone = true)
        {
            int playerCount = players.Count;
            int grNr = 0;
            var groups = new List<Group>();

            if (playerCount == 0 || groupCount == 0)
            {
                return groups;
            }

            for (int i = 0; i < groupCount;)
            {
                groups.Add(new Group() { Name = "Tisch " + ++i });
            }

            int x = 0;
            for (; x < playerCount / 2; x++)
            {
                grNr = x % groupCount;
                string name = players[x * 2] + " + " + players[x * 2 + 1];
                groups[grNr].Teams.Add(new Team() { Name = name });
            }

            if (playerCount % 2 == 1)
            {
                if (lastAlone)
                {
                    var g = groups[x % groupCount];
                    g.Teams.Add(new Team() { Name = players[playerCount - 1] });
                }
                else
                {
                    var g = groups[(x - 1) % groupCount];
                    g.Teams[g.Teams.Count - 1].Name += players[playerCount - 1];
                }
            }

            foreach (Group group in groups)
            {
                TournamentCreator.CreateRoundRobin(group);
            }

            return groups;
        }

        public static List<Group> FromTeams(List<string> teams, int groupCount)
        {
            int teamCount = teams.Count;
            var groups = new List<Group>();

            for (int i = 0; i < groupCount;)
            {
                groups.Add(new Group() { Name = "Tisch " + ++i });
            }

            for (int i = 0; i < teamCount; i++)
            {
                int grNr = i % groupCount;
                groups[grNr].Teams.Add(new Team() { Name = teams[i] });
            }

            foreach (Group group in groups)
            {
                TournamentCreator.CreateRoundRobin(group);
            }
            return groups;
        }

        public static void CreateRoundRobin(Group group)
        {
            int left = 0; int right = 1;
            group.Games.Clear();
            switch (group.Teams.Count)
            {
                case 2:
                    {
                        int[,] m =
                        {
                                {1, 2}
                        };
                        for (int j = 0; j < m.Length / 2; j++)
                        {
                            group.Games.Add(new Game(group.Teams[m[j, left] - 1], group.Teams[m[j, right] - 1]));
                        }
                    }
                    break;

                case 3:
                    {
                        int[,] m =
                        {
                                {1, 2},
                                {2, 3},
                                {3, 1}
                        };
                        for (int j = 0; j < m.Length / 2; j++)
                        {
                            group.Games.Add(new Game(group.Teams[m[j, left] - 1], group.Teams[m[j, right] - 1]));
                        }
                    }
                    break;

                case 4:
                    {
                        int[,] m =
                        {
                                {2, 1},
                                {3, 4},
                                {4, 2},
                                {1, 3},
                                {4, 1},
                                {2, 3}
                        };
                        for (int j = 0; j < m.Length / 2; j++)
                        {
                            group.Games.Add(new Game(group.Teams[m[j, left] - 1], group.Teams[m[j, right] - 1]));
                        }
                    }
                    break;

                case 5:
                    {
                        int[,] m =
                        {
                                {1, 4},
                                {2, 5},
                                {3, 1},
                                {4, 2},
                                {5, 3},
                                {1, 2},
                                {4, 5},
                                {3, 2},
                                {1, 5},
                                {4, 3}
                        };
                        for (int j = 0; j < m.Length / 2; j++)
                        {
                            group.Games.Add(new Game(group.Teams[m[j, left] - 1], group.Teams[m[j, right] - 1]));
                        }
                    }
                    break;
                case 6:
                    {
                        int[,] m =
                        {
                                {1, 2},
                                {3, 4},
                                {5, 6},
                                {1, 3},
                                {2, 5},
                                {6, 4},
                                {3, 5},
                                {1, 4},
                                {2, 6},
                                {1, 5},
                                {6, 3},
                                {2, 4},
                                {1, 6},
                                {5, 4},
                                {2, 3}
                        };
                        for (int j = 0; j < m.Length / 2; j++)
                        {
                            group.Games.Add(new Game(group.Teams[m[j, left] - 1], group.Teams[m[j, right] - 1]));
                        }
                    }
                    break;
            }
        }
    }
}