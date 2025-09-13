using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace BookStoreMVC.Models
{
    public class Author
    {
        public int Id { get; set; }
        [Required(ErrorMessage = "You have to provide a valid full name")]
        [MinLength(5, ErrorMessage = "Full name mustn't be less than 5 characters.")]
        [MaxLength(50, ErrorMessage = "Full name mustn't exceed 50 characters.")]
        [DisplayName("Full Name")]
        public string Name { get; set; }

        public string Nationality { get; set; }

        [ValidateNever]
        public List<Book> Books { get; set; }
    }
}
