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


    }
}
