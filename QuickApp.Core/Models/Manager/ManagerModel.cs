namespace QuickApp.Core.Models.Manager
{
    public class ManagerModel
    {
        public int manager_id { get; set; }
        public string first_name { get; set; }
        public string last_name { get; set; }
        public string email { get; set; }
        public string phone_number { get; set; }
        public string work_role { get; set; }
        public ICollection<TeamManagerModel> TeamManagers { get; set; }
    }
}
