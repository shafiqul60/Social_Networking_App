using AspNetCoreHero.ToastNotification.Abstractions;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Social_Networking_App.Core.Domains;
using Social_Networking_App.Core.IServices;
using Social_Networking_App.Core.Services;
using Social_Networking_App.Web.Models;
using System.Diagnostics;

namespace Social_Networking_App.Web.Controllers
{
    [Authorize]
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly UserManager<IdentityUser> _userManager;
        private readonly IProfileService _profileService;
        private readonly IEventService _eventService;
        private readonly IWebHostEnvironment _webHostEnvironment;
        private readonly INotyfService _notyf;

        public HomeController(IEventService eventService, INotyfService notyf, IWebHostEnvironment webHostEnvironment, ILogger<HomeController> logger, UserManager<IdentityUser> userManager, IProfileService profileService)
        {
            _userManager = userManager;
            _profileService = profileService;
            _logger = logger;
            _webHostEnvironment = webHostEnvironment;
            _notyf = notyf;
            _eventService = eventService;
        }

        public IActionResult Index()
        {
            return View();
        }
        public async Task<IActionResult> Newsfeed()
        {
            string imagePath= "Images/Profiles/default-profile.png";
            string userName = "";
            string? singedInUserId = _userManager.GetUserId(User);

            if (string.IsNullOrEmpty(singedInUserId))
            {
                // Handle case where no user is signed in.
                return RedirectToAction("Login", "Account", new { area = "Identity" });
            }

            UserInfo? userInfo = await _profileService.GetUserInfoByUserId(singedInUserId);

            if (userInfo is not null)
            {
                if (string.IsNullOrEmpty(userInfo.ImgUrl))
                {
                    imagePath = "Images/Profiles/default-profile.png";
                    userName = "";
                }
                else
                {
                    imagePath = userInfo.ImgUrl;
                    userName = userInfo.FirstName + " " + userInfo.LastName;
                }
            }

            ViewBag.ImgUrl = imagePath;
            ViewBag.UserName = userName;
            var EventList = await _eventService.GetAllEvents();
            ViewBag.EventList = EventList;
            return View();
        }


        [HttpPost]
        public async Task<IActionResult> SaveEvent(string? postForm, string? userName)
        {         
            string? singedInUserId = _userManager.GetUserId(User);

            if (!string.IsNullOrEmpty(postForm) && !string.IsNullOrEmpty(userName))
            {
                Event evnetModel = new Event
                {
                    UserName = userName,
                    EventMessage = postForm,
                    CreatedBy = singedInUserId,
                    CreatedDate = DateTime.Now
                };

                try
                {
                    if(evnetModel is not null)
                    {
                        var isSaved = await _eventService.AddEvent(evnetModel);

                        if (isSaved)
                        {
                            _notyf.Success("Event Successfully Updated");
                        }
                        else
                        {
                            _notyf.Error("Failed to save event.");
                        }
                    }
                    
                }
                catch (Exception ex)
                {
                    _logger.LogError(ex, "Error while saving event.");
                    _notyf.Error("An error occurred while saving the event.");
                }
            }
            else
            {
                _notyf.Error("Invalid input. Please provide all required fields.");
            }

            return RedirectToAction("Newsfeed", "Home");
        }







    }
}
