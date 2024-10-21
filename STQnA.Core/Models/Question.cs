using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace STQnA.Core.Models
{
    public class Question
    {
        public int QuestionId { get; set; }
        public string QuestionText { get; set; }
        public DateTime CreatedDate { get; set; }
        public bool IsAnswered { get; set; }

        // Foreign Key to the Student who asked the question (ApplicationUser)
        public string StudentId { get; set; } // This will hold IdentityUser Id (string)

        // Navigation Properties
        public ApplicationUser Student { get; set; }
        public ICollection<Answer> Answers { get; set; }
    }
}
