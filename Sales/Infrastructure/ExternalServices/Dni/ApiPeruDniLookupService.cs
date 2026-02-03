using System.Net.Http.Headers;
using System.Text.Json;
using System.Text.Json.Serialization;
using Microsoft.Extensions.Options;
using Sales.Application.Internal.OutboundServices;

namespace Sales.Infrastructure.ExternalServices.Dni;

public class ApiPeruDniLookupService : IDniLookupService
{
    private readonly HttpClient _http;
    private readonly ApiPeruOptions _options;

    public ApiPeruDniLookupService(HttpClient http, IOptions<ApiPeruOptions> options)
    {
        _http = http;
        _options = options.Value;
    }

    public async Task<string?> GetFullNameByDniAsync(string dni, CancellationToken ct = default)
    {
        dni = (dni ?? "").Trim();

        if (dni.Length != 8 || !dni.All(char.IsDigit))
            throw new ArgumentException("DNI must have exactly 8 digits.");

        using var request = new HttpRequestMessage(HttpMethod.Post, "/api/dni");

        request.Headers.Authorization = new AuthenticationHeaderValue("Bearer", _options.Token);
        request.Content = JsonContent.Create(new { dni });

        using var response = await _http.SendAsync(request, ct);

        if (!response.IsSuccessStatusCode)
            return null;

        var raw = await response.Content.ReadAsStringAsync(ct);

        var payload = JsonSerializer.Deserialize<ApiPeruDniResponse>(raw, new JsonSerializerOptions { PropertyNameCaseInsensitive = true });

        if (payload?.Data is null)
            return null;

        var data = payload.Data;


        if (!string.IsNullOrWhiteSpace(data.NombreCompleto))
            return NormalizeFullName(data.NombreCompleto);


        var parts = new List<string>();

        if (!string.IsNullOrWhiteSpace(data.Nombres))
            parts.Add(data.Nombres.Trim());

        if (!string.IsNullOrWhiteSpace(data.ApellidoPaterno))
            parts.Add(data.ApellidoPaterno.Trim());

        if (!string.IsNullOrWhiteSpace(data.ApellidoMaterno))
            parts.Add(data.ApellidoMaterno.Trim());

        if (parts.Count > 0)
            return string.Join(" ", parts);

        return null;
    }
    private static string NormalizeFullName(string fullName)
    {
        // "APELLIDOS, NOMBRES" => "NOMBRES APELLIDOS"
        if (fullName.Contains(','))
        {
            var parts = fullName
                .Split(',', 2, StringSplitOptions.TrimEntries);

            if (parts.Length == 2)
                return $"{parts[1]} {parts[0]}";
        }

        return fullName.Trim();
    }

    private sealed class ApiPeruDniResponse
    {
        [JsonPropertyName("success")]
        public bool Success { get; set; }

        [JsonPropertyName("data")]
        public ApiPeruDniData? Data { get; set; }
    }

    private sealed class ApiPeruDniData
    {
        [JsonPropertyName("nombre_completo")]
        public string? NombreCompleto { get; set; }

        [JsonPropertyName("nombres")]
        public string? Nombres { get; set; }

        [JsonPropertyName("apellido_paterno")]
        public string? ApellidoPaterno { get; set; }

        [JsonPropertyName("apellido_materno")]
        public string? ApellidoMaterno { get; set; }
    }
}
