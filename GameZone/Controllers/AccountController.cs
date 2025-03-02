using Microsoft.AspNetCore.Mvc;

namespace GameZone.Controllers
{
	public class AccountController : Controller
	{
		private readonly UserManager<ApplicationUser> _applicationUser;
        private readonly SignInManager<ApplicationUser> _signInManager;

        public AccountController(UserManager<ApplicationUser> applicationUser , SignInManager<ApplicationUser> signInManager)
        {
			_applicationUser = applicationUser;
            _signInManager = signInManager;
        }
        [HttpGet]
		public IActionResult Register()
		{
			return View();
		}
		public IActionResult Logout()
		{
			_signInManager.SignOutAsync();
			return RedirectToAction("Index" , "Games");
		}
		[HttpPost]
		[ValidateAntiForgeryToken]
		public async Task<IActionResult> Register(UserViewModel viewModel)
		{
			if (ModelState.IsValid)
			{
				ApplicationUser NewUser = new ApplicationUser();

				NewUser.UserName = viewModel.UserName;
				NewUser.Address = viewModel.Address;
				NewUser.PasswordHash = viewModel.Password;

				IdentityResult result =  await _applicationUser.CreateAsync(NewUser , viewModel.Password);

				if (result.Succeeded)
				{
					await _applicationUser.AddToRoleAsync(NewUser, "Admin");
					await _signInManager.SignInAsync(NewUser, false);
					return RedirectToAction("Index" , "Home");
				}
				else
				{
					foreach (var errorItem in result.Errors)
					{
						ModelState.AddModelError("Password", errorItem.Description);
					}
				}
			}
			return View(viewModel);
		}
		[HttpGet]
		public IActionResult Login()
		{
			return View();
		}
        [HttpPost]
		[ValidateAntiForgeryToken]
        public async Task<IActionResult> Login(LoginUserViewModel viewModel)
        {
			if (ModelState.IsValid)
			{
				var appUser = await _applicationUser.FindByNameAsync(viewModel.UserName);

				if(appUser != null)
				{
					bool Found = await _applicationUser.CheckPasswordAsync(appUser, viewModel.Password);
					if (Found)
					{
						await _signInManager.SignInAsync(appUser, viewModel.RememberMe);
						return RedirectToAction("Index", "Home");
					}
				}
				ModelState.AddModelError("Password", "User Name or password are wrong");

            }
            return View(viewModel);
        }
    }
}
