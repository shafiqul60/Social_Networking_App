using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Social_Networking_App.Core.IServices;
using Social_Networking_App.Core.Services;

namespace Social_Networking_App.Web.Controllers
{
    public class MessingController : Controller
    {
        private readonly UserManager<IdentityUser> _userManager;
        private readonly IProfileService _profileService;
        public MessingController(IProfileService profileService, UserManager<IdentityUser> userManager)
        {
            _userManager = userManager;
            _profileService = profileService;
        }
        public async Task<IActionResult> Chat()
        {
            string? singedInUserId = _userManager.GetUserId(User);

            if(!string.IsNullOrEmpty(singedInUserId))
            {
                var fromUserName = await _profileService.GetUserNameById(singedInUserId);
                ViewBag.CurrentUser = fromUserName;
            }    
            return View();
        }
    }
}
