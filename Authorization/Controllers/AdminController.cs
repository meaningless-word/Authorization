using Authorization.Entities;
using Authorization.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace Authorization.Controllers
{
	[Authorize]
	public class AdminController : Controller
	{
		private readonly UserManager<ApplicationUser> _userManager;
		private readonly SignInManager<ApplicationUser> _signInManager;

		public AdminController(UserManager<ApplicationUser> userManager, SignInManager<ApplicationUser> signInManager)
		{
			_userManager = userManager;
			_signInManager = signInManager;
		}

		public IActionResult Index()
		{
			return View();
		}

		[Authorize(Policy = "Administrator")]
		public IActionResult Administrator()
		{
			return View();
		}

		[Authorize(Policy = "Manager")]
		public IActionResult Manager()
		{
			return View();
		}

		[AllowAnonymous]
		public IActionResult Login(string returnUrl)
		{
			return View();
		}

		[HttpPost]
		[AllowAnonymous]
		public async Task<IActionResult> Login(LoginViewModel model)
		{
			if (!ModelState.IsValid)
			{
				return View(model);
			}

			//var user = await _context.Users.SingleOrDefaultAsync(x => x.UserName == model.UserName && x.Password == model.Password);
			var user = await _userManager.FindByNameAsync(model.UserName);
			if (user == null)
			{
				//ModelState.AddModelError("UserName", "User Not Found");
				ModelState.AddModelError("", "User Not Found");
				return View(model);
			}

			//var claims = new List<Claim>
			//{
			//	new Claim(ClaimTypes.Name, model.UserName),
			//	new Claim(ClaimTypes.Role, "Administrator")
			//};
			//var claimIdentity = new ClaimsIdentity(claims, "Cookie");
			//var claimPrincipal = new ClaimsPrincipal(claimIdentity);
			//await HttpContext.SignInAsync("Cookie", claimPrincipal);

			var result = await _signInManager.PasswordSignInAsync(user, model.Password, false, false);

			if (result.Succeeded)
			{
				return Redirect(model.ReturnUrl);
			}

			return View(model);

		}

		public async Task<IActionResult> LogOffAsync()
		{
			//HttpContext.SignOutAsync("Cookie");
			await _signInManager.SignOutAsync();
			return Redirect("/Home/Index");
		}
	}
}
