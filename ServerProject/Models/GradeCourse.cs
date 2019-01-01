using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace ServerProject.Models
{
    public class GradeCourse
    {
        public GradeCourse()
        {
            this.CreatedAt = DateTime.Now;
            this.UpdatedAt = DateTime.Now;
        }
        [Display(Name = "ID lớp")]
        public int GradeId { get; set; }
        [Display(Name = "ID môn học")]
        public int CourseId { get; set; }
        [DataType(DataType.Date)]
        [Display(Name = "Ngày bắt đầu")]
        public DateTime StartTime { get; set; }
        [DataType(DataType.Date)]
        [Display(Name = "Ngày kết thúc")]
        public DateTime EndTime { get; set; }
        [Display(Name = "Trạng thái")]
        public bool IsActive { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }
        public Grades Grades { get; set; }
        public Courses Courses { get; set; }
    }
}
