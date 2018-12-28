using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ServerProject.Models
{
    public class Marks
    {
        public Marks()
        {
            this.CreatedAt = DateTime.Now;
            this.UpdatedAt = DateTime.Now;
        }
        public long Id { get; set; }
        public float Value { get; set; }
        public MarkStatus Status { get; set; }
        public string RollNumber { get; set; }
        public int TypeMark { get; set; }
        public int CourseId { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }
        public Students Students { get; set; }
        public Types Types { get; set; }
        public Courses Courses { get; set; }
    }

    public enum MarkStatus
    {
        Null = 0,
        Fail = 1,
        Pass = 2,
        Great = 3,
        Excellent = 4
    }
}
