using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace ShapeShift.Models
{
    public class TextMessage
    {
        [Key]
        public string Postion { get; set; }
        public string NumberToSendTo { get; set; }
        public string BodyOfMessage { get; set; }
        public int id { get; set; }
        public List<AppUser> ListOfPeopleToSendTo { get; set; }
    }
}