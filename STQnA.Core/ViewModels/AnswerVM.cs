using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace STQnA.Core.ViewModels
{
    public class AnswerVM
    {
        public int AnswerId { get; set; }
        public string AnswerText { get; set; }
        public DateTime CreatedDate { get; set; } = DateTime.Now;

        // Foreign Key to the Question this Answer belongs to
        public int QuestionId { get; set; }

        // Foreign Key to the Teacher who answered (ApplicationUser)
        public string? TeacherId { get; set; } // This will hold IdentityUser Id (string)
    }
}
