using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel.DataAnnotations;

namespace Model.Model
{
    public class ViewGroup
    {
        public int Id { get; set; }
        public int UserId { get; set; }
        public string UserName { get; set; }
        [StringLength(20, MinimumLength = 5)]
        [Required]
        public string Name { get; set; }
        [StringLength(50, MinimumLength = 5)]
        public string Description { get; set; }
    }
}
