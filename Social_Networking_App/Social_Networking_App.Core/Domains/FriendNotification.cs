using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Social_Networking_App.Core.Domains
{
    public class FriendNotification
    {
        [Key]
        public int NotificationId { get; set; }
        public string FromRequest { get; set; }
        public string ToRequest { get; set; }
        public string NotiMessage { get; set; }
        public bool IsSeen { get; set; }
        public DateTime CreatedDate { get; set; }
    }
}
