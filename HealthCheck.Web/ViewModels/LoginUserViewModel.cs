using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace HealthCheck.Web.ViewModels
{
    public class LoginUserViewModel
    {
        public string Username { get; set; }
        public string Password { get; set; }

        [DisplayName("Guest Name")]
        public string GuestName { get; set; }

        [Display(Name = "Session Key")]
        [MaxLength(6, ErrorMessage = "Session Key must be 6 characters")]
        [MinLength(6)]
        public string SessionKey { get; set; }

        public bool IsCredentialsIncorrect { get; set; }
    }
}