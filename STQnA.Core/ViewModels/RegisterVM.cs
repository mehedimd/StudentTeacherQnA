using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace STQnA.Core.ViewModels
{
    public class RegisterVM
    {
        [Required]
        [EmailAddress]
        public string Email { get; set; }

        [Required]
        [DataType(DataType.Password)]
        public string Password { get; set; }

        [Required]
        [Compare("Password", ErrorMessage = "Passwords do not match.")]
        [DataType(DataType.Password)]
        public string ConfirmPassword { get; set; }

        [Required(ErrorMessage = "Please choose a Role.")]
        public string Role { get; set; } // Either "Student" or "Teacher"

        // Fields specific to students
        public string? Name { get; set; }
        public string? InstituteName { get; set; }
        public string? InstituteIDCardNumber { get; set; }
    }
}
