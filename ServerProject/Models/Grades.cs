using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ServerProject.Models
{
    using System.Collections;
    using System.ComponentModel.DataAnnotations;

    public class Grades 
    {
        public Grades()
        {
            this.CreatedAt = DateTime.Now;
            this.UpdatedAt = DateTime.Now;
        }
        public int Id { get; set; }
        [Required(ErrorMessage = "Tên lớp không được để trống")]
        [Display(Name = "Tên lớp")]
        public string Name { get; set; }
        [DataType(DataType.Date)]
        [Required(ErrorMessage = "Ngày bắt đầu không được để trống")]
        [Display(Name = "Ngày bắt đầu")]
        public DateTime StartTime { get; set; }
        [DataType(DataType.Date)]
        [Required(ErrorMessage = "Ngày kết thúc không được để trống")]
        [Display(Name = "Ngày kết thúc")]
        public DateTime EndTime { get; set; }
        [Display(Name = "Trạng thái")]
        public bool IsActive { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }
        public List<StudentGrade> StudentGrades { get; set; }
        public List<GradeCourse> GradeCourses { get; set; }
       
    }
}
