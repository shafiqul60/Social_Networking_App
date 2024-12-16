using Social_Networking_App.Core.Domains.IRepositories;
using Social_Networking_App.Core.Domains;
using Social_Networking_App.Infrastructure.DbContext;
using Microsoft.AspNetCore.Mvc;

namespace Social_Networking_App.Infrastructure.Repositories
{
    public class EventRepo : GenericRepo<Event>, IEventRepo
    {
        private readonly ApplicationDbContext _db;
        public EventRepo(ApplicationDbContext db) : base(db)
        {
            _db = db;
        }

       
    }
}
