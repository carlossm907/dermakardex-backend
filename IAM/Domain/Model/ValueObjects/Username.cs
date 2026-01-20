namespace IAM.Domain.Model.ValueObjects;

public record Username
{
    public string Value { get; }

    public Username(string value)
    {
        if (string.IsNullOrWhiteSpace(value))
            throw new ArgumentException("Username cannot be empty");

        if (value.Length < 3)
            throw new ArgumentException("Username is too short");

        Value = value.Trim().ToLowerInvariant();
    }

    public override string ToString() => Value;
}