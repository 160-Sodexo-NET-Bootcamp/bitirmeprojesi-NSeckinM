using ApplicationCore.Entities;
using ApplicationCore.Enum;
using ApplicationCore.Interfaces.UnitOfWork;
using Infrastructure;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using WebAPI.DTOs.MemberDtos;

namespace WebAPI.Controllers
{
    [Route("api/v1/MemberOperations")]
    [ApiController]
    public class MemberOperationController : ControllerBase
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly UserManager<User> _userManager;
        private readonly SignInManager<User> _signInManager;
        private readonly IConfiguration _configuration;

        public MemberOperationController(IUnitOfWork unitOfWork, UserManager<User> userManager, SignInManager<User> signInManager, IConfiguration configuration)
        {
            _unitOfWork = unitOfWork;
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
                    Mail mail = new()
                    {
                        EmailAdress = user.Email,
                        EmailStatus = EmailStatus.WelcomeMail
                    };
                    await _unitOfWork.MailService.AddMail(mail);
                    var user1 = await _userManager.FindByNameAsync(user.UserName);
                    var token = JwtGenerator.Generate(user1, _configuration);
                    _unitOfWork.Complete();
                    return Ok("Kayıt işlemi başarılı:" + token);
                }
            }
            return BadRequest("Kayıt işlemi başarısız geçersiz bilgiler girdiniz.");

        }

        //string userEmail = User.FindFirst(ClaimTypes.Email).Value;
        //string userId = User.FindFirst(ClaimTypes.NameIdentifier).Value;
        [HttpPost]
        [Route("Login")]
        public async Task<IActionResult> Login(LoginDto loginDto)
        {
            var user = await _userManager.FindByNameAsync(loginDto.UserName);
            if (user == null)
            {
                return BadRequest("There is no registered user in system with this email Please try with valid email");
            }
            if (user.AccessFailedCount < 3)
            {
                if (ModelState.IsValid)
                {
                    var loginResult = await _signInManager.PasswordSignInAsync(loginDto.UserName, loginDto.Password, true, false);
                    if (loginResult.Succeeded)
                    {
                        var token = JwtGenerator.Generate(user, _configuration);
                        return Ok("Giriş işlemi başarılı : " + "  //  " + token);
                    }
                    else
                    {
                        user.AccessFailedCount++;
                        _unitOfWork.Complete();
                        return BadRequest();
                    }
                }
                return BadRequest(ModelState);
            }
            else
            {
                user.LockoutEnabled = true;
                user.LockoutEnd = DateTime.Now.AddMinutes(5);
                user.AccessFailedCount = 0;
                Mail mail = _unitOfWork.MailService.GetMail(user.Email);
                mail.EmailStatus = EmailStatus.BlockMail;
                _unitOfWork.Complete();
                return BadRequest("This Account locked out for 3 days");
            }

        }


    }
}
