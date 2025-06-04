using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace QuickApp.Core.Models.SalesLead
{
    [Table("Leads")]
    public class LeadModel
    {
        [Key]
        public int lead_id { get; set; }

        [JsonPropertyName("leadName")]
        public string lead_name { get; set; }

        [JsonPropertyName("emailAddress")]
        public string email { get; set; }

        [JsonPropertyName("phoneNumber")]
        public string phone_number { get; set; }

        [JsonPropertyName("productInterest")]
        public string product_interest { get; set; }

        [JsonPropertyName("leadType")]
        public string lead_type { get; set; }

        [JsonPropertyName("leadSource")]
        public string lead_source { get; set; }

        [JsonPropertyName("leadRequirements")]
        public string requirement { get; set; }

        [JsonPropertyName("expectedBudget")]
        public string expected_budget { get; set ; }
        public int sales_rep_id { get; set; }
        public DateTime submission_date { get; set; } = DateTime.UtcNow;
        public string status { get; set; } = "New";
    }
}
