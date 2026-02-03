namespace Sales.Infrastructure.ExternalServices.Dni;

public class ApiPeruOptions
{
    public string BaseUrl { get; set; } = "https://apiperu.dev";
    public string Token { get; set; } = string.Empty;
}