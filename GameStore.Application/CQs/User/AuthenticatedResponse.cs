namespace GameStore.Application.CQs.User;

public class AuthenticatedResponse
{
    public string Token { get; set; }
    public string RefreshToken { get; set; }
}