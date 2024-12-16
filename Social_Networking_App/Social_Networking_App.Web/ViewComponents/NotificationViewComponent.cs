using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Social_Networking_App.Core.Domains;
using Social_Networking_App.Core.IServices;
using System.Security.Claims;

namespace Social_Networking_App.Web.ViewComponents
{
    public class NotificationViewComponent : ViewComponent
    {
        private readonly IProfileService _profileService;
        private readonly UserManager<IdentityUser> _userManager;
        public NotificationViewComponent(UserManager<IdentityUser> userManager, IProfileService profileService)
        {
            _profileService = profileService;
            _userManager = userManager;
        }
        public async Task<IViewComponentResult> InvokeAsync()
        {
            IEnumerable<FriendNotification> appNotifications = new List<FriendNotification>();
            try
            {
                string? singedInUserId = _userManager.GetUserId((ClaimsPrincipal)User);
                if(singedInUserId != null)
                {
                    appNotifications = await _profileService.GetUserWiseNotification(singedInUserId);
                }

            }
            catch (Exception ex)
            {
                throw;
            }
            return View("~/ViewComponents/Views/Notification.cshtml", appNotifications);
        }
    }
}
