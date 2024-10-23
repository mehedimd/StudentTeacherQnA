using STQnA.Core.Interfaces;
using STQnA.Core.Models;
using STQnA.Infrastructure.Repositories.Generic;

namespace STQnA.Infrastructure.Repositories
{
    public class QuestionRepository : GenericRepository<Question>, IQuestionRepository
    {
        public QuestionRepository(STQnAContext context) : base(context)
        {

        }
    }
}
