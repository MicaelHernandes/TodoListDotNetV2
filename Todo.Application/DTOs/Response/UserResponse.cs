namespace Todo.Application.DTOs.Response;

public class UserResponse
{
    public int Id { get; set; }
    public string Name { get; set; }
    public string Email { get; set; }

    public UserResponse(int id, string name, string email)
    {
        Id = id;
        Name = name;
        Email = email;
    }
}