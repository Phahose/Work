#nullable disable
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WorkSchedule.ViewModels
{
    public class Reg_Employee
    {
        public bool SelctedSkill { get; set; }
        public int SkillID { get; set; }
        public int Level { get; set; }
        public decimal HourlyWage { get; set; }
        public int? YearsOfExperience { get; set; }
        public string Description { get; set; }
    }
}
