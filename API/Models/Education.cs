﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace API.Models
{
    public class Education
    {
        [Key]
        public int Id { get; set; }
        [Required]
        public Degree Degree { get; set; }
        [Required]
        public string GPA { get; set; }
        [Required][ForeignKey("University")]
        public int University_Id { get; set; }

        public ICollection<Profiling> Profilings { get; set; }
        public University University { get; set; }

        
    }
    public enum Degree
    {
        D3,
        D4,
        S1,
        S2,
        S3
    }
}
