using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using STQnA.Core.Models;
using STQnA.Core.ViewModels;
using STQnA.Infrastructure.Common;
using STQnA.Service.Interfaces;

namespace STQnA.UI.Controllers
{
    public class AnswerController : Controller
    {
        #region Config
        private readonly IAnswerService _answerService; // Assuming a service layer for handling logic

        public AnswerController(IAnswerService answerService)
        {
            _answerService = answerService;
        }
        #endregion

        #region Index/List
        // GET: List of answers
        public async Task<IActionResult> Index()
        {
            var answers = await _answerService.GetAllAnswersAsync();
            return View(answers);
        }
        #endregion

        #region Details
        // GET: Answer details
        public async Task<IActionResult> Details(int id)
        {
            var answer = await _answerService.GetAnswerByIdAsync(id);
            if (answer == null)
            {
                return NotFound();
            }
            return View(answer);
        }
        #endregion

        #region Create
        // GET: 
        [Authorize(Roles = "Teacher")] // Only Teacher can answer question
        public IActionResult Create()
        {
            return View();
        }

        // POST: Save new answer
        [HttpPost]
        [ActionName("PostAnswer")]
        [ValidateAntiForgeryToken]
        [TeacherOrStudentAuthorization("id")] // Use the custom attribute // Only Teacher and who ask the question can answer question
        public async Task<IActionResult> Create(AnswerVM vm)
        {
            if (ModelState.IsValid)
            {
                // Save the answer to the database
                await _answerService.AddAnswerAsync(vm);
                return RedirectToAction("Details", "Question", new {id=vm.QuestionId});
            }
            return View(vm);
        }
        #endregion

        #region Edit
        // GET: Edit a answer 
        [Authorize(Roles = "Teacher")] // Only teachers can edit their own questions
        public async Task<IActionResult> Edit(int id)
        {
            var answer = await _answerService.GetAnswerByIdAsync(id);
            if (answer == null)
            {
                return NotFound();
            }

            return View(answer);
        }

        // POST: Save changes to the answer
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Teacher")] // Only Teachers can submit edits
        public async Task<IActionResult> Edit(int id, Answer model)
        {
            if (id != model.AnswerId)
            {
                return BadRequest();
            }

            if (ModelState.IsValid)
            {
                await _answerService.UpdateAnswerAsync(model);
                return RedirectToAction("Index");
            }
            return View(model);
        }
        #endregion

        #region Delete
        // POST: Delete a answer
        [ActionName("Delete")]
        [Authorize(Roles = "Teacher")] // Only teachers can delete their own questions
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var answer = await _answerService.GetAnswerByIdAsync(id);
            if (answer == null)
            {
                return NotFound();
            }

            await _answerService.DeleteAnswerAsync(id);
            return RedirectToAction("Index");
        }
        #endregion
    }
}
