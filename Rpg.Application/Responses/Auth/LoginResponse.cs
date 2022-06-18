namespace Rpg.Application.Responses.Auth
{
    public class LoginResponse
    {
        public Guid Id { get; set; }
        public string Username { get; set; }
        public string AccessToken { get; set; }
    }
}
