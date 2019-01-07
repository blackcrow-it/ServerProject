using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ServerProject.Models
{
    using System.ComponentModel.DataAnnotations;

    public class Marks
    {
        public static int MAX_THEORY = 10;
        public static int MAX_PRATICE = 15;
        public static int MAX_ASSIGNMENT = 10;

        public Marks()
        {
            this.Value = 0;
            this.Type = MarkType.THEORY;
            this.CalculateMarkStatus();
            this.CreatedAt = DateTime.Now;
            this.CreatedAt = DateTime.Now;
        }

        public Marks(MarkType type, int value )
        {
           
            this.Value = value;
            this.Type = type;
            this.CalculateMarkStatus();
            this.CreatedAt = DateTime.Now;
            this.UpdatedAt = DateTime.Now;
        }

        public void CalculateMarkStatus()
        {
            int maximum = 0;
            if (this.Type == MarkType.THEORY)
            {
                maximum = 10;
            }
            else if (this.Type == MarkType.PRATICE)
            {
                maximum = 15;;
            }
            else if (this.Type == MarkType.ASSIGNMENT)
            {
                maximum = 10;
            }
            this.Status = this.Value >= (40 * maximum / 100) ? MarkStatus.PASS : MarkStatus.FAIL;
        }
        public long Id { get; set; }
        [Display(Name = "Loại điểm")]
        public MarkType Type { get; set; }
        [Display(Name = "điểm")]
        public int Value { get; set; }
        public string RollNumber { get; set; }
        public int TypeMark { get; set; }
        [Display(Name = "ID môn học")]
        public int CourseId { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }
        public Students Students { get; set; }
        //public Types Types { get; set; }
        public Courses Courses { get; set; }
        public MarkStatus Status { get; set; }
    }

    public enum MarkType
    {
        THEORY = 1,
        PRATICE = 2,
        ASSIGNMENT = 3
    }
    public enum MarkStatus
    {
        PASS = 1,
        FAIL = 0
    }
}