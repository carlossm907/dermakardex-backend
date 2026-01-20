namespace IAM.Domain.Model.ValueObjects;

public record FullName
{
    public string Value { get; }

    public FullName(string value)
    {
        if (string.IsNullOrWhiteSpace(value))
            throw new ArgumentException("Full name cannot be empty");

        if (value.Length < 3)
            throw new ArgumentException("Full name is too short");

        Value = value.Trim();
    }

    public override string ToString() => Value;
}