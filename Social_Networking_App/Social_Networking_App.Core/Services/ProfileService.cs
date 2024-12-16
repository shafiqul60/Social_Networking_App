using Social_Networking_App.Core.Domains;
using Social_Networking_App.Core.Domains.IRepositories;
using Social_Networking_App.Core.IServices;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Social_Networking_App.Core.Services
{
    public class ProfileService : IProfileService
    {
        private readonly IProfileRepo _profileRepo;
        public ProfileService(IProfileRepo profileRepo)
        {
            _profileRepo = profileRepo;
        }

        public async Task<UserInfo> GetUserInfo(int? id)
        {
            return await _profileRepo.GetAsync(id);
        } 

        public async Task<bool> UserInfoAdd(UserInfo model)
        {
            if (model is null)
            {
                throw new ArgumentNullException("model");
            }
            else
            {
                return await _profileRepo.AddAsync(model);
            }

        }

        public async Task<UserInfo?> GetUserInfoByUserId(string userId)
        {
            return await _profileRepo.GetUserInfoByUserId(userId);
        }


        public async Task<IEnumerable<UserInfo>> GetFriendList(string userId)
        {
            return await _profileRepo.GetFriendList(userId);
        }

        public async Task<IEnumerable<UserInfo>> GetFriendBySearch(string searchQuery, string userId)
        {
            return await _profileRepo.GetFriendBySearch(searchQuery, userId);
        }


        public async Task<string> GetUserNameById(string userId)
        {
            return await _profileRepo.GetUserNameById(userId);
        }

        public async Task<bool> AddNotification(FriendNotification model)
        {
            return await _profileRepo.AddNotification(model);
        }


        public async Task<IEnumerable<FriendNotification>> GetUserWiseNotification(string userId)
        {
            return await _profileRepo.GetUserWiseNotification(userId);
        }


        public async Task<FriendNotification> GetNotificationById(int id)
        {
           return await _profileRepo.GetNotificationById(id);
        }

        public async Task<bool> UpdateNotification(FriendNotification model)
        {
          return await _profileRepo.UpdateNotification(model);
        }


        public async Task<bool> AddFriendIntoFriendList(FriendList model)
        {
            return await _profileRepo.AddFriendIntoFriendList(model);
        }

        public async Task<IEnumerable<FriendList>> GetUserWiseFriendList(string usId)
        {
            return await _profileRepo.GetUserWiseFriendList(usId);
        }

        public async Task<IEnumerable<UserInfo>> GetExistingFriendList(string useId)
        {         
            return await _profileRepo.GetExistingFriendList(useId);
        }

    }
}
