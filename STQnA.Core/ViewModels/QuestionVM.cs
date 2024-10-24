﻿using STQnA.Core.Models;

namespace STQnA.Core.ViewModels
{
    public class QuestionVM
    {
        public int QuestionId { get; set; }
        public string QuestionText { get; set; }
        public DateTime CreatedDate { get; set; } = DateTime.Now;
        public bool IsAnswered { get; set; } = false;
        public string? StudentId { get; set; }
        public ApplicationUser? Student { get; set; }
        public ICollection<Answer>? Answers { get; set; }
    }
}
