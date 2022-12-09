#nullable disable
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WorkSchedule.ViewModels
{
    public class AddedSkillsInfo
    {
        public int SkillID { get; set; }
        public int Level { get; set; }
        public int HourlyWage { get; set; }
        public int YearsOfExperience { get; set; }
        public string Description { get; set; }
    }
}
