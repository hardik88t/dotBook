using dotBook.Models;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace dotBook.Models
{
    public class Sale
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        [Required]
        public DateTime SaleDate { get; set; }

        [Required]
        [ForeignKey(nameof(Customer.Id))]
        public int CustomerId { get; set; }

        [Required]
        public ICollection<SaleBook>? SaleBooks { get; set; }
        //public ICollection<SaleBook> SaleBooks { get; set; } = new List<SaleBook>();

        [Required]
        [Range(1, int.MaxValue)]
        public int TotalPrice { get; set; }
    }
}



//using System;
//using System.ComponentModel.DataAnnotations;
//using System.ComponentModel.DataAnnotations.Schema;

//namespace dotBook.Models
//{
//    public class Sale
//    {
//        [Key]
//        public int Id { get; set; }

//        [Required]
//        public DateTime SaleDate { get; set; }

//        [Required]
//        public string? CustomerName { get; set; }

//        [Required]
//        public int BookId { get; set; }
//        public Book? Book { get; set; }


//        [Required]
//        public int Quantity { get; set; }

//        //[Column(TypeName = "decimal(18,2)")]
//        [Required]
//        [Range(1, int.MaxValue)]
//        public decimal Price { get; set; }
//    }
//}

