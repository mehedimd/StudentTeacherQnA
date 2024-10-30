using AutoMapper;
using STQnA.Core.Models;
using STQnA.Core.ViewModels;

namespace STQnA.Core.Common
{
    public class AppAutoMapperProfile : Profile
    {
        public AppAutoMapperProfile()
        {
            CreateMap<QuestionVM, Question>();
            CreateMap<Question, QuestionVM>();
            CreateMap<Answer,AnswerVM>();
            CreateMap<AnswerVM, Answer>();
        }
    }
}
