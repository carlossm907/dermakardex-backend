using Sales.Interfaces.REST.Resources;

namespace Sales.Interfaces.REST.Transform;

public static class DniLookupResourceFromStringAssembler
{
    public static DniLookupResource ToResourceFromFullName(string fullName)
    {
        return new DniLookupResource(fullName);
    }
}