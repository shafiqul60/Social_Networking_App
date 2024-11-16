using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Social_Networking_App.Core.Domains
{
    public class UserInfo
    {
        [Key]
        public int UserInfoId { get; set; }
        [Required]
        public string FirstName { get; set; }
        [Required]
        public string LastName { get; set; }
        [DataType(DataType.Date)]
        public DateTime DateOfBirth { get; set; }
        [Required]
        public string Gender { get; set; }
        [RegularExpression(@"^(\([0-9]{3}\)|[0-9]{3})[\s\-]?[\0-9]{3}[\s\-]?[0-9]{4}$", ErrorMessage = "Invalid Phone Number.")]
        public string PhoneNumber { get; set; }

        public string? AboutMe { get; set; }
        public string UserId { get; set; }
        public bool IsOnline { get; set; }
        public string? JobTitle { get; set; }
        public string? ImgUrl { get; set; }
    }
}
