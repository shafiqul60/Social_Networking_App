using Microsoft.EntityFrameworkCore;
using Social_Networking_App.Core.Domains;
using Social_Networking_App.Core.Domains.IRepositories;
using Social_Networking_App.Infrastructure.DbContext;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Social_Networking_App.Infrastructure.Repositories
{
    public class ProfileRepo : GenericRepo<UserInfo>, IProfileRepo
    {
        private readonly ApplicationDbContext _db;
        public ProfileRepo(ApplicationDbContext db) : base(db)
        {
            _db = db;
        }

        public async Task<UserInfo?> GetUserInfoByUserId(string userId)
        {
            if(string.IsNullOrEmpty(userId)) 
            {
                return null;
            }

            return await _db.UserInfos.Where(u => u.UserId == userId).FirstOrDefaultAsync();
        }

        public async Task<IEnumerable<UserInfo>> GetFriendList(string userId)
        {
            var list1= await _db.UserInfos.Where(u => u.UserId != userId).ToListAsync();
            var list2 = await GetUserWiseFriendList(userId);

            // Get all FriendIds from list2
            var friendIds = list2.Select(f => f.FriendId).ToHashSet();

            // Remove users from list1 whose UserId is in the list of FriendIds
            list1 = list1.Where(u => !friendIds.Contains(u.UserId)).ToList();

            return list1;
        }


        public async Task<IEnumerable<UserInfo>> GetFriendBySearch(string searchQuery, string userId)
        {
            var list1 = await _db.UserInfos.Where(u => u.UserId != userId).ToListAsync();
            var list2 = await GetUserWiseFriendList(userId);

            // Get all FriendIds from list2
            var friendIds = list2.Select(f => f.FriendId).ToHashSet();

            // Remove users from list1 whose UserId is in the list of FriendIds
            list1 = list1.Where(u => !friendIds.Contains(u.UserId)).ToList();

            var result = list1.Where(u => u.FirstName.Contains(searchQuery));
            return result;
        }

        public async Task<string> GetUserNameById(string userId)
        {
            return await _db.UserInfos.Where(u => u.UserId == userId).Select(u => u.FirstName).FirstOrDefaultAsync() ?? string.Empty;
        }

        public async Task<bool> AddNotification(FriendNotification model)
        {
            if (model is null)
            {
                throw new ArgumentNullException("model");
            }
            else
            {
                await _db.FriendNotifications.AddAsync(model);
                var isSaved = await _db.SaveChangesAsync();
                return isSaved > 0;
            }
        }

        public async Task<IEnumerable<FriendNotification>> GetUserWiseNotification(string userId)
        {
            return await _db.FriendNotifications.Where(x=> x.ToRequest == userId && x.IsSeen == false).ToListAsync();
        }


        public async Task<FriendNotification> GetNotificationById(int id)
        {
            if(id > 0)
            {
                return await _db.FriendNotifications.Where(x => x.NotificationId == id).FirstOrDefaultAsync();
            }
            return null;
        }

        public async Task<bool> UpdateNotification(FriendNotification model)
        {
            if (model is null)
            {
                throw new ArgumentNullException("model");
            }
            else
            {
                 _db.FriendNotifications.Update(model);
                var isSaved = await _db.SaveChangesAsync();
                return isSaved > 0;
            }
        }

        public async Task<bool> AddFriendIntoFriendList(FriendList model)
        {
            if (model is null)
            {
                throw new ArgumentNullException("model");
            }
            else
            {
                 await _db.FriendLists.AddAsync(model);
                var isSaved = await _db.SaveChangesAsync();
                return isSaved > 0;
            }
        }

        public async Task<IEnumerable<FriendList>> GetUserWiseFriendList(string usId)
        {
            return await _db.FriendLists.Where(x => x.UserId == usId).ToListAsync();
        }


        public async Task<IEnumerable<UserInfo>> GetExistingFriendList(string useId)
        {
            var existingFriendList = await _db.FriendLists.Where(u => u.UserId == useId).Select(u => u.FriendId).ToListAsync();

            var DetailOfExistingFriendList = await _db.UserInfos.Where(u => existingFriendList.Contains(u.UserId)).ToListAsync();

            return DetailOfExistingFriendList;
        }


    }
}
