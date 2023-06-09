﻿using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace dotBook.Models
{
    public class Customer
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        [Required]
        [MaxLength(100)]
        public string? Name { get; set; }

        [Required]
        public string? Address { get; set; }

        [Required]
        [RegularExpression(@"^([+]\d{2})?\d{10}$")]
        public string? Contact { get; set; }

        [Required]
        public DateTime JoinDate { get; set; }


    }
}
