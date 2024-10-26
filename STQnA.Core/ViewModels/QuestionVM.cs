using STQnA.Core.Models;

namespace STQnA.Core.ViewModels
{
    public class QuestionVM
    {
        public int QuestionId { get; set; }
        public string QuestionText { get; set; } = string.Empty;
        public DateTime CreatedDate { get; set; } = DateTime.Now;
        public bool IsAnswered { get; set; } = false;
        public string? StudentId { get; set; }
    }
}
