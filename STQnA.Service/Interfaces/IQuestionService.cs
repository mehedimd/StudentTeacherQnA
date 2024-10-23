using STQnA.Core.Models;
using STQnA.Core.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace STQnA.Service.Interfaces
{
    public interface IQuestionService
    {
        Task<IEnumerable<Question>> GetAllQuestionsAsync();
        Task<Question> GetQuestionByIdAsync(int id);
        Task<bool> AddQuestionAsync(QuestionVM model);
        Task<bool> UpdateQuestionAsync(Question model);
        Task<bool> DeleteQuestionAsync(int id);
    }
}
