namespace RPGCalendar.Controllers
{
    using System;
    using System.Collections.Generic;
    using System.Threading.Tasks;
    using Core.Dto;
    using Core.Services;
    using Microsoft.AspNetCore.Http;
    using Microsoft.AspNetCore.Mvc;

    [Route("api/[controller]")]
    [ApiController]
    public class UserController : Controller
    {
        private readonly IUserService _userService;
        public UserController(IUserService service)
        {
            this._userService = service;
        }

        [HttpGet]
        public async Task<List<User>> Get() => await _userService.GetPlayersList();
    }
}
