using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using System.ComponentModel.DataAnnotations;

namespace BookStoreMVC.Models
{
    public class Reader
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "Reader name is required")]
        [MinLength(5), MaxLength(50)]
        public string Name { get; set; }

        [EmailAddress(ErrorMessage = "Invalid email")]
        public string Email { get; set; }

        [Phone(ErrorMessage = "Invalid phone number")]
        public string PhoneNumber { get; set; }

        [ValidateNever]
        public List<Book> Books { get; set; }
    }
}
