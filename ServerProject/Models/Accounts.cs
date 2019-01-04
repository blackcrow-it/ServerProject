using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace ServerProject.Models
{
    using Newtonsoft.Json;
    using System.ComponentModel.DataAnnotations;

    public class Accounts
    {
        public Accounts()
        {
            this.CreatedAt = DateTime.Now;
            this.UpdatedAt = DateTime.Now;
            this.Role = 0;
        }
        public int Id { get; set; }
        [Required(ErrorMessage = "Password không được để trống")]
        public string UserName { get; set; }
        public string Password { get; set; }
        [NotMapped]
        [Compare("Password")]
        public string ConfirmPassword { get; set; }
        public string Salt { get; set; }
        public int Role { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }
        public Informations Informations { get; set; }
        public Students Students { get; set; }
    }
}
