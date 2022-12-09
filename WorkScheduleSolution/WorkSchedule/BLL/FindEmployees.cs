#nullable disable
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WorkSchedule.DAL;
using WorkSchedule.ViewModels;

namespace WorkSchedule.BLL
{
    public class FindEmployees
    {
        private WorkScheduleContext _context;
        internal FindEmployees(WorkScheduleContext context)
        {
            _context = context;
        }

        public EmployeeInfo FindEmployee(string homephone)
        {
            return _context.Employees.Where(x => x.HomePhone == homephone)
                .Select(x => new EmployeeInfo()
                {
                    FirstName = x.FirstName,
                    LastName = x.LastName,
                    EmployeeID  = x.EmployeeID
                }).FirstOrDefault();
        }
        public List<Reg_Employee> GetEmployeeSkills (int employeeId)
        {
            return _context.EmployeeSkills.Where(x => x.EmployeeID == employeeId)
                .Select(x => new Reg_Employee()
                {
                    HourlyWage = (int)x.HourlyWage,
                    Level = x.Level,
                    YearsOfExperience = x.YearsOfExperience,
                    Description = x.Skill.Description,
                    SkillID= x.SkillID
                }).ToList();
        }
    }
}
