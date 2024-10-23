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
        public IMapper _iMapper;
        public QuestionService(IQuestionRepository repo)
        {
            _repo = repo;
        }

        public async Task<bool> AddQuestionAsync(QuestionVM vm)
        {
            if (vm != null)
            {
                var model = _iMapper.Map<Question>(vm);

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
            var listOfModel = await _repo.GetAll();
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

        public async Task<bool> UpdateQuestionAsync(Question model)
        {
            if (model != null)
            {
                var questionFind = await _repo.GetById(model.QuestionId);
                if (questionFind != null)
                {
                    questionFind.QuestionText = model.QuestionText;
                    questionFind.CreatedDate = DateTime.Now;

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
