using AutoMapper;
using STQnA.Core.Interfaces;
using STQnA.Core.Models;
using STQnA.Core.ViewModels;
using STQnA.Service.Interfaces;
namespace STQnA.Service.Services
{
    public class QuestionService : IQuestionService
    {
        public IQuestionRepository _repo;
        public IUserService _userService;
        public IMapper _iMapper;
        public QuestionService(IQuestionRepository repo, IUserService userService, IMapper mapper)
        {
            _repo = repo;
            _userService = userService;
            _iMapper = mapper;
        }

        public async Task<bool> AddQuestionAsync(QuestionVM vm)
        {
            if (vm != null)
            {
                var model = _iMapper.Map<Question>(vm);
                model.StudentId = _userService.GetCurrentUserId;

                await _repo.Add(model);

                var result = _repo.Save();

                if (result > 0)
                    return true;
                else
                    return false;
            }
            return false;
        }

        public async Task<bool> DeleteQuestionAsync(int id)
        {
            if (id > 0)
            {
                var model = await _repo.GetById(id);
                if (model != null)
                {
                    _repo.Delete(model);
                    var result = _repo.Save();

                    if (result > 0)
                        return true;
                    else
                        return false;
                }
            }
            return false;
        }

        public async Task<IEnumerable<Question>> GetAllQuestionsAsync()
        {
            var listOfModel = await _repo.GetAllQuestion();
            return listOfModel;
        }

        public async Task<Question> GetQuestionByIdAsync(int id)
        {
            if (id > 0)
            {
                var model = await _repo.GetById(id);
                if (model != null)
                {
                    return model;
                }
            }
            return new Question { };
        }

        public async Task<Question> GetQuestionByIdWithAnswerAsync(int id)
        {
            if (id > 0)
            {
                var model = await _repo.GetQuestionByIdWithAnswer(id);
                if (model != null)
                {
                    return model;
                }
            }
            return new Question { };
        }
        public async Task<bool> UpdateQuestionAsync(QuestionVM vm)
        {
            if (vm != null)
            {
                var questionFind = await _repo.GetById(vm.QuestionId);
                if (questionFind != null && !questionFind.IsAnswered)
                {
                    questionFind.QuestionId = vm.QuestionId;
                    questionFind.QuestionText = vm.QuestionText;
                    questionFind.StudentId = vm.StudentId ?? "";
                    questionFind.CreatedDate = vm.CreatedDate;

                    _repo.Update(questionFind);
                    var result = _repo.Save();

                    if (result > 0)
                        return true;
                    else
                        return false;
                }
            }
            return false;
        }
    }
}
