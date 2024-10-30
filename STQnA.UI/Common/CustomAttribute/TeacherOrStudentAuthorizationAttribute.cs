using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using STQnA.Core.Interfaces;
using System.Security.Claims;

namespace STQnA.UI.Common.CustomAttribute
{
    public class TeacherOrStudentAuthorizationAttribute : AuthorizeAttribute, IAsyncAuthorizationFilter
    {
        private readonly string id;

        public TeacherOrStudentAuthorizationAttribute(string id)
        {
            this.id = id;
        }

        public async Task OnAuthorizationAsync(AuthorizationFilterContext context)
        {
            var user = context.HttpContext.User;

            if (user == null || (!user.Identity?.IsAuthenticated ?? true))
            {
                context.Result = new UnauthorizedResult();
                return;
            }

            // Retrieve IQuestionRepository via IServiceProvider
            var _repo = context.HttpContext.RequestServices.GetService<IQuestionRepository>();

            // Check if the user is in the "Teacher" role
            if (user.IsInRole("Teacher"))
            {
                return; // Access granted
            }

            // Get the QuestionId from the route data
            var questionId = context.HttpContext.Request.Form["QuestionId"];
            if (int.TryParse(questionId, out var id))
            {
                // Assuming you have a method to check if the user is the student who asked the question
                bool isStudent = await IsUserStudentAsync(user, id, _repo);
                if (isStudent)
                {
                    return; // Access granted
                }
            }

            // If neither condition is met, return unauthorized
            context.Result = new ForbidResult();
        }

        private async Task<bool> IsUserStudentAsync(ClaimsPrincipal user, int questionId, IQuestionRepository _repo)
        {
            // Get the student ID from the claims (assumed to be stored as the NameIdentifier)
            var studentId = user.FindFirst(ClaimTypes.NameIdentifier)?.Value;

            if (string.IsNullOrEmpty(studentId))
            {
                return false; // User is not authenticated or doesn't have a valid ID
            }

            // Fetch the question from the repository asynchronously
            var question = await _repo.GetById(questionId); // Assuming you have an async version of GetById

            // Check if the question exists and if the studentId matches the one associated with the question
            if (question != null && question.StudentId == studentId)
            {
                return true; // User is the student who asked the question
            }

            return false; // User is not the student who asked the question
        }
    }
}

