using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace STQnA.Core.ViewModels
{
    public class QuestionVM
    {
        public int QuestionId { get; set; }
        public string QuestionText { get; set; }
        public DateTime? CreatedDate { get; set; }
        public bool? IsAnswered { get; set; }
    }
}
