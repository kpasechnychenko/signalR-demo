using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel.DataAnnotations;

namespace Model.Model
{
    public class ViewEvent
    {
        public int Id { get; set; }
        public int GroupId { get; set; }
        [Required]
        [StringLength(20, MinimumLength = 5)]
        public string Name { get; set; }
        [StringLength(50,MinimumLength=5)]
        public string Description { get; set; }
        [Required]
        [Range(-90, 90)]
        public double Lat { get; set; }
        [Required]
        [Range(-180, 180)]
        public double Lon { get; set; }
        [Required]
        [Range(0, 50,ErrorMessage="Please enter Radius less than 50")]
        public double Radius { get; set; }
    }
}
