using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace ShapeShift.Models
{
    public class Availability
    {
        [Key]
        public int Id { get; set; }

        [ForeignKey("AppUser")]
        public int UserId { get; set; }
        public AppUser AppUser { get; set; }
        public string PhoneNumber { get; set; }

        [Display(Name = "Weekday")]
        public string weekday { get; set; }

        [Display(Name = "Start")]
        public string start { get; set; }

        [Display(Name = "End")]
        public string end { get; set; }

        }
    }
