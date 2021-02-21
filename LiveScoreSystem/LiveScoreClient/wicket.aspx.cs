using System;
using LiveScoreSystem;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace LiveScoreClient
{
    public partial class wicket : System.Web.UI.Page
    {
        ServiceRef.LiveScoreServiceClient client;
        protected void Page_Load(object sender, EventArgs e)
        {
            client = new ServiceRef.LiveScoreServiceClient("BasicHttpBinding_ILiveScoreService");
            Team batteam = client.getTeamDetails((int)Session["batteamid"]);

            //For all wickets down
            if(batteam.Wickets == 10)
            {
                Response.Redirect("inning.aspx");
            }

            string team1 = client.getTeamName((int)Session["team1id"]);
            string team2 = client.getTeamName((int)Session["team2id"]);
            lbteam1.Text = team1;
            lbteam2.Text = team2;
            lbmatchtitle.Text = client.getMatchTitle((int)Session["matchid"]);
            List<Player> p = client.getNextBatsmans((int)Session["batteamid"]).ToList();
            int notoutpid;
            Player striker = client.getPlayerDetails((int)Session["striker"]);
            //Striker is out
            if (striker.Status == (int)Player.OutStatus.OUT)
            {
                notoutpid = (int)Session["nonstriker"];
            }
            //Non striker is out
            else
            {
                notoutpid = (int)Session["striker"];
            }
            if (ddlnextbat.Items.Count == 1)
            {
                foreach(Player a in p)
                {
                    if (a.Id == notoutpid)
                        continue;
                    ddlnextbat.Items.Add(new ListItem(a.Name, a.Id.ToString()));
                }
            }
        }

        protected void ddlnextbat_SelectedIndexChanged(object sender, EventArgs e)
        {
            ddlbat.Items.Clear();
            int notoutpid = -1;
            Player striker = client.getPlayerDetails((int)Session["striker"]);
            //Striker is out
            if (striker.Status == (int)Player.OutStatus.OUT)
            {
                notoutpid = (int)Session["nonstriker"];
            }
            //Non striker is out
            else
            {
                notoutpid = (int)Session["striker"];
            }
            Player notoutp = client.getPlayerDetails(notoutpid);
            ddlbat.Items.Add(new ListItem(notoutp.Name, notoutp.Id.ToString()));
            int nextbatid = Int32.Parse(ddlnextbat.SelectedValue);
            Player n = client.getPlayerDetails(nextbatid);
            ddlbat.Items.Add(new ListItem(n.Name, n.Id.ToString()));
        }

        protected void GoBtn_Click(object sender, EventArgs e)
        {
            int strikerid = Int32.Parse(ddlbat.SelectedValue);
            Session["striker"] = strikerid;
            foreach(ListItem a in ddlbat.Items)
            {
                if(strikerid != Int32.Parse(a.Value))
                {
                    Session["nonstriker"] = Int32.Parse(a.Value);
                    break;
                }
            }
            double overs = (double)Session["overs"];
            double temp = overs - (int)Math.Floor(overs);
            temp = Math.Round((Double)temp, 1);
            if (temp == 0.6)
                Response.Redirect("over.aspx");
            else
                Response.Redirect("scoreboard.aspx");
        }
    }
}