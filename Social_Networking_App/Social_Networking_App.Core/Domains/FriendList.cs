using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Social_Networking_App.Core.Domains
{
    public class FriendList
    {
        [Key]
        public int FriendListId { get; set; }
        public string UserId { get; set; }
        public string FriendId { get; set; }
    }
}
