namespace Products.Domain.Model.Entities;

public class Laboratory
{
    public int Id { get; private set; }
    public string Name { get; private set; }

    protected Laboratory() { }

    public Laboratory(string name)
    {
        SetName(name);
    }

    public void SetName(string name)
    {
        if (string.IsNullOrWhiteSpace(name))
        {
            throw new ArgumentException("Laboratory name cannot be empty");
        }
        Name = name.Trim();
    }
}