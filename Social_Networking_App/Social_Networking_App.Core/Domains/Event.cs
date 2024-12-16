using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Social_Networking_App.Core.Domains
{
    public class Event
    {
        [Key]
        public int EventId { get; set; }
        public string? EventMessage { get; set; }
        public string? CreatedBy { get; set; }
        public string? UserName { get; set; }
        public DateTime? CreatedDate { get; set; }

    }
}
