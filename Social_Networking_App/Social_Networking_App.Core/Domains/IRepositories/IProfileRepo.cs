using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Social_Networking_App.Core.Domains.IRepositories
{
    public interface IProfileRepo : IGenericRepo<UserInfo>
    {
        Task<UserInfo?> GetUserInfoByUserId(string userId);

        Task<IEnumerable<UserInfo>> GetFriendList(string userId);

        Task<IEnumerable<UserInfo>> GetFriendBySearch(string searchQuery, string userId);

        Task<bool> AddNotification(FriendNotification model);

        Task<string> GetUserNameById(string userId);

        Task<IEnumerable<FriendNotification>> GetUserWiseNotification(string userId);

        Task<FriendNotification> GetNotificationById(int id);

        Task<bool> UpdateNotification(FriendNotification model);

        Task<bool> AddFriendIntoFriendList(FriendList model);

        Task<IEnumerable<FriendList>> GetUserWiseFriendList(string usId);

        Task<IEnumerable<UserInfo>> GetExistingFriendList(string useId);

    }
}
