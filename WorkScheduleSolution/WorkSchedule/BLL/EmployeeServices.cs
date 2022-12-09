#nullable disable
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WorkSchedule.DAL;
using WorkSchedule.ViewModels;
using WorkSchedule.Entities;

namespace WorkSchedule.BLL
{
    public class EmployeeServices
    {
        #region Constructor and Context Dependency
        private WorkScheduleContext _context;
        internal EmployeeServices(WorkScheduleContext context)
        {
            _context = context;
        }
        #endregion
        #region Services
        public EmployeeInfo GetDbVersion()
        {
            EmployeeInfo info = _context.Employees
                .Select(x => new EmployeeInfo()
                {
                   FirstName = x.FirstName,
                   LastName = x.LastName,
                   HomePhone= x.HomePhone,
                }).FirstOrDefault();
            return info;
        }
        #endregion

        #region Queries
        public void EmployeeService_Add_Employee(string firstName, string lastName, string homephone, List<Reg_Employee> employeeskillslist)
        {
            #region AddEmployee
            List<EmployeeSkill> employeeSkills = new List<EmployeeSkill>();
            List<Exception> errorlist;
            bool employeeExists = false;
            bool skillExists = false;           
            Employee employee = null;
            List<Reg_Employee>  empskills = GetEmployeeSkills();
            errorlist = new List<Exception>();
            employeeExists = _context.Employees
                           .Where(e => e.FirstName == firstName && e.LastName == lastName && e.HomePhone == homephone).Any();

            IEnumerable<Reg_Employee> addedskills = empskills
                                        .Where(x => x.SelctedSkill == true).ToList();


            if (!employeeExists)
            {
                employee = new Employee
                {
                    FirstName = firstName,
                    LastName = lastName,
                    HomePhone = homephone
                };
                if (string.IsNullOrWhiteSpace(firstName))
                {
                    errorlist.Add(new Exception("Firstname is Missing "));
                }
                if (string.IsNullOrWhiteSpace(lastName))
                {
                    errorlist.Add(new Exception("Lastname is Missing "));
                }
                if (string.IsNullOrWhiteSpace(homephone))
                {
                    errorlist.Add(new Exception("HomePhone is Missing "));
                }
                else
                {
                    _context.Employees.Add(employee);
                }
            }
            else
            {
                employee = _context.Employees.Where(e => e.FirstName == firstName && e.LastName == lastName && e.HomePhone == homephone).FirstOrDefault();
            }
            #endregion
            //IEnumerable <Reg_Employee> addskill = employeeskillslist.Where(e => e.SlectedSkill == true);
            for (int index = 0; index < employeeskillslist.Count(); index++)
            {
                int level = employeeskillslist[index].Level;
                decimal hourlyWage = employeeskillslist[index].HourlyWage;
                int? yearsOfExperience = employeeskillslist[index].YearsOfExperience;
                int skillID = employeeskillslist[index].SkillID;

                skillExists = _context.EmployeeSkills
                            .Where(x => x.SkillID == skillID && x.Employee.FirstName == firstName && x.Employee.LastName == lastName && x.Employee.HomePhone == homephone)
                            .Any();

                if (employeeskillslist.Count() == 0)
                {
                    errorlist.Add(new ArgumentException("Must Have At least One skill"));
                }

                if (skillExists)
                {
                    errorlist.Add(new ArgumentException("This Employee Already has this skill"));
                }
                else
                {
                    if (hourlyWage < 15 || hourlyWage > 100)
                    {
                        errorlist.Add(new ArgumentException("The Hourly Wage must fall between $15.00 and $100.00 inclusive."));
                    }

                    if (level < 1)
                    {
                        errorlist.Add(new Exception("The Level is Required and Must be a positive interger"));
                    }

                    if (yearsOfExperience < 1 || yearsOfExperience > 50)
                    {
                        if (yearsOfExperience == 0)
                        {

                        }
                        else
                        {
                            errorlist.Add(new Exception("The Years of Experience (if present) must be in the range of 1 to 50 inclusive"));
                        }

                    }
                    else
                    {
                        employeeSkills.Add(new EmployeeSkill
                        {
                            SkillID = employeeskillslist[index].SkillID,
                            HourlyWage = employeeskillslist[index].HourlyWage,
                            Level = employeeskillslist[index].Level,
                            YearsOfExperience = employeeskillslist[index].YearsOfExperience
                        });
                        employee.EmployeeSkill.Add(employeeSkills[index]);
                    }
                }
                if (errorlist.Count() > 0)
                {
                    throw new AggregateException("Unable to Add this Skill to the employee", errorlist);
                }
                else
                {
                    _context.SaveChanges();
                }
            }

        }
        #endregion
        #region Transaction Methods
        public List<Reg_Employee> GetEmployeeSkills ()
        {
            return _context.Skills
                .OrderBy(x => x.Description)
                .Select(x => new Reg_Employee()
                {
                    Description= x.Description,
                    SkillID= x.SkillID,
                    SelctedSkill = false
                })
                .ToList();
        }
        public List<Reg_Employee> RegEmployeeSkills(List<Reg_Employee> employeeskillslist)
        {
            return employeeskillslist
                .Where(x => x.SelctedSkill == true)
                .Select(x => new Reg_Employee()
                {
                    Description= x.Description,
                    YearsOfExperience= x.YearsOfExperience,
                    Level = x.Level,
                    HourlyWage= x.HourlyWage,
                })
                .ToList();
        }
        #endregion

       /* public List<Employee> GetEmployees(string phone)
        {
            IEnumerable<Employee> info = _context.Employees
                                            .Where(x => x.HomePhone == phone);
           return info.ToList();
        }*/
    }
}
