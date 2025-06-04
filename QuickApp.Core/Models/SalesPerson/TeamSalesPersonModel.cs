using QuickApp.Core.Models.Team;

namespace QuickApp.Core.Models.SalesPerson
{
    public class TeamSalesPersonModel
    {
        public int team_id { get; set; }
        public int sales_rep_id { get; set; }
        public TeamModel Team { get; set; }
        public SalesTeamModel SalesTeam { get; set; }
    }
}
