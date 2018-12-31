using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ServerProject.Models
{
    public class Marks
    {
        public static int MAX_THEORY = 10;
        public static int MAX_PRATICE = 15;
        private static int MAX_ASSIGNMENT = 10;

        public Marks()
        {
            this.Value = 0;
            this.Type = MarkType.THEORY;
            this.calculateMarkStatus();
            this.CreatedAt = DateTime.Now;
            this.CreatedAt = DateTime.Now;
        }

        public Marks(MarkType type, int value )
        {
           
            this.Value = value;
            this.Type = type;
            this.calculateMarkStatus();
            this.CreatedAt = DateTime.Now;
            this.UpdatedAt = DateTime.Now;
        }

        public void calculateMarkStatus()
        {
            int maximum = 0;
            if (this.Type == MarkType.THEORY)
            {
                maximum = MAX_THEORY;
            }
            else if (this.Type == MarkType.PRATICE)
            {
                maximum = MAX_PRATICE;
            }
            else if (this.Type == MarkType.ASSIGNMENT)
            {
                maximum = MAX_ASSIGNMENT;
            }
            this.Status = (this.Value / maximum) * 100 >= 40 ? MarkStatus.PASS : MarkStatus.FAIL;
        }
        public long Id { get; set; }
        public MarkType Type { get; set; }
        public int Value { get; set; }
       
        public string RollNumber { get; set; }
        public int TypeMark { get; set; }
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