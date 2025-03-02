using Microsoft.AspNetCore.Mvc;

namespace GameZone.Controllers
{
    public class RuleController : Controller
    {
        private readonly RoleManager<IdentityRole> _roleManager;

        public RuleController(RoleManager<IdentityRole> RoleManager)
        {
            _roleManager = RoleManager;
        }
        public IActionResult Index()
        {
            return View();
        }
        [HttpGet]
        public IActionResult New()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> New(RoleViewModel viewModel)
        {
            if(ModelState.IsValid)
            {
                IdentityRole role = new IdentityRole();
                role.Name = viewModel.RoleName;

                IdentityResult result = await _roleManager.CreateAsync(role);

                if(result.Succeeded)
                {
                    return RedirectToAction("Index" , "Home");
                }
                else
                {
                    foreach ( var error in result.Errors)
                    {
                        ModelState.AddModelError("", error.Description);
                    }
                }
            }
            return View(viewModel);
        }
    }
}
