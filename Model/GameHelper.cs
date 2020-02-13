namespace BierPongTurnier.Model
{
    public static class GameHelper
    {
        public static int GetGoodBeers(IGame g, Team t)
        {
            if (g == null || t == null) return 0;

            if (t.Equals(g.Team1))
            {
                return g.Score.Beers2 != -1 ? g.Score.Beers2 : 0;
            }
            else if (t.Equals(g.Team2))
            {
                return g.Score.Beers1 != -1 ? g.Score.Beers1 : 0;
            }
            else
            {
                return 0;
            }
        }

        public static int GetBadBeers(IGame g, Team t)
        {
            if (g == null || t == null) return 0;

            if (t.Equals(g.Team1))
            {
                return g.Score.Beers1 != -1 ? g.Score.Beers1 : 0;
            }
            else if (t.Equals(g.Team2))
            {
                return g.Score.Beers2 != -1 ? g.Score.Beers2 : 0;
            }
            else
            {
                return 0;
            }
        }

        public static GameResult GetTeamResult(IGame g, Team t)
        {
            if (!g.Score.IsValid)
            {
                return GameResult.OPEN;
            }

            if (t.Equals(g.Team1))
            {
                if (g.Score.Beers1 < g.Score.Beers2)
                {
                    return GameResult.WIN;
                }
                else if (g.Score.Beers1 > g.Score.Beers2)
                {
                    return GameResult.LOSE;
                }
                else
                {
                    return GameResult.DRAW;
                }
            }
            else if (t.Equals(g.Team2))
            {
                if (g.Score.Beers2 < g.Score.Beers1)
                {
                    return GameResult.WIN;
                }
                else if (g.Score.Beers2 > g.Score.Beers1)
                {
                    return GameResult.LOSE;
                }
                else
                {
                    return GameResult.DRAW;
                }
            }
            else
            {
                return GameResult.NOT_PARTICIPATED;
            }
        }
    }
}