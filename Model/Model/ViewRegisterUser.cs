using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel.DataAnnotations;

namespace Model.Model
{
    public class ViewRegisterUser : ViewUser
    {
        [Required]
        [StringLength(30, MinimumLength = 1)]
        public string Password { get; set; }
    }
}
