using ShapeShift.Controllers;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace ShapeShift.Models
{
    public class AppUser
    {
        [Key]
        public int UserId { get; set; }

        [Display(Name = "First Name")]
        public string firstName { get; set; }

        [Display(Name = "Middle Name")]
        public string middleName { get; set; }

        [Display(Name = "Last Name")]
        public string lastName { get; set; }

        [Display(Name = "Positions")]
        public ICollection<Position> Positions { get; set; }

        [Display(Name = "Availability")]
        public ICollection<Availability> Availability { get; set; }

        [Display(Name = "Location")]
        public ICollection<Availability> Location { get; set; }

        [ForeignKey("ApplicationUser")]
        public string ApplicationId { get; set; }
        public ApplicationUser ApplicationUser { get; set; }

        [ForeignKey("Organization")]
        public int OrganizationId { get; set; }
        public Organization Organization { get; set; }
        
        [Display(Name = "Phone Number")]
        public string phoneNumber { get; set; }


        

    }
}