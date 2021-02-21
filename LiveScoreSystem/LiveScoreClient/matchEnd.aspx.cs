using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace LiveScoreClient
{
    public partial class matchEnd : System.Web.UI.Page
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
                int team1id = (int)Session["team1id"];
                int team2id = (int)Session["team2id"];
                ddlwinteam.Items.Add(new ListItem(team1, team1id.ToString()));
                ddlwinteam.Items.Add(new ListItem(team2, team2id.ToString()));
            }
        }

        protected void EndBtn_Click(object sender, EventArgs e)
        {
            int winteamid = Int32.Parse(ddlwinteam.SelectedValue);
            int matchid = (int)Session["matchid"];
            string endcom = taendcom.Text;
            DateTime endtime = DateTime.Now;
            ServiceRef.LiveScoreServiceClient client = new ServiceRef.LiveScoreServiceClient("BasicHttpBinding_ILiveScoreService");
            client.updateWinner(winteamid, matchid);
            client.updateEndComm(endcom, matchid);
            client.updateEndTime(endtime, matchid);
            client.deleteComm();
            Response.Redirect("scorecard.aspx");
        }
    }
}