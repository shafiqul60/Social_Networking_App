using Social_Networking_App.Core.Domains;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Social_Networking_App.Core.IServices
{
    public interface IProfileService
    {
        Task<bool> UserInfoAdd(UserInfo model);
        Task<UserInfo> GetUserInfo(int? id);
    }
}
