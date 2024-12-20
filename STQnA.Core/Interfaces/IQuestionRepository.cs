﻿using STQnA.Core.Interfaces.Generic;
using STQnA.Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace STQnA.Core.Interfaces
{
    public interface IQuestionRepository : IGenericRepository<Question>
    {
        Task<IEnumerable<Question>> GetAllQuestion();
        Task<Question> GetQuestionByIdWithAnswer(int id);
        int UpdateIsAnswered(int questionId);
    }
}
