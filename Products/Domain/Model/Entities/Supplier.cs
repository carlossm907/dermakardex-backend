namespace Products.Domain.Model.Entities;

public class Supplier
{
    public int Id { get; set; }
    public string Name { get; private set; }

    protected Supplier() { }

    public Supplier(string name)
    {
        SetName(name);

    }

    public void SetName(string name)
    {
        if (string.IsNullOrWhiteSpace(name))
            throw new ArgumentException("Supplier name is required");

        Name = name.Trim();
    }
}