namespace Todo.Application.DTOs.Response.User;

public class UserAuthResponse
{
    public string Token { get; set; }

    public UserAuthResponse(string token)
    {
        Token = token;
    }
}