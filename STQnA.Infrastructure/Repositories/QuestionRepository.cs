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
            return await db.Set<Question>().Include(s=>s.Student).OrderByDescending(q=>q.CreatedDate).ToListAsync();
        }

        public async Task<Question> GetQuestionByIdWithAnswer(int id)
        {
            var res =  await db.Set<Question>().Where(q=>q.QuestionId == id).Include(s=>s.Student).Include(a => a.Answers).ThenInclude(t=>t.Teacher).FirstOrDefaultAsync();
            if(res != null)
            {
                return res;
            }
            return Activator.CreateInstance<Question>();
        }
        public int UpdateIsAnswered(int questionId)
        {
           return db.Questions
                .Where(q=>q.QuestionId == questionId)
                .ExecuteUpdate(q=>q.SetProperty(
                    a=>a.IsAnswered,
                    a=> true
                ));
        }
    }
}
