namespace Products.Domain.Model.Entities;

public class Category
{
    public int Id { get; set; }
    public string Name { get; private set; }

    protected Category() { }

    public Category(string name)
    {
        SetName(name);
    }

    public void SetName(string name)
    {
        if (string.IsNullOrWhiteSpace(name))
            throw new ArgumentException("Category cannot be empty");

        Name = name.Trim();
    }

}