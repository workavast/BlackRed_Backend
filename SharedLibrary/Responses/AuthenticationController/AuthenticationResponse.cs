namespace SharedLibrary.Responses.AuthenticationController
{
    public class AuthenticationResponse
    {
        public string Toke { get; set; }
    
        public AuthenticationResponse()
        {
            Toke = default;
        }

        public AuthenticationResponse(string toke)
        {
            Toke = toke;
        }
    }
}