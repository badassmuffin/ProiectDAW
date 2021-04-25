using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace ProiectDAW.Models
{
    public class Technique
    {
        [Key]
        public int TechniqueId { get; set; }
        public string Name { get; set; }
        // many-to-many relationship
        public virtual ICollection<Painting> paintings { get; set; }
    }
}