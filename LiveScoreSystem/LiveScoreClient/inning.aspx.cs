using System;
using System.Collections.Generic;
using System.Linq;
using LiveScoreSystem;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace LiveScoreClient
{
    public partial class inning : System.Web.UI.Page
    {
        
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                ServiceRef.LiveScoreServiceClient client = new ServiceRef.LiveScoreServiceClient("BasicHttpBinding_ILiveScoreService");
                string team1 = client.getTeamName((int)Session["team1id"]);
                string team2 = client.getTeamName((int)Session["team2id"]);
                lbteam1.Text = team1;
                lbteam2.Text = team2;
                lbmatchtitle.Text = client.getMatchTitle((int)Session["matchid"]);

                int matchid = (int)Session["matchid"];
                int inn = client.getInning(matchid);
                //For 2nd Innings
                if (inn == 2)
                {
                    Response.Redirect("matchEnd.aspx");
                }

                //Swap batteam and bowlteam
                int temp = (int)Session["batteamid"];
                Session["batteamid"] = (int)Session["bowlteamid"];
                Session["bowlteamid"] = temp;

                //Declaring striker non striker
                List<Player> ob = client.getOpeners((int)Session["batteamid"]).ToList();
                Session["striker"] = ob[0].Id;
                Session["nonstriker"] = ob[1].Id;

                //Select Bowler
                if (ddlfbowl.Items.Count == 0)
                {
                    int bowlteamid = (int)Session["bowlteamid"];
                    List<Player> b = client.getBowlers(bowlteamid).ToList<Player>();
                    foreach (Player p in b)
                    {
                        ddlfbowl.Items.Add(new ListItem(p.Name, p.Id.ToString()));
                    }
                }
            }
        }

        protected void GoBtn_Click(object sender, EventArgs e)
        {
            ServiceRef.LiveScoreServiceClient client = new ServiceRef.LiveScoreServiceClient("BasicHttpBinding_ILiveScoreService");
            //Update Bowler
            Session["bowler"] = Int32.Parse(ddlfbowl.SelectedValue);

            int matchid = (int)Session["matchid"];
            //Update Innings and Inning Comment
            client.updateInning(2, matchid);
            string incom = tainncom.Text;
            client.updateInningComm(incom, matchid);
            client.deleteComm();
            Response.Redirect("scoreboard.aspx");
        }
    }
}