using ApplicationCore.Interfaces.UnitOfWork;
using Infrastructure;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebAPI.DTOs.MemberDtos;

namespace WebAPI.Controllers
{
    [Route("api/v1/MemberOperations")]
    [ApiController]
    public class MemberOperationController : ControllerBase
    {
        private readonly UserManager<User> _userManager;
        private readonly SignInManager<User> _signInManager;
        private readonly IConfiguration _configuration;

        public MemberOperationController(UserManager<User> userManager, SignInManager<User> signInManager, IConfiguration configuration)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _configuration = configuration;
        }


        [HttpPost]
        [Route("Register")]
        public async Task<IActionResult> SignUp(CreateMemberDto memberDto)
        {

            if (ModelState.IsValid)
            {
                User user = new()
                {
                    NickName = memberDto.NickName,
                    FullName = memberDto.FullName,
                    UserName = memberDto.Email,
                    Email = memberDto.Email,
                    EmailConfirmed = true,
                    RoleType = true
                };

                var registerUser = await _userManager.CreateAsync(user, memberDto.Password);
                if (registerUser.Succeeded)
                {
                    await _userManager.AddToRoleAsync(user, "member");
                    await _signInManager.SignInAsync(user, isPersistent: false);
                    var user1 = await _userManager.FindByNameAsync(user.UserName);
                    var token = JwtGenerator.Generate(user1, _configuration);
                    return Ok(token);
                }
            }
            return BadRequest(ModelState);

        }

        [HttpPost]
        [Route("Login")]
        public async Task<IActionResult> Login(LoginDto loginDto)
        {
            if (ModelState.IsValid)
            {
                var loginResult = await _signInManager.PasswordSignInAsync(loginDto.UserName, loginDto.Password, true, false);
                if (!loginResult.Succeeded)
                {
                    return BadRequest();
                }
                var user = await _userManager.FindByNameAsync(loginDto.UserName);
                var token = JwtGenerator.Generate(user, _configuration);
                return Ok(token);
            }
            return BadRequest(ModelState);

        }


    }
}
