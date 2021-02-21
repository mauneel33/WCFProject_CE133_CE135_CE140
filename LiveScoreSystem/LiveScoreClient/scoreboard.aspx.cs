using System;
using LiveScoreSystem;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace LiveScoreClient
{
    public partial class scoreboard : System.Web.UI.Page
    {
        ServiceRef.LiveScoreServiceClient client = new ServiceRef.LiveScoreServiceClient("BasicHttpBinding_ILiveScoreService");
        protected List<Commentary> c;
        protected int i=0;
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                client = new ServiceRef.LiveScoreServiceClient("BasicHttpBinding_ILiveScoreService");
                int mid = (int)Session["matchid"];
                int inn = client.getInning(mid);
                if (inn == 1)
                {
                    string tosscomm = client.getTossCom(mid);
                    lbtoss.Text = tosscomm;
                    lbinning.Text = inn.ToString() + "st Innings";
                }
                else
                {
                    string inncomm = client.getInnComment(mid);
                    lbtoss.Text = inncomm;
                    lbinning.Text = inn.ToString() + "nd Innings";
                    Team bowlteam = client.getTeamDetails((int)Session["bowlteamid"]);
                    int target = bowlteam.Score + 1;
                    lbtarget.Text = "Target: " + target.ToString();
                }
                string team1 = client.getTeamName((int)Session["team1id"]);
                string team2 = client.getTeamName((int)Session["team2id"]);
                lbteam1.Text = team1;
                lbteam2.Text = team2;
                lbmatchtitle.Text = client.getMatchTitle((int)Session["matchid"]);
                int bid = (int)Session["batteamid"];

                //Batting Team stats
                Team t = client.getTeamDetails(bid);
                lbbatteam.Text = t.Name;
                lbtscore.Text = t.Score.ToString();
                lbtwickets.Text = t.Wickets.ToString();
                lbtover.Text = t.Overs.ToString();
                lbtrr.Text = t.Runrate.ToString();

                Player str = client.getPlayerDetails((int)Session["striker"]);
                Player nonstr = client.getPlayerDetails((int)Session["nonstriker"]);
                Player bowl = client.getPlayerDetails((int)Session["bowler"]);

                //Striker stats
                lbbat1name.Text = str.Name;
                lbbat1runs.Text = str.Batruns.ToString();
                lbbat1balls.Text = str.Balls.ToString();
                lbbat1fours.Text = str.Fours.ToString();
                lbbat1sixes.Text = str.Sixes.ToString();
                lbbat1strk.Text = str.Strikerate.ToString();

                //Non Striker Stats
                lbbat2name.Text = nonstr.Name;
                lbbat2runs.Text = nonstr.Batruns.ToString();
                lbbat2balls.Text = nonstr.Balls.ToString();
                lbbat2fours.Text = nonstr.Fours.ToString();
                lbbat2sixes.Text = nonstr.Sixes.ToString();
                lbbat2strk.Text = nonstr.Strikerate.ToString();

                //Bowler stats
                lbbowl.Text = bowl.Name;
                lbbowlover.Text = bowl.Overs.ToString();
                lbbowlrun.Text = bowl.Bowlruns.ToString();
                lbbowlwick.Text = bowl.Wickets.ToString();
                lbbowleco.Text = bowl.Economy.ToString();

                c = client.getCommentary(mid).ToList();
            }
        }

        protected void AddBtn_Click(object sender, EventArgs e)
        {
            //int batteamid, int bowlteamid, int strikerid, int nonstriker, int bowlerid, int runonball, int balltype, int wickettype)
            int batteamid = (int)Session["batteamid"];
            int bowlteamid = (int)Session["bowlteamid"];
            int strikerid = (int)Session["striker"];
            int nonstrikerid = (int)Session["nonstriker"];
            int bowlerid = (int)Session["bowler"];
            int runonball = Int32.Parse(ddlrun.SelectedValue);
            int balltype = Int32.Parse(ddlballtype.SelectedValue);
            int wickettype = Int32.Parse(ddlwicket.SelectedValue);
            double overs = client.updateBall(batteamid, bowlteamid, strikerid, nonstrikerid, bowlerid, runonball, balltype, wickettype);
            int matchid = (int)Session["matchid"];
            Commentary c = new Commentary
            {
                Match_ID = matchid,
                Over = overs,
                Comment = tbcomment.Text,
            };
            //Adding Commentary
            client.insertCommentary(c);

            Session["overs"] = overs;

            //For wicket, redirect to other page
            if(wickettype != 0)
            {
                Response.Redirect("wicket.aspx");
            }

            //Change strike in odd runs
            if(runonball == 1 || runonball == 3)
            {
                int temp1 = (int)Session["striker"];
                Session["striker"] = (int)Session["nonstriker"];
                Session["nonstriker"] = temp1;
            }

            double temp = overs - (int)Math.Floor(overs);
            temp = Math.Round((Double)temp, 1);
            if (temp == 0.6)
                Response.Redirect("over.aspx");

            Response.Redirect("scoreboard.aspx");
        }

        protected void EndBtn_Click(object sender, EventArgs e)
        {
            Response.Redirect("matchEnd.aspx");
        }
    }
}