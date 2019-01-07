using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Newtonsoft.Json;
using ServerProject.Services;

namespace ServerProject.Models
{
    public class Students
    {
        public Students()
        {
            //this.RollNumber = HandleUid.UidRollNumberStudent(
            //    "Server=(localdb)\\mssqllocaldb;Database=ServerProjectContext-0be10c1e-d04b-4df2-b4ea-8ecd477f1bd4;Trusted_Connection=True;MultipleActiveResultSets=true");
            this.CreatedAt = DateTime.Now;
            this.UpdatedAt = DateTime.Now;
        }
        public string RollNumber { get; set; }
        public int AccountId { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }
        public Accounts Accounts { get; set; }
        [JsonIgnore]
        public List<StudentGrade> StudentGrades { get; set; }
        [JsonIgnore]
        public List<Marks> Markses { get; set; }
    }
}
