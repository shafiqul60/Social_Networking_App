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
    }
}
