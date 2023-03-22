using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace dotBook.Models
{
    public class Book
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        [Required]
        [MaxLength(100)]
        public string? Title { get; set; }

        [Required]
        public string? Author { get; set; }

        [Required]
        [Range(1, int.MaxValue)]
        public int Price { get; set; }

        [Required]
        [Range(1, int.MaxValue)]
        public int Stock { get; set; }

        [Required]
        public string? Description { get; set; }

        [Required]
        public string? Publisher { get; set; }

        [Required]
        public string? ISBN { get; set; }

        [Required]
        public DateTime ArrivalDate { get; set; }

    }
}
