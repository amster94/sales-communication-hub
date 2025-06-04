using QuickApp.Core.Models.Manager;
using QuickApp.Core.Models.SalesPerson;

namespace QuickApp.Core.Models.Team
{
    public class TeamModel
    {
        public int team_id { get; set; }
        public string team_name { get; set; }
        public string team_description { get; set; }
        public string region { get; set; }
        public DateTime creation_date { get; set; }
        public bool active_status { get; set; }
        public string performance_rating { get; set; }
        public DateTime last_updated { get; set; }
        public bool is_virtual_team { get; set; }
        public ICollection<TeamManagerModel> TeamManagers { get; set; }
        public ICollection<TeamSalesPersonModel> TeamSalesPerson { get; set; }
    }
}
