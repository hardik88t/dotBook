using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace dotBook.Models.NewModels
{
    public class StockOfBook
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        [Required]
        [ForeignKey(nameof(Book.Id))]
        public int BookId { get; set; }
        //public Book? Book { get; set; }

        [Required]
        public int Quantity { get; set; }
    }
}
