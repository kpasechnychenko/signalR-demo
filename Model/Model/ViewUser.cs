using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel.DataAnnotations;

namespace Model.Model
{
    public class ViewUser
    {
        public int Id { get; set; }
        [Required]
        [StringLength(20, MinimumLength = 3)]
        public string UserName { get; set; }
        [Required]
        [RegularExpression("^([\\w\\.\\-]+)@([\\w\\-]+)((\\.(\\w){2,3})+)$", ErrorMessage = "Please enter valid Email")]
        public string Email { get; set; }
        [Required]
        [StringLength(20, MinimumLength = 3, ErrorMessage = "Please enter valid First Name")]
        public string FirstName { get; set; }
        [Required]
        [StringLength(20, MinimumLength = 3, ErrorMessage = "Please enter valid Last Name")]
        public string LastName { get; set; }
        public bool IsActive { get; set; }
    }
}
