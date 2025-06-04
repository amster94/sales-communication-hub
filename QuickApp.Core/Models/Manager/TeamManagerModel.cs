using QuickApp.Core.Models.Team;

namespace QuickApp.Core.Models.Manager
{
    public class TeamManagerModel
    {
        public int team_id {  get; set; }
        public int manager_id { get; set; }
        //public object Team { get; internal set; }
        //public object Manager { get; internal set; }
        public TeamModel Team { get; set; }
        public ManagerModel Manager { get; set; }
    }
}
