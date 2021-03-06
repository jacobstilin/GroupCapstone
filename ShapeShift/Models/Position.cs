﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace ShapeShift.Models
{
    public class Position
    {
        [Key]
        public int PositionId { get; set; }

        [ForeignKey("AppUser")]
        public int UserId { get; set; }
        public AppUser AppUser { get; set; }


        [Display(Name = "Title")]
        public string title { get; set; }





    }
}