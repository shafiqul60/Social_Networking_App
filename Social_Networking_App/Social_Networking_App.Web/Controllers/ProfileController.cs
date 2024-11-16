using AspNetCoreHero.ToastNotification.Abstractions;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Social_Networking_App.Core.Domains;
using Social_Networking_App.Core.IServices;
using Social_Networking_App.Core.ViewModels;

namespace Social_Networking_App.Web.Controllers
{
    [Authorize]
    public class ProfileController : Controller
    {
        private readonly IProfileService _profileService;
        private readonly IWebHostEnvironment _webHostEnvironment;
        private readonly UserManager<IdentityUser> _userManager;
        private readonly INotyfService _notyf;

        public ProfileController(IProfileService profileService, IWebHostEnvironment webHostEnvironment, UserManager<IdentityUser> userManager, INotyfService notyf) 
        {
            _profileService = profileService;
            _webHostEnvironment = webHostEnvironment;
            _userManager = userManager;
            _notyf = notyf;

        }

        [HttpGet]
        public async Task<IActionResult> Upsert(int? id)
        {
            UserInfoVM userInfoVM = new UserInfoVM
            {
                UserInfo = new UserInfo()  // Initialize UserInfo here
            };

            string? singedInUserId = _userManager.GetUserId(User);

            if (singedInUserId is not null)
            {
                userInfoVM.UserInfo.UserId = singedInUserId;
            }


            IEnumerable<SelectListItem> GenderItemList = new List<SelectListItem>
                {
                    new SelectListItem { Value = "Male", Text = "Male" },
                    new SelectListItem { Value = "Female", Text = "Female" },
                    new SelectListItem { Value = "Other", Text = "Other" }
             };

            
          

            if (id is null || id == 0)
            {
                ViewBag.GenderList = GenderItemList;
                return View(userInfoVM);
            }
            else
            {                         
                userInfoVM.UserInfo = await _profileService.GetUserInfo(id);
                ViewBag.GenderList = GenderItemList;
                return View(userInfoVM);
            }       
        }

        [HttpPost]
        public async Task <IActionResult> Upsert(UserInfoVM model, IFormFile? file)
        {
            if (ModelState.IsValid)
            {              
                if (file is not null)
                {
                    string wwwRootPath = _webHostEnvironment.WebRootPath;
                    string fileName = Guid.NewGuid().ToString() + Path.GetExtension(file.FileName);
                    string profileImagePath = Path.Combine(wwwRootPath, @"Images", "Profiles", fileName);
                    using(var fileStream = new FileStream(Path.Combine(wwwRootPath, profileImagePath), FileMode.Create))
                    {
                       await file.CopyToAsync(fileStream);
                    }                      
                    model.UserInfo.ImgUrl = @"Images\Profiles" + fileName;
                }              
                var isSaved = await _profileService.UserInfoAdd(model.UserInfo);
                if (isSaved)
                {
                    _notyf.Success("Information Successfully Updated");
                }
            }
            else
            {

            }
            return View();
        }
    }
}
