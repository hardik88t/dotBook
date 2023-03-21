using dotBook.Controllers;
using dotBook.Models;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;


namespace dotBook.Data
{
    public class DotDBcontext : DbContext
    {
        public DotDBcontext(DbContextOptions<DotDBcontext> options) : base(options)
        {

        }
        public DbSet<Book> Books { get; set; }
        public DbSet<Sale> Sales { get; set; }
        public DbSet<SaleBook> BooksBook { get; set; }
        public DbSet<Customer> Customers { get; set; }
        //public DbSet<StockOfBook> StockOfBooks { get; set;}


        //protected override void OnModelCreating(ModelBuilder modelBuilder)
        //{
        //    modelBuilder.Entity<StockOfBook>()
        //        .HasOne(s => s.Book)
        //        .WithMany()
        //        .HasForeignKey(s => s.BookId)
        //        .OnDelete(DeleteBehavior.SetNull);

        //    modelBuilder.Entity<Sale>()
        //        .HasMany(s => s.SaleBooks)
        //        .WithOne(sb => sb.Sale)
        //        .IsRequired();

        //    modelBuilder.Entity<SaleBook>()
        //        .HasOne(sb => sb.Book)
        //        .WithMany()
        //        .HasForeignKey(sb => sb.BookId)
        //        .OnDelete(DeleteBehavior.Restrict);

        //    modelBuilder.Entity<SaleBook>()
        //        .HasOne(sb => sb.Sale)
        //        .WithMany(s => s.SaleBooks)
        //        .HasForeignKey(sb => sb.SaleId)
        //        .OnDelete(DeleteBehavior.Cascade);
        //}

    }
}