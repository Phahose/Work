#nullable disable
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using WorkSchedule.BLL;
using WorkSchedule.Entities;
using WorkSchedule.ViewModels;

namespace WorkScheduleApp.Pages
{
    public class SearchEmployeeModel : PageModel
    {
        private readonly EmployeeServices _employeeServices;
        private readonly FindEmployees _findemployeeservices;
        public SearchEmployeeModel(EmployeeServices employeeServices, FindEmployees findEmployeeesservices) 
        {
            _employeeServices = employeeServices;
            _findemployeeservices = findEmployeeesservices;
        }

        [TempData]
        public string Feedback { get; set; }

        [BindProperty(SupportsGet = true)]
        public string PhoneNumber { get; set; }
        public string SkillLevelNovice { get; set; } = "Novice";
        public string SkillLevelProficient { get; set; } = "Proficient";
        public string SkillLevelExpert { get; set; } = "Expert";
        public EmployeeInfo EmployeeInfo { get; set; } = new();
        public List<Reg_Employee> EmployeeSkillsInfo { get; set; } = new();
       /* public List<WorkSchedule.Entities.Employee> WorkScheduleInfo { get; set; }*/
        public void OnGet()
        {
            EmployeeInfo = _findemployeeservices.FindEmployee(PhoneNumber);       
        }

        public void OnPostFetch()
        {
            if (string.IsNullOrWhiteSpace(PhoneNumber))
            {
                Feedback = "Employee Phone Number is Empty";
            }
            EmployeeInfo = _findemployeeservices.FindEmployee(PhoneNumber);
            EmployeeSkillsInfo = _findemployeeservices.GetEmployeeSkills(EmployeeInfo.EmployeeID);
           
        }
        public void OnPostClear()
        {
            PhoneNumber = "";
            ModelState.Clear();
        }
    }
}
