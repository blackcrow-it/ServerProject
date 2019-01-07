using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ServerProject.Models
{
    using System.ComponentModel.DataAnnotations;

    public class LoginData
    {
       
            [Required]
            public string Username { get; set; }

            [Required, DataType(DataType.Password)]
            public string Password { get; set; }

            public bool RememberMe { get; set; }
        
    }
}
