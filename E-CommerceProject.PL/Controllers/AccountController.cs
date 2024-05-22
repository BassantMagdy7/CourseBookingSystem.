using AutoMapper;
using CourseBookingSystem.BLL.UnitOfWork;
using CourseBookingSystem.DAL.Models;
using CourseBookingSystem.PL.ViewModels;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace CourseBookingSystem.PL.Controllers
{
    public class AccountController : Controller
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public AccountController(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }
        //To Show Form
        public IActionResult Register()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Register(RegisterViewModel model)
        {
            if (ModelState.IsValid)
            {
                var User = _mapper.Map<User>(model);
                await _unitOfWork.Users.AddUserAsync(User);
                await _unitOfWork.SaveAsync();
                return RedirectToAction("Login");
            }
            // Return the view if model state is not valid
            return View();
        }
        //To Show Form
        public IActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Login(LoginViewModel model)
        {
            var user = await _unitOfWork.Users.GetUserByEmailAndPasswordAsync(model.UserName, model.UserPassword);
            if (user == null)
            {
                ModelState.AddModelError("", "Invalid username or password");
                return View(model);
            }

            // Generate claims based on user properties
            var claims = new List<Claim>
            {
                new Claim(ClaimTypes.Name, user.UserName),
                new Claim(ClaimTypes.Email, user.UserEmail)
            };

            // Create identity and principal
            var identity = new ClaimsIdentity(claims, "Cookies");
            var principal = new ClaimsPrincipal(identity);

            // Sign in the user
            await HttpContext.SignInAsync(principal);
            return RedirectToAction("Index", "Course");
        }

        public async Task<IActionResult> LogOut()
        {
            await HttpContext.SignOutAsync();
            return RedirectToAction("Index", "Home");
        }
    }
}
