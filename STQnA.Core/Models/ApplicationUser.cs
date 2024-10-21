using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace STQnA.Core.Models
{
    public class ApplicationUser : IdentityUser
    {
        // For student users
        public string? Name { get; set; }
        public string? InstituteName { get; set; }
        public string? InstituteIDCardNumber { get; set; }
        public string? Password { get; set; }

        // Role: Either 'Student' or 'Teacher'
        public string? Role { get; set; }

        // Navigation properties for Questions and Answers
        public ICollection<Question> Questions { get; set; }
        public ICollection<Answer> Answers { get; set; }
    }
}
