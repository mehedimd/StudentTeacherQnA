using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using STQnA.Core.Models;
using STQnA.Core.ViewModels;
using STQnA.Service.Interfaces;

namespace STQnA.UI.Controllers;

public class QuestionController : Controller
{
    #region Config
    private readonly IQuestionService _questionService; // Assuming a service layer for handling logic
    private readonly IMapper _iMapper;

    public QuestionController(IQuestionService questionService)
    {
        _questionService = questionService;
    }
    #endregion

    #region Index/List
    // GET: List of questions (for students and teachers)
    public async Task<IActionResult> Index()
    {
        var questions = await _questionService.GetAllQuestionsAsync();
        return View(questions);
    }
    #endregion

    #region Details
    // GET: Question details
    public async Task<IActionResult> Details(int id)
    {
        var question = await _questionService.GetQuestionByIdWithAnswerAsync(id);
        if (question == null)
        {
            return NotFound();
        }
        return View(question);
    }
    #endregion

    #region Create
    // GET: Ask a new question (for students)
    [Authorize(Roles = "Student")] // Only students can ask questions
    public IActionResult Create()
    {
        return View();
    }

    // POST: Save new question
    [HttpPost]
    [ValidateAntiForgeryToken]
    [Authorize(Roles = "Student")] // Only students can submit questions
    public async Task<IActionResult> Create(QuestionVM vm)
    {
        if (ModelState.IsValid)
        {
            // Save the question to the database
            await _questionService.AddQuestionAsync(vm);
            return RedirectToAction("Index");
        }
        return View(vm);
    }
    #endregion

    #region Edit
    // GET: Edit a question (only if the question has not been answered yet)
    [Authorize(Roles = "Student")] // Only students can edit their own questions
    public async Task<IActionResult> Edit(int id)
    {
        var question = await _questionService.GetQuestionByIdAsync(id);
        if (question == null || question.IsAnswered)
        {
            return NotFound(); // Prevent editing questions that already have answers
        }
        QuestionVM model = new QuestionVM()
        {
            QuestionId = question.QuestionId,
            StudentId = question.StudentId,
            QuestionText = question.QuestionText,
            CreatedDate = question.CreatedDate
        };

        return View(model);
    }

    // POST: Save changes to the question
    [HttpPost]
    [ValidateAntiForgeryToken]
    [Authorize(Roles = "Student")] // Only students can submit edits
    public async Task<IActionResult> Edit(int id, QuestionVM vm)
    {
        if (id != vm.QuestionId)
        {
            return BadRequest();
        }

        if (ModelState.IsValid)
        {
            _questionService.UpdateQuestionAsync(vm);
            return RedirectToAction(nameof(Index));
        }
        return View(vm);
    }
    #endregion

    #region Delete
    // POST: Delete a question (only if there is no answer yet)
    [ActionName("Delete")]
    [Authorize(Roles = "Student")] // Only students can delete their own questions
    public async Task<IActionResult> DeleteConfirmed(int id)
    {
        var question = await _questionService.GetQuestionByIdAsync(id);
        if (question == null || question.IsAnswered)
        {
            return NotFound(); // Prevent deletion of questions that already have answers
        }

        _questionService.DeleteQuestionAsync(id);
        return RedirectToAction("Index");
    }
    #endregion
}
