using STQnA.Core.Interfaces;
using STQnA.Core.Models;
using STQnA.Infrastructure.Repositories.Generic;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace STQnA.Infrastructure.Repositories
{
    public class AnswerRepository : GenericRepository<Answer>, IAnswerRepository
    {
        public AnswerRepository(STQnAContext context) : base(context)
        {
        }
    }
}
