using System.Net;
using System.Net.Http.Headers;
using Microsoft.Extensions.Configuration;

namespace Client.Services;

public class HttpClientService
{
    private readonly HttpClient _client;
    private readonly string _baseUrl;

    public HttpClientService(IConfiguration configuration)
    {
        var baseUrl = configuration["ApiSettings:BaseUrl"].TrimEnd('/');
        if (string.IsNullOrEmpty(baseUrl))
        {
            throw new Exception("Base url doesn't exist in configuration.");
        }

        _baseUrl = baseUrl;

        var handler = new HttpClientHandler
        {
            AutomaticDecompression = DecompressionMethods.GZip | DecompressionMethods.Deflate,
        };

        _client = new HttpClient(handler)
        {
            BaseAddress = new Uri(_baseUrl),
        };

        _client.DefaultRequestHeaders.Add("Accept", "application/json");
        _client.DefaultRequestHeaders.Add("X-Client-Type", "mobile");
    }

    public async Task AddAuthHeaderAsync()
    {
        var token = await SecureStorage.GetAsync("token");
        if (!string.IsNullOrEmpty(token))
        {
            _client.DefaultRequestHeaders.Authorization =
                new AuthenticationHeaderValue("Bearer", token.Trim());
        }
    }

    public async Task<HttpResponseMessage> GetAsync(string endpoint)
    {
        await AddAuthHeaderAsync();
        return await _client.GetAsync(endpoint);
    }

    public async Task<HttpResponseMessage> PostAsync(string endpoint, HttpContent content)
    {
        await AddAuthHeaderAsync();
        return await _client.PostAsync(endpoint, content);
    }

    public async Task<HttpResponseMessage> PatchAsync(string endpoint, HttpContent content)
    {
        await AddAuthHeaderAsync();
        return await _client.PatchAsync(endpoint, content);
    }

    public async Task<HttpResponseMessage> DeleteAsync(string endpoint)
    {
        await AddAuthHeaderAsync();
        return await _client.DeleteAsync(endpoint);
    }
}