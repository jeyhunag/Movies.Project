using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Movies.DAL.DbModel;
using Movies.WebAdmin.ViewModels;
using System.Text;

namespace Movies.WebAdmin.Controllers
{
    public class UserManagmentController : Controller
    {
        private UserManager<AppUser> _userManager;
        private RoleManager<AppRole> _roleManager;
        public UserManagmentController(UserManager<AppUser> userManager, RoleManager<AppRole> roleManager)
        {
            _userManager = userManager;
            _roleManager = roleManager;
        }

        public IActionResult Index()
        {
            return View();
        }

        #region UserOperation
        public IActionResult UserIndex()
        {

            List<UserViewModel> viewModels = new List<UserViewModel>();

            List<AppUser> appUsers = _userManager.Users.ToList();
            foreach (var item in appUsers)
            {
                UserViewModel viewModel = new UserViewModel
                {
                    Id = item.Id,
                    Name = item.Name,
                    Surname = item.Surname,
                    Country = item.Country,
                    Img = item.Img,
                    DateOfBirth = item.DateOfBirth,
                    Email = item.Email,
                    UserName = item.UserName,
                    Gender = item.Gender,

                };
                viewModels.Add(viewModel);
            }

            return View(viewModels);
        }

        public IActionResult UserCreate()
        {

            return View();

        }
        [HttpPost]
        public async Task<IActionResult> UserCreate(UserViewModel viewModel)
        {
            if (ModelState.IsValid)
            {
                AppUser user = new AppUser()
                {

                    Name = viewModel.Name,
                    Surname = viewModel.Surname,
                    UserName = viewModel.UserName,
                    Country = viewModel.Country,
                    Img = viewModel.Img,
                    DateOfBirth = viewModel.DateOfBirth,
                    Email = viewModel.Email,
                    Gender = viewModel.Gender
                };
                IdentityResult result = await _userManager.CreateAsync(user, viewModel.Password);
                if (result.Succeeded)
                {
                    return RedirectToAction("UserIndex");
                }

            }
            return View(viewModel);

        }

        public async Task<string> UserRole(string id)
        {

            AppUser user = await _userManager.FindByIdAsync(id);
            IList<string> roles = await _userManager.GetRolesAsync(user);

            StringBuilder builder = new StringBuilder();
            foreach (var item in roles)
            {
                builder.Append(item + "; ");
            }
            return builder.ToString();
        }
        #endregion

        #region RoleOperation
        public IActionResult RoleIndex()
        {

            List<RoleViewModel> viewModels = new List<RoleViewModel>();

            List<AppRole> appRoles = _roleManager.Roles.ToList();
            foreach (var item in appRoles)
            {
                RoleViewModel viewModel = new RoleViewModel
                {
                    Name = item.Name

                };
                viewModels.Add(viewModel);
            }

            return View(viewModels);
        }
        public IActionResult RoleCreate()
        {

            return View();

        }
        [HttpPost]
        public async Task<IActionResult> RoleCreate(RoleViewModel viewModel)
        {
            if (ModelState.IsValid)
            {
                AppRole role = new AppRole()
                {
                    Name = viewModel.Name
                };
                IdentityResult result = await _roleManager.CreateAsync(role);
                if (result.Succeeded)
                {
                    return RedirectToAction("RoleIndex");
                }

            }
            return View(viewModel);

        }

        public async Task<IActionResult> RoleAssign(string Id)
        {
            AppUser user = await _userManager.FindByIdAsync(Id);

            UserRoleViewModel viewModel = new UserRoleViewModel()
            {
                UserFullName = user.Name + " " + user.Surname,
                UserId = user.Id
            };
            List<AppRole> roles = _roleManager.Roles.ToList();

            List<RoleViewModel> roleViewModels = new List<RoleViewModel>();
            foreach (var item in roles)
            {
                RoleViewModel roleView = new RoleViewModel()
                {
                    Id = item.Id,
                    Name = item.Name,
                };
                roleViewModels.Add(roleView);
            }
            viewModel.Roles = roleViewModels;
            return View(viewModel);

        }

        [HttpPost]
        public async Task<IActionResult> RoleAssign(UserRoleViewModel viewModel)
        {
            AppUser user = await _userManager.FindByIdAsync(viewModel.UserId);
            if (user != null)
            {
                IdentityResult result = await _userManager.AddToRoleAsync(user, viewModel.RoleName);
                if (result.Succeeded)
                {
                    return RedirectToAction("UserIndex");
                }
            }


            return View(viewModel);

        }
        #endregion
    }

}
