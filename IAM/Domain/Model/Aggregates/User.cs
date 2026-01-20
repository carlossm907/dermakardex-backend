using IAM.Domain.Model.Entities;
using IAM.Domain.Model.ValueObjects;

namespace IAM.Domain.Model.Aggregates;

public class User
{
    public int Id { get; private set; }
    public FullName FullName { get; private set; }
    public PasswordHash PasswordHash { get; private set; }
    public Role Role { get; private set; }
    public Username Username { get; private set; } = null!;

    protected User()
    {
        FullName = null!;
        PasswordHash = null!;
        Role = null!;
        Username = null!;
    }

    private User(FullName fullName, PasswordHash password, Role role, Username username)
    {
        FullName = fullName;
        PasswordHash = password;
        Role = role;
        Username = username;
    }

    public static User Create(
    string fullName,
    string passwordHash,
    Role role,
    string username)
    {
        return new User(
            new FullName(fullName),
            new PasswordHash(passwordHash),
            role ?? Role.Default(),
            new Username(username)
        );
    }

    public void ChangePassword(PasswordHash passwordHash)
    {
        PasswordHash = passwordHash
            ?? throw new ArgumentNullException(nameof(passwordHash));
    }

    public void ChangeRole(Role role)
    {
        Role = role ?? throw new ArgumentNullException(nameof(role));
    }
}