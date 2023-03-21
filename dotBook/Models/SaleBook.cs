using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace dotBook.Models
{
    public class SaleBook
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        [Required]
        [ForeignKey(nameof(Sale.Id))]
        public int SaleId { get; set; }
        //public Sale? Sale { get; set; }

        [Required]
        [ForeignKey(nameof(Book.Id))]
        public int BookId { get; set; }
        //public Book? Book { get; set; }

        [Required]
        [Range(1, int.MaxValue)]
        public int Quantity { get; set; }

        [Required]
        [Range(1, int.MaxValue)]
        public int Price { get; set; }
    }
}
