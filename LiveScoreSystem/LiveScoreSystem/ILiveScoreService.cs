using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.ServiceModel.Web;
using System.Text;

namespace LiveScoreSystem
{
    [ServiceContract]
    public interface ILiveScoreService
    {
        [OperationContract]
        void insertPlayer(Player player);

        [OperationContract]
        int insertTeam(Team team);

        [OperationContract]
        int insertMatch(Match match);

        [OperationContract]
        void insertCommentary(Commentary comment);

        [OperationContract]
        string getTeamName(int teamid);

        [OperationContract]
        string getMatchTitle(int matchid);

        [OperationContract]
        void updateToss(int matchid, int batfirstid, string tosscom);

        [OperationContract]
        string getTossCom(int matchid);

        [OperationContract]
        Team getTeamDetails(int teamid);

        [OperationContract]
        Player getPlayerDetails(int playerid);

        [OperationContract]
        List<Player> getNextBatsmans(int teamid);

        [OperationContract]
        List<Player> getOpeners(int teamid);

        [OperationContract]
        List<Player> getBowlers(int teamid);

        [OperationContract]
        double updateBall(int batteamid, int bowlteamid, int strikerid, int nonstriker, int bowlerid, int runonball, int balltype, int wickettype);

        [OperationContract]
        void updateTeamOver(double overs, int batteamid);

        [OperationContract]
        void updateBowlOver(double overs, int bowlerid);

        [OperationContract]
        void updateInning(int inning, int matchid);

        [OperationContract]
        void updateInningComm(string inncomm, int matchid);

        [OperationContract]
        string getInnComment(int matchid);

        [OperationContract]
        int getInning(int matchid);

        [OperationContract]
        int getMatchOvers(int matchid);

        [OperationContract]
        void updateWinner(int winnerid, int matchid);

        [OperationContract]
        void updateEndComm(string endcom, int matchid);

        [OperationContract]
        void updateEndTime(DateTime endtime, int matchid);

        [OperationContract]
        List<Commentary> getCommentary(int matchid);

        [OperationContract]
        void deleteComm();

        [OperationContract]
        List<Player> getAllBatsman(int teamid);

        [OperationContract]
        int getWinnerId(int matchid);

        [OperationContract]
        string getEndComm(int matchid);

    }

    [DataContract]
    public class Commentary
    {
        private int id;
        private int match_id;
        private double over;
        private string comment;
        private DateTime time;
        [DataMember]
        public int Match_ID { get => match_id; set => match_id = value; }
        [DataMember]
        public DateTime Time { get => time; set => time = value; }
        [DataMember]
        public string Comment { get => comment; set => comment = value; }
        [DataMember]
        public double Over { get => over; set => over = value; }
        [DataMember]
        public int Id { get => id; set => id = value; }
    }


    [DataContract]
    public class Match
    {
        private int id;
        private string name;
        private int team1id;
        private int team2id;
        private int overs;
        private int batfirstid;
        private int winnerid;
        private string endComment;
        private string tossComment;
        private DateTime starttime;
        private DateTime endtime;
        private int inning;
        private string inningComment;
        [DataMember]
        public string Name { get => name; set => name = value; }
        [DataMember]
        public int Team1Id { get => team1id; set => team1id = value; }
        [DataMember]
        public int Team2Id { get => team2id; set => team2id = value; }
        [DataMember]
        public DateTime Starttime { get => starttime; set => starttime = value; }
        [DataMember]
        public DateTime Endtime { get => endtime; set => endtime = value; }
        [DataMember]
        public int Overs { get => overs; set => overs = value; }
        [DataMember]
        public int Batfirstid { get => batfirstid; set => batfirstid = value; }
        [DataMember]
        public int WinnerId { get => winnerid; set => winnerid = value; }
        [DataMember]
        public string EndComment { get => endComment; set => endComment = value; }
        [DataMember]
        public string TossComment { get => tossComment; set => tossComment = value; }
        [DataMember]
        public int Id { get => id; set => id = value; }
        [DataMember]
        public int Inning { get => inning; set => inning = value; }
        [DataMember]
        public string InningComment { get => inningComment; set => inningComment = value; }
    }


    [DataContract]
    public class Team
    {
        private int id;
        private string name;
        private int score;
        private int wideball;
        private int noball;
        private int wickets;
        private double overs;
        private double runrate;
        [DataMember]
        public int Noball { get => noball; set => noball = value; }
        [DataMember]
        public int Wideball { get => wideball; set => wideball = value; }
        [DataMember]
        public int Score { get => score; set => score = value; }
        [DataMember]
        public string Name { get => name; set => name = value; }
        [DataMember]
        public int Wickets { get => wickets; set => wickets = value; }
        [DataMember]
        public double Overs { get => overs; set => overs = value; }
        [DataMember]
        public double Runrate { get => runrate; set => runrate = value; }
        [DataMember]
        public int Id { get => id; set => id = value; }
    }


    [DataContract]
    public class Player
    {
        public enum PlayerType{
            BOW,
            BAT,
            AllR
        }
        public enum OutStatus
        {
            NOTOUT,
            OUT
        }
        private int id;
        private string name;
        private int type;
        private int batruns;
        private int bowlruns;
        private int wickets;
        private double strikerate;
        private double economy;
        private double overs;
        private int balls;
        private int fours;
        private int sixes;
        private int team_id;
        private int status;



        [DataMember]
        public string Name
        {
            get
            {
                return name;
            }
            set
            {
                name = value;
            }
        }

        [DataMember]
        public int Type { get => type; set => type = value; }

        [DataMember]
        public int Wickets
        {
            get
            {
                return wickets;
            }
            set
            {
                wickets = value;
            }
        }

        [DataMember]
        public double Strikerate
        {
            get
            {
                return strikerate;
            }
            set
            {
                strikerate = value;
            }
        }
        [DataMember]
        public double Economy
        {
            get
            {
                return economy;
            }
            set
            {
                economy = value;
            }
        }
        [DataMember]
        public double Overs { get => overs; set => overs = value; }

        [DataMember]
        public int Balls
        {
            get
            {
                return balls;
            }
            set
            {
                balls = value;
            }
        }
        [DataMember]
        public int Fours
        {
            get
            {
                return fours;
            }
            set
            {
                fours = value;
            }
        }
        [DataMember]
        public int Sixes
        {
            get
            {
                return sixes;
            }
            set
            {
                sixes = value;
            }
        }
        [DataMember]
        public int Team_id { get => team_id; set => team_id = value; }
        [DataMember]
        public int Batruns { get => batruns; set => batruns = value; }
        [DataMember]
        public int Bowlruns { get => bowlruns; set => bowlruns = value; }
        [DataMember]
        public int Status { get => status; set => status = value; }
        [DataMember]
        public int Id { get => id; set => id = value; }
    }
}
