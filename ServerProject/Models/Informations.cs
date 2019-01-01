using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace ServerProject.Models
{
    public class Informations
    {
        public Informations()
        {
            this.CreatedAt = DateTime.Now;
            this.UpdatedAt = DateTime.Now;
        }
        public int AccountId { get; set; }
        [Required(ErrorMessage = "Họ không được để trống")]
        [Display(Name = "Họ")]
        public string FirstName { get; set; }
        [Required(ErrorMessage = "tên đêmk không được để trống")]
        [Display(Name = "Tên đệm")]
        public string MiddleName { get; set; }
        [Required(ErrorMessage = "tên không được để trống")]
        [Display(Name = "Tên")]
        public string LastName { get; set; }
        [Display(Name = "Giới tính")]
        public TypeGender Gender { get; set; }
        [Required(ErrorMessage = "ngày sinh không được để trống")]
        [DataType(DataType.Date)]
        [Display(Name = "Sinh nhật")]
        public DateTime Birthday { get; set; }
        [Required(ErrorMessage = "Email không được để trống")]
        public string Email { get; set; }
        [Required(ErrorMessage = "địa chỉ không được để trống")]
        [Display(Name = "Địa chỉ")]
        public string Address { get; set; }
        [Required(ErrorMessage = "số điện thoại không được để trống")]
        [Display(Name = "Số điện thoại")]
        public string Phone { get; set; }
        [Display(Name = "ảnh đại diện")]
        public string Avatar { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }
        public Accounts Accounts { get; set; }
    }

    public enum TypeGender
    {
        Male = 1,
        Female = 0,
        Other = 2
    }
}
