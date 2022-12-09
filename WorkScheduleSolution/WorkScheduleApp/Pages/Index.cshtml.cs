#nullable disable
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.IdentityModel.Tokens;
using WorkSchedule.BLL;
using WorkSchedule.ViewModels;

namespace WorkScheduleApp.Pages
{
    public class IndexModel : PageModel
    {
        #region Private veriables and Di Constructor
        private readonly ILogger<IndexModel> _logger;
        private readonly EmployeeServices _employeeServices;
        public IndexModel(ILogger<IndexModel> logger, EmployeeServices employeeServices)
        {
            _employeeServices = employeeServices;
            _logger = logger;
        }
        #endregion

        #region Feedback and ErrorHandeling
        [TempData]
        public string Feedback { get; set; }
        public bool HasFeedback => string.IsNullOrWhiteSpace(Feedback);

        #endregion
        public void OnGet()
        {
            EmployeeInfo info = _employeeServices.GetDbVersion();
            if (info == null)
            {
                Feedback = "Unknown";
            }
            else
            {
                Feedback = $"Version: {info.FirstName}.{info.LastName}.{info.HomePhone}";        
            }

        }
    }
}