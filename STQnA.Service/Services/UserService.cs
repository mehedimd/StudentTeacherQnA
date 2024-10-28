using Microsoft.AspNetCore.Http;
using STQnA.Service.Interfaces;
using System.Security.Claims;

namespace STQnA.Service.Services;
public class UserService : IUserService
{
    private readonly IHttpContextAccessor _httpContextAccessor;
    public UserService(IHttpContextAccessor httpContextAccessor)
    {
        _httpContextAccessor = httpContextAccessor;
    }

    public string GetCurrentUserId
    {
        get
        {
            var userId = _httpContextAccessor.HttpContext?.User?.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            return userId ?? string.Empty; // Return empty string if userId is null
        }
    }
}