﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace ShapeShift.Models
{
    public class TextMessage
    {
        [Key]
        public string Position { get; set; }
      
        public string BodyOfMessage { get; set; }
        public int id { get; set; }

    }
}