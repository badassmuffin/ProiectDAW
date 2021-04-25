using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;
using System.Web.Mvc;
namespace ProiectDAW.Models
{
    public class Curator
    {
        [Key]
        public int CuratorId { get; set; }
        public string Name { get; set; }
        // many-to-one relationship
        public virtual ICollection<Painting> Paintings { get; set; }
        // one-to one-relationship
        [Required]
        //public virtual ContactInfo ContactInfo { get; set; }
    }
}

