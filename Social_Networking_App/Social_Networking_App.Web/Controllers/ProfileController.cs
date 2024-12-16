using AspNetCoreHero.ToastNotification.Abstractions;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Social_Networking_App.Core.Domains;
using Social_Networking_App.Core.IServices;
using Social_Networking_App.Core.ViewModels;
using System.Collections.Generic;
using System.Security.Claims;

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
        public async Task<IActionResult> Upsert(UserInfoVM model, IFormFile? file)
        {
            if (ModelState.IsValid)
            {
                if (file is not null)
                {
                    string wwwRootPath = _webHostEnvironment.WebRootPath;
                    string fileName = Guid.NewGuid().ToString() + Path.GetExtension(file.FileName);
                    string profileImagePath = Path.Combine(wwwRootPath, @"Images", "Profiles", fileName);
                    //using(var fileStream = new FileStream(Path.Combine(wwwRootPath, profileImagePath), FileMode.Create))
                    //{
                    //   await file.CopyToAsync(fileStream);
                    //}                      
                    using (var fileStream = new FileStream(profileImagePath, FileMode.Create))
                    {
                        await file.CopyToAsync(fileStream);
                    }
                    model.UserInfo.ImgUrl = @"Images/Profiles/" + fileName;
                }
                var isSaved = await _profileService.UserInfoAdd(model.UserInfo);
                if (isSaved)
                {
                    _notyf.Success("Information Successfully Updated");
                }
            }
            else
            {
                _notyf.Warning("Please check your infromation!!");
            }
            return RedirectToAction("Newsfeed", "Home");
        }

        [HttpGet]
        public async Task<IActionResult> FindFriends(string searchQuery)
        {

            string? singedInUserId = _userManager.GetUserId(User);

            if (singedInUserId is not null)
            {
                var ExistingFriends = await _profileService.GetExistingFriendList(singedInUserId);

                IList<UserInfo> userList = new List<UserInfo>();

                if (!string.IsNullOrEmpty(searchQuery))
                {
                    userList = (IList<UserInfo>) await _profileService.GetFriendBySearch(searchQuery, singedInUserId);
                    ViewData["SearchQuery"] = searchQuery;
                }
                else
                {
                    userList = (IList<UserInfo>) await _profileService.GetFriendList(singedInUserId);
                }

                ViewBag.ExistingFriends = ExistingFriends;


                return View(userList);
            }
            else
            {
                ViewBag.ExistingFriends = null;
                return View();
            }
       
        }


     
        public async Task<IActionResult> AddFriendRequest(string id)
        {

            string? singedInUserId = _userManager.GetUserId(User);

            if (!string.IsNullOrEmpty(singedInUserId) && !string.IsNullOrEmpty(id))
            {
                var fromUserName = await _profileService.GetUserNameById(singedInUserId);
                var massage = fromUserName + " " + "Sent you a friend request";

                FriendNotification friendNotification = new FriendNotification{
                    FromRequest = singedInUserId,
                    ToRequest = id,
                    NotiMessage = massage,
                    CreatedDate = DateTime.Now,
                    IsSeen = false
                };
               
                var isSave = await _profileService.AddNotification(friendNotification);

                if(isSave)
                {
                    _notyf.Success("Friend request sent successfully");
                }
                return RedirectToAction("FindFriends", "Profile");

            }
            else
            {
                return RedirectToAction("FindFriends", "Profile");
            }
        }


        public async Task<IActionResult> DetailNotification()
        {
            IEnumerable<FriendNotification> appNotifications = new List<FriendNotification>();
         
            string? singedInUserId = _userManager.GetUserId((ClaimsPrincipal)User);
            if (singedInUserId != null)
            {
                appNotifications = await _profileService.GetUserWiseNotification(singedInUserId);
            }

            return View(appNotifications);
        }


        public async Task<IActionResult> Accept(int id)
        {
            var notification = await _profileService.GetNotificationById(id);

            notification.IsSeen = true;

           

            var isUpdate = await _profileService.UpdateNotification(notification);

            if (isUpdate)
            {
                FriendList friendList = new FriendList();
                friendList.UserId = notification.FromRequest;
                friendList.FriendId = notification.ToRequest;

                var isAdded = await _profileService.AddFriendIntoFriendList(friendList);
                if(isAdded)
                {
                    _notyf.Success("Successfully Added");
                }              
            }

            return RedirectToAction("DetailNotification", "Profile");
        }

        public async Task<IActionResult> Reject(int id)
        {
            var notification = await _profileService.GetNotificationById(id);

            notification.IsSeen = true;

            var isUpdate = await _profileService.UpdateNotification(notification);

            if (isUpdate)
            {
                _notyf.Success("Successfully Rejected");
            }

            return RedirectToAction("DetailNotification", "Profile");
        }

    }
}
