using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;

namespace BookStoreMVC.Models
{
    public class Book
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "Title is required!")]
        public string Title { get; set; }

        [DataType(DataType.Date)]
        [Display(Name = "Published On")]
        public DateTime PublishDate { get; set; }

        [Range(1, 1000, ErrorMessage = "Price must be between 1 and 1000")]
        public decimal Price { get; set; }
        public int? ReaderId { get; set; } // optional reader
        [ValidateNever]
        [DeleteBehavior(DeleteBehavior.NoAction)]
        public Reader Reader { get; set; }


        public int AuthorId { get; set; }

        [ValidateNever]
        [DeleteBehavior(DeleteBehavior.NoAction)]
        public Author Author { get; set; }
    }
}
