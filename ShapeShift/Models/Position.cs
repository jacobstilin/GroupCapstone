using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace ShapeShift.Models
{
    public class Position
    {
        [Key]
        public int PositionId { get; set; }

        [Display(Name = "Title")]
        public string title { get; set; }





    }
}