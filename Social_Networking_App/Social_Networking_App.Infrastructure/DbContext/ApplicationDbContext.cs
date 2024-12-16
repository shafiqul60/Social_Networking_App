using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Social_Networking_App.Core.Domains;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace Social_Networking_App.Infrastructure.DbContext
{
    public class ApplicationDbContext : IdentityDbContext 
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {
                
        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
        }

        public DbSet<UserInfo> UserInfos { get; set; }
        public DbSet<Event> Events { get; set; }
        public DbSet<FriendNotification> FriendNotifications { get; set; }
        public DbSet<FriendList> FriendLists { get; set; }
    }
}
