using IAM.Domain.Model.ValueObjects;

namespace IAM.Domain.Model.Entities;

public class Role
{
    public int Id { get; private set; }
    public Roles Name { get; private set; }

    protected Role() { }

    public Role(Roles name)
    {
        Name = name;
    }

    public string GetStringName() => Name.ToString();

    public static Role Default() => new Role(Roles.USER);

    public static Role FromName(string name) => new(Enum.Parse<Roles>(name));
}