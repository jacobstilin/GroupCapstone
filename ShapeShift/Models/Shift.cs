﻿using System;
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
        // Status: 1 = not taken, 2 = in process, 3 = taken

        [Display(Name = "Position Required")]
        public string position { get; set; }

        [Display(Name = "Start Time")]
        public DateTime? start { get; set; }

        [Display(Name = "End Time")]
        public DateTime? end { get; set; }

        [Display(Name = "Duration")]
        public double? duration { get; set; }

        [Display(Name = "Additional Information")]
        public string additionalInfo { get; set; }

        [ForeignKey("Location")]
        public int? LocationId { get; set; }
        public Location Location { get; set; }

        [ForeignKey("User")]
        public int UserId { get; set; }
        public AppUser User { get; set; }

    }
}