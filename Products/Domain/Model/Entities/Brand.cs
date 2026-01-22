using Microsoft.IdentityModel.Protocols.OpenIdConnect;

namespace Products.Domain.Model.Entitites;

public class Brand
{
    public int Id { get; set; }
    public string Name { get; private set; }

    public Brand(string name)
    {
        SetName(name);
    }

    public void SetName(string name)
    {
        if (string.IsNullOrWhiteSpace(name))
            throw new ArgumentException("Brand name cannot be empty");

        Name = name.Trim();
    }
}