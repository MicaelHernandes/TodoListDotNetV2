namespace Todo.Domain.Entities;

public class User
{
    public int Id { get; private set; }
    public string Name { get; private set; } = string.Empty;
    public string Email { get; private set; } = string.Empty;
    public string Password { get; private set; } = string.Empty;
    public ICollection<Task> Tasks { get; private set; } = new List<Task>();
    
    protected User() { }
    
    public User(string name, string email, string password)
    {
        Name = name;
        Email = email;
        Password = password;
    }
    
    public void UpdateName(string name)
    {
        Name = name;
    }

    public void ChangePassword(string newPassword)
    {
        Password = newPassword;
    }
}