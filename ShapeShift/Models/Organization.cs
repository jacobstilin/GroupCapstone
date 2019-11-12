using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace ShapeShift.Models
{
    public class Organization
    {
        [Key]
        public int OrganizationId { get; set; }

        [Display(Name = "Organization Name")]
        public string organizationName { get; set; }





    }
}