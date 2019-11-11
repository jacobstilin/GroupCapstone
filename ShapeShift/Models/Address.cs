using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace ShapeShift.Models
{
    public class Address
    {
        [Key]
        public int AddressId { get; set; }
    }
}