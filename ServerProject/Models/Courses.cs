using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ServerProject.Models
{
    using System.ComponentModel.DataAnnotations;

    public class Courses
    {
        public Courses()
        {
            this.CreatedAt = DateTime.Now;
            this.UpdatedAt = DateTime.Now;
        }
        public int Id { get; set; }
        [Required(ErrorMessage = "Tên môn không được để trống")]
        [Display(Name = "Tên môn")]
        public string Name { get; set; }
        [Display(Name = "Mô tả")]
        public string Description { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }
        public List<Marks> Markses { get; set; }
        public List<GradeCourse> GradeCourses { get; set; }

    }
}
