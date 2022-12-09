#nullable disable
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using WorkSchedule.BLL;
using WorkSchedule.Entities;
using WorkSchedule.ViewModels;

namespace WorkScheduleApp.Pages.Samples
{
    public class AddSkillModel : PageModel
    {
        private readonly EmployeeServices _employeeServices;
        public AddSkillModel(EmployeeServices employeeServices)
        {
            _employeeServices = employeeServices;
        }
        [BindProperty(SupportsGet = true)]
        public string FirstName { get; set; }

        [BindProperty(SupportsGet = true)]
        public string LastName { get; set; }

        [BindProperty(SupportsGet = true)]
        public string HomePhone { get; set; }

       /* [BindProperty(SupportsGet = true)]
        public List<EmployeeSkill> EmpSkills { get; set; }*/

        [BindProperty(SupportsGet = true)]
        public List<AddedSkillsInfo> AddedSkills { get; set; }

        [BindProperty(SupportsGet = true)]
        public int Level { get; set; }

        [BindProperty(SupportsGet = true)]
        public int YOE { get; set; }

        [BindProperty(SupportsGet = true)]
        public decimal HourlyWage { get; set; }

        [BindProperty(SupportsGet = true)]
        public bool SelectedSkill { get; set; }

        [BindProperty(SupportsGet =true)]
        public List<Reg_Employee> SkillsList { get; set; }

        public List<Reg_Employee> SelectedSkillsList { get; set; } = new();
        public List<Exception> Errors { get; set; } = new();
        public List<string> ErrorDetails { get; set; } = new();
        public bool HasError => !string.IsNullOrWhiteSpace(ErrorFeedback);

        public string ErrorFeedback { get; set; }
        public void OnGet()
        {
            SkillsList = _employeeServices.GetEmployeeSkills();
        }
        public void OnPostRegisterEmployee()
        {
            try
            {
                string firstname = FirstName;
                string lastname = LastName;
                string homephone = HomePhone;
                foreach (var item in SkillsList)
                {
                    if (item.SelctedSkill == true)
                    {

                        SelectedSkillsList.Add(new Reg_Employee()
                        {
                            SkillID = item.SkillID,
                            HourlyWage = item.HourlyWage,
                            Level = item.Level,
                            YearsOfExperience= item.YearsOfExperience
                        });

                        ErrorFeedback = "Add Successful";
                        
                    }
                }
                if (SelectedSkillsList.Count == 0)
                {
                    throw new Exception("Must add one skill");
                }
                if (Errors.Any())
                {
                    ErrorFeedback = Errors.First().ToString();
                    SkillsList = _employeeServices.GetEmployeeSkills();
                }
                _employeeServices.EmployeeService_Add_Employee(firstname, lastname, homephone, SelectedSkillsList);
                

            }
            catch (AggregateException ex)
            {
                ErrorFeedback = "Unable To Add this Skill to the Employee Check Your Parameters";
                foreach (var error in ex.InnerExceptions)
                {
                    ErrorDetails.Add(error.Message);
                }
            }
            catch (Exception ex)
            {
                ErrorFeedback = GetInnerException(ex).Message;
            }


        }
        public void OnPostClear()
        {
            FirstName = "";
            LastName = "";
            HomePhone = "";
            ModelState.Clear();
            SkillsList = _employeeServices.GetEmployeeSkills();
        }
        private Exception GetInnerException(Exception ex)
        {
            while (ex.InnerException != null)
                ex = ex.InnerException;
            return ex;
        }

    }
}
