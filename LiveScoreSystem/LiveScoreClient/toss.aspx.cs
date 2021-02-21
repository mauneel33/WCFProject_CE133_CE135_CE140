using System;
using LiveScoreSystem;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace LiveScoreClient
{
    public partial class toss : System.Web.UI.Page
    {
        ServiceRef.LiveScoreServiceClient client;
        protected void Page_Load(object sender, EventArgs e)
        {
            client = new ServiceRef.LiveScoreServiceClient("BasicHttpBinding_ILiveScoreService");
            string team1 = client.getTeamName((int)Session["team1id"]);
            string team2 = client.getTeamName((int)Session["team2id"]);
            lbteam1.Text = team1;
            lbteam2.Text = team2;
            lbmatchtitle.Text = client.getMatchTitle((int)Session["matchid"]);
            if(ddlbatfirst.Items.Count == 1)
            {
                ddlbatfirst.Items.Add(new ListItem(team1, Session["team1id"].ToString()));
                ddlbatfirst.Items.Add(new ListItem(team2, Session["team2id"].ToString()));
            }
        }

        protected void StartBtn_Click(object sender, EventArgs e)
        {
            int mid = (int)Session["matchid"];
            int bid = Int32.Parse(ddlbatfirst.SelectedValue);
            Session["batteamid"] = bid;
            int team1id = (int)Session["team1id"];
            int team2id = (int)Session["team2id"];
            if (bid == team1id)
                Session["bowlteamid"] = team2id;
            else
                Session["bowlteamid"] = team1id;
            string tc = tatosscom.Text;
            client.updateToss(mid, bid, tc);
            List<Player> ob = client.getOpeners(bid).ToList<Player>();
            Session["striker"] = ob[0].Id;
            Session["nonstriker"] = ob[1].Id;
            Session["bowler"] = Int32.Parse(ddlfbowl.SelectedValue);
            Response.Redirect("scoreboard.aspx");
        }

        protected void ddlbatfirst_SelectedIndexChanged(object sender, EventArgs e)
        {
            ddlfbowl.Items.Clear();
            int bid = Int32.Parse(ddlbatfirst.SelectedValue);
            int bwid;
            if (bid == (int)Session["team1id"])
                bwid = (int)Session["team2id"];
            else
            bwid = (int)Session["team1id"];
            List<Player> b = client.getBowlers(bwid).ToList<Player>();
            foreach (Player p in b)
            {
                ddlfbowl.Items.Add(new ListItem(p.Name, p.Id.ToString()));
            }
        }
    }
}