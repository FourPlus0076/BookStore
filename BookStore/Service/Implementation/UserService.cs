﻿using System.Security.Claims;
using BookStore.Service.Interface;

namespace BookStore.Service.Implementation
{
#nullable disable
    public class UserService : IUserService
    {
        private readonly IHttpContextAccessor _httpContent;
        public UserService(IHttpContextAccessor httpContent)
        {
            _httpContent = httpContent;
        }
        public string GetUserId()
        {
            return _httpContent.HttpContext.User?.FindFirstValue(ClaimTypes.NameIdentifier);
        }
        public bool IsAthunticated()
        {
            return _httpContent.HttpContext.User.Identity.IsAuthenticated;
        }
    }
}
