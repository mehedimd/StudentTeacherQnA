using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace STQnA.Core.Models
{
    public class Answer
    {
        public int AnswerId { get; set; }
        public string AnswerText { get; set; }
        public DateTime CreatedDate { get; set; }

        // Foreign Key to the Question this Answer belongs to
        public int QuestionId { get; set; }

        // Foreign Key to the Teacher who answered (ApplicationUser)
        public string TeacherId { get; set; } // This will hold IdentityUser Id (string)

        // Navigation Properties
        public Question Question { get; set; }
        public ApplicationUser Teacher { get; set; }
    }
}
