using STQnA.Core.Models;
using STQnA.Core.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace STQnA.Service.Interfaces
{
    public interface IAnswerService
    {
        Task<IEnumerable<Answer>> GetAllAnswersAsync();
        Task<Answer> GetAnswerByIdAsync(int id);
        Task<bool> AddAnswerAsync(AnswerVM model);
        Task<bool> UpdateAnswerAsync(Answer model);
        Task<bool> DeleteAnswerAsync(int id);
    }
}
