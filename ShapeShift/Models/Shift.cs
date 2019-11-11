using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace ShapeShift.Models
{
    public class Shift
    {
        [Key]
        public int ShiftId { get; set; }

        [Display(Name = "Shift Status")]
        public int status { get; set; }

        [Display(Name = "Position Required")]
        public int position { get; set; }

        [Display(Name = "Start Time")]
        public DateTime? start { get; set; }

        [Display(Name = "End Time")]
        public DateTime? end { get; set; }

        [Display(Name = "Duration")]
        public double? duration { get; set; }

        [Display(Name = "Additional Information")]
        public string additionalInfo { get; set; }



        [ForeignKey("User")]
        public int UserId { get; set; }
        public AppUser User { get; set; }

    }
}