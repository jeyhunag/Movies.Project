using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Movies.DAL.DbModel;
using Movies.WebAdmin.ViewModels;
using System.Text;

namespace Movies.WebAdmin.Controllers
{
    //[Authorize(Roles = "Admin")]
    public class UserManagmentController : Controller
    {
        private UserManager<AppUser> _userManager;
        private RoleManager<AppRole> _roleManager;
        private readonly IWebHostEnvironment _webHostEnvironment;
        private readonly string _imgPath = @"img/";

        public UserManagmentController(UserManager<AppUser> userManager, RoleManager<AppRole> roleManager, IWebHostEnvironment webHostEnvironment)
        {
            _userManager = userManager;
            _roleManager = roleManager;
            _webHostEnvironment = webHostEnvironment;
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
        public async Task<IActionResult> UserCreate(UserViewModel viewModel, IFormFile imageFile)
        {
            //if (ModelState.IsValid)
            //{

            if (imageFile != null && imageFile.Length > 0)
            {
                var imagePath = _imgPath + imageFile.FileName;
                var fullPath = Path.Combine(_webHostEnvironment.WebRootPath, imagePath);
                using (var stream = new FileStream(fullPath, FileMode.Create))
                {
                    await imageFile.CopyToAsync(stream);
                    viewModel.Img = imagePath;
                }
            }

            AppUser user = new AppUser()
            {
                Id = viewModel.Id,
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

            //}
            return View(viewModel);

        }
        public async Task<IActionResult> UserUpdate(string id)
        {
            AppUser user = await _userManager.FindByIdAsync(id);

            if (user == null)
            {
                return NotFound();
            }

            UserViewModel viewModel = new UserViewModel
            {
                Id = user.Id,
                Name = user.Name,
                Surname = user.Surname,
                Country = user.Country,
                Img = user.Img,
                DateOfBirth = user.DateOfBirth,
                Email = user.Email,
                UserName = user.UserName,
                Gender = user.Gender
            };

            return View(viewModel);
        }

        [HttpPost]
        public async Task<IActionResult> UserUpdate(UserViewModel viewModel, IFormFile imageFile)
        {
            //if (ModelState.IsValid)
            //{
            if (imageFile != null && imageFile.Length > 0)
            {
                var imagePath = _imgPath + imageFile.FileName;
                var fullPath = Path.Combine(_webHostEnvironment.WebRootPath, imagePath);
                using (var stream = new FileStream(fullPath, FileMode.Create))
                {
                    await imageFile.CopyToAsync(stream);
                    viewModel.Img = imagePath;
                }
            }

            AppUser user = await _userManager.FindByIdAsync(viewModel.Id);

            if (user == null)
            {
                return NotFound();
            }

            user.Name = viewModel.Name;
            user.Surname = viewModel.Surname;
            user.Country = viewModel.Country;
            user.Img = viewModel.Img;
            user.DateOfBirth = viewModel.DateOfBirth;
            user.Email = viewModel.Email;
            user.UserName = viewModel.UserName;
            user.Gender = viewModel.Gender;

            IdentityResult result = await _userManager.UpdateAsync(user);

            if (result.Succeeded)
            {
                return RedirectToAction("UserIndex");
            }
            //}

            return View(viewModel);
        }

        public async Task<IActionResult> UserDelete(string id)
        {
            AppUser user = await _userManager.FindByIdAsync(id);
            if (user != null)
            {
                IdentityResult result = await _userManager.DeleteAsync(user);
                if (result.Succeeded)
                {
                    return RedirectToAction("UserIndex");
                }
            }
            return RedirectToAction("UserIndex");
        }

        #endregion

        #region RoleOperation
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
        

        
        public IActionResult RoleIndex()
        {

            List<RoleViewModel> viewModels = new List<RoleViewModel>();

            List<AppRole> appRoles = _roleManager.Roles.ToList();
            foreach (var item in appRoles)
            {
                RoleViewModel viewModel = new RoleViewModel
                {
                    Id = item.Id,
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
            //if (ModelState.IsValid)
            //{
                AppRole role = new AppRole()
                {
                    Name = viewModel.Name
                };
                IdentityResult result = await _roleManager.CreateAsync(role);
                if (result.Succeeded)
                {
                    return RedirectToAction("RoleIndex");
                }

            //}
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

        public async Task<IActionResult> RoleUpdate(string id)
        {
            AppRole role = await _roleManager.FindByIdAsync(id);
            if (role == null)
            {
                return NotFound();
            }

            RoleViewModel viewModel = new RoleViewModel
            {
                Id = role.Id,
                Name = role.Name
            };

            return View(viewModel);
        }

        [HttpPost]
        public async Task<IActionResult> RoleUpdate(RoleViewModel viewModel)
        {
            if (!ModelState.IsValid)
            {
                return View(viewModel);
            }

            AppRole role = await _roleManager.FindByIdAsync(viewModel.Id);
            if (role == null)
            {
                return NotFound();
            }

            role.Name = viewModel.Name;
            IdentityResult result = await _roleManager.UpdateAsync(role);

            if (result.Succeeded)
            {
                return RedirectToAction("RoleIndex");
            }

            foreach (IdentityError error in result.Errors)
            {
                ModelState.AddModelError("", error.Description);
            }

            return View(viewModel);
        }

        [HttpGet]
        public async Task<IActionResult> RoleDelete(string id)
        {
            AppRole role = await _roleManager.FindByIdAsync(id);
            if (role == null)
            {
                return NotFound();
            }

            IdentityResult result = await _roleManager.DeleteAsync(role);
            if (result.Succeeded)
            {
                return RedirectToAction("RoleIndex");
            }
            else
            {
                return BadRequest(result.Errors);
            }
        }

        //public async Task<IActionResult> RoleDelete(int id)
        //{
        //    AppRole role = await _roleManager.FindByIdAsync(id.ToString());

        //    if (role == null)
        //    {
        //        return NotFound();
        //    }

        //    IdentityResult result = await _roleManager.DeleteAsync(role);

        //    if (result.Succeeded)
        //    {
        //        return Ok();
        //    }

        //    return BadRequest();
        //}


        #endregion
    }

}
