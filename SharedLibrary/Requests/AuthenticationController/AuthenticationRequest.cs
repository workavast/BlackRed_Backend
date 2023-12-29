namespace SharedLibrary.Requests.AuthenticationController
{
    public class AuthenticationRequest
    {
        public string UserLogin { get; set; }
        public string UserPassword { get; set; }

        public AuthenticationRequest()
        {
            UserLogin = default;
            UserPassword = default;
        }
    
        public AuthenticationRequest(string userLogin, string userPassword)
        {
            UserLogin = userLogin;
            UserPassword = userPassword;
        }
    }
}