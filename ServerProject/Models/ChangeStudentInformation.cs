using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ServerProject.Models
{
    public class ChangeStudentInformation
    {
        public string Email { get; set; }
        public string newEmail { get; set; }
        public string Address { get; set; }
        public string newAddress { get; set; }
        public string Phone { get; set; }
        public string newPhone { get; set; }
        public string Avatar { get; set; }
        public string newAvatar { get; set; }
    }
}
