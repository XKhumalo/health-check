namespace HealthCheck.Web.ViewModels
{
    public class LoginUserViewModel
    {
        public string Username { get; set; }
        public string Password { get; set; }

        public bool IsCredentialsIncorrect { get; set; }
    }
}