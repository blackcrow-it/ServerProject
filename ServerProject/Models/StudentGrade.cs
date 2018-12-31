using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ServerProject.Models
{
    using System.ComponentModel.DataAnnotations;

    public class StudentGrade
    {
        public StudentGrade()
        {
            this.IsActive = true;
            this.CreatedAt = DateTime.Now;
            this.UpdatedAt = DateTime.Now;
        }
        public string RollNumber { get; set; }
        public int GradeId { get; set; }
        [DataType(DataType.Date)]
        public DateTime JoinAt { get; set; }
        [DataType(DataType.Date)]
        public DateTime LeftAt { get; set; }
        public bool IsActive { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }
        public Students Students { get; set; }
        public Grades Grades { get; set; }

    }

}
