using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Polly;
using QuickApp.Core.Infrastructure;
using QuickApp.Core.Models.SalesLead;
using QuickApp.Core.Services.Account;
using QuickApp.Server.Attributes;
using QuickApp.Server.Services;

namespace QuickApp.Server.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class SalesLeadController: Controller
    {
        private readonly ILogger<SalesLeadController> _logger;
        private readonly ApplicationDbContext _Applicationcontext;
        public SalesLeadController(ILogger<SalesLeadController> logger, ApplicationDbContext Applicationcontext)
        {
            _logger = logger;
            _Applicationcontext = Applicationcontext;
        }

        [HttpPost("create-lead")]
        public async Task<IActionResult> CreateLeadAsync([FromBody] LeadModel lead)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            lead.submission_date = DateTime.UtcNow;
            lead.status = "New";

            _Applicationcontext.Leads.Add(lead);
            await _Applicationcontext.SaveChangesAsync();

            return Ok(new { message = "Lead saved", id = lead.lead_id });
        }
    }
}
