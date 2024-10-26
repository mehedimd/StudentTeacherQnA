using AutoMapper;
using STQnA.Core.Interfaces;
using STQnA.Core.Models;
using STQnA.Core.ViewModels;
using STQnA.Service.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace STQnA.Service.Services
{
    public class AnswerService : IAnswerService
    {
        public IAnswerRepository _repo;
        public IQuestionRepository _iQuestionRepo;
        public IUserService _userService;
        public IMapper _iMapper;
        public AnswerService(IAnswerRepository repo, IUserService userService, IQuestionRepository iQuestionRepo)
        {
            _repo = repo;
            _userService = userService;
            _iQuestionRepo = iQuestionRepo;
        }

        public async Task<bool> AddAnswerAsync(AnswerVM vm)
        {
            if (vm != null)
            {
                Answer model = new Answer()
                {
                    AnswerText = vm.AnswerText,
                    TeacherId = _userService.GetCurrentUserId,
                    QuestionId = vm.QuestionId,
                    CreatedDate = vm.CreatedDate
                };

                await _repo.Add(model);

                var result = _repo.Save();

                if (result > 0)
                {
                    var res = _iQuestionRepo.UpdateIsAnswered(vm.QuestionId);
                    if (res > 0)
                    {
                        return true;
                    }
                    else
                        return false;
                }
                else
                    return false;
            }
            return false;
        }

        public async Task<bool> DeleteAnswerAsync(int id)
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

        public async Task<IEnumerable<Answer>> GetAllAnswersAsync()
        {
            var listOfModel = await _repo.GetAll();
            return listOfModel;
        }

        public async Task<Answer> GetAnswerByIdAsync(int id)
        {
            if (id > 0)
            {
                var model = await _repo.GetById(id);
                if (model != null)
                {
                    return model;
                }
            }
            return new Answer { };
        }

        public async Task<bool> UpdateAnswerAsync(Answer model)
        {
            if (model != null)
            {
                var answerFind = await _repo.GetById(model.QuestionId);
                if (answerFind != null)
                {
                    answerFind.AnswerText = model.AnswerText;
                    answerFind.AnswerId = model.AnswerId;
                    answerFind.QuestionId = model.QuestionId;
                    answerFind.TeacherId = model.TeacherId;
                    answerFind.CreatedDate = DateTime.Now;

                    _repo.Update(answerFind);
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
