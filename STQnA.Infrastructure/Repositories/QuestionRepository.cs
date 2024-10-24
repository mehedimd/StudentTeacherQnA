using Microsoft.EntityFrameworkCore;
using STQnA.Core.Interfaces;
using STQnA.Core.Models;
using STQnA.Infrastructure.Repositories.Generic;

namespace STQnA.Infrastructure.Repositories
{
    public class QuestionRepository : GenericRepository<Question>, IQuestionRepository
    {
        protected readonly STQnAContext db;
        public QuestionRepository(STQnAContext context) : base(context)
        {
            this.db = context;
        }
        public async Task<IEnumerable<Question>> GetAllQuestion()
        {
            return await db.Set<Question>().Include(s=>s.Student).ToListAsync();
        }

        public async Task<Question> GetQuestionByIdWithAnswer(int id)
        {
            return await db.Set<Question>().Include(s=>s.Student).Include(a => a.Answers).ThenInclude(t=>t.Teacher).FirstOrDefaultAsync();
        }
    }
}
