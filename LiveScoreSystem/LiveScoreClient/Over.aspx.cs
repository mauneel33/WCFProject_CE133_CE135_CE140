using System;
using System.Collections.Generic;
using System.Linq;
using LiveScoreSystem;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace LiveScoreClient
{
    public partial class Over : System.Web.UI.Page
    {
        ServiceRef.LiveScoreServiceClient client;
        protected void Page_Load(object sender, EventArgs e)
        {
            client = new ServiceRef.LiveScoreServiceClient("BasicHttpBinding_ILiveScoreService");
            string team1 = client.getTeamName((int)Session["team1id"]);
            string team2 = client.getTeamName((int)Session["team2id"]);
            lbteam1.Text = team1;
            lbteam2.Text = team2;
            int matchid = (int)Session["matchid"];
            lbmatchtitle.Text = client.getMatchTitle(matchid);
            double overs = (double)Session["overs"];

            //Converting #.6 over to #.0
            overs = Math.Ceiling(overs);
            Session["overs"] = overs;
            lbover.Text = overs.ToString();

            int bowlerid = (int)Session["bowler"];
            int bowlteamid = (int)Session["bowlteamid"];
            int batteamid = (int)Session["batteamid"];

            double bowlovers = client.getPlayerDetails(bowlerid).Overs;
            bowlovers = Math.Ceiling(bowlovers);

            //Updating over
            client.updateBowlOver(bowlovers, bowlerid);
            client.updateTeamOver(overs, batteamid);

            //For all overs done
            int movers = client.getMatchOvers(matchid);
            //Match m = client.getMatchDetails((int)Session["matchid"]);
            if (overs == (double)movers)
            {
                Response.Redirect("inning.aspx");
            }

            //Adding bowlers to dropdownlist
            //ddlnextbowl.Items.Clear();
            if (ddlnextbowl.Items.Count == 0)
            {
                List<Player> b = client.getBowlers(bowlteamid).ToList<Player>();
                foreach (Player p in b)
                {
                    //Current bowler cannot bowl again
                    if (p.Id == bowlerid)
                        continue;
                    ddlnextbowl.Items.Add(new ListItem(p.Name, p.Id.ToString()));
                }
            }
        }

        protected void GoBtn_Click(object sender, EventArgs e)
        {
            //Change strike
            int temp1 = (int)Session["striker"];
            Session["striker"] = (int)Session["nonstriker"];
            Session["nonstriker"] = temp1;

            //Change bowler
            Session["bowler"] = Int32.Parse(ddlnextbowl.SelectedValue);
            Response.Redirect("scoreboard.aspx");
        }
    }
}