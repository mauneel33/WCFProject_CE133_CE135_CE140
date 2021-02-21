using System;
using System.Collections.Generic;
using LiveScoreSystem;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace LiveScoreClient
{
    public partial class scorecard : System.Web.UI.Page
    {
        ServiceRef.LiveScoreServiceClient client;
        protected int i = 0;
        protected List<Player> bat1;
        protected List<Player> bowl1;
        protected List<Player> bat2;
        protected List<Player> bowl2;
        protected void Page_Load(object sender, EventArgs e)
        {
            client = new ServiceRef.LiveScoreServiceClient("BasicHttpBinding_ILiveScoreService");
            int matchid = (int)Session["matchid"];
            int team2 = (int)Session["batteamid"];
            int team1 = (int)Session["bowlteamid"];
            bat1 = client.getAllBatsman(team1).ToList();
            bat2 = client.getAllBatsman(team2).ToList();
            bowl1 = client.getBowlers(team2).ToList();
            bowl2 = client.getBowlers(team1).ToList();

            int winid = client.getWinnerId(matchid);
            lbwinner.Text = client.getTeamDetails(winid).Name;
            lbendcom.Text = client.getEndComm(matchid);

            //Team1 stats
            Team t1 = client.getTeamDetails(team1);
            lbteam1.Text = t1.Name;
            lbscore1.Text = t1.Score.ToString();
            lbwick1.Text = t1.Wickets.ToString();
            lbover1.Text = t1.Overs.ToString();
            lbrr1.Text = t1.Runrate.ToString();
            lbwide1.Text = t1.Wideball.ToString();
            lbnoball1.Text = t1.Noball.ToString();

            //Team2 stats
            Team t2 = client.getTeamDetails(team2);
            lbteam2.Text = t2.Name;
            lbscore2.Text = t2.Score.ToString();
            lbwick2.Text = t2.Wickets.ToString();
            lbover2.Text = t2.Overs.ToString();
            lbrr2.Text = t2.Runrate.ToString();
            lbwide2.Text = t2.Wideball.ToString();
            lbnoball2.Text = t2.Noball.ToString();
        }
    }
}