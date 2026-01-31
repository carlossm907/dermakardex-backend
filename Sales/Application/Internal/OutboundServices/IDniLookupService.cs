namespace Sales.Application.Internal.OutboundServices;

public interface IDniLookupService
{
    Task<string?> GetFullNameByDniAsync(string dni, CancellationToken ct = default);
}