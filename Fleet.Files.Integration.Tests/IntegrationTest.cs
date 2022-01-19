using System.Net.Http;
using Microsoft.Extensions.Configuration;
using NUnit.Framework;

namespace Fleet.Files.Tests;

public class IntegrationTest
{
    private readonly IConfigurationRoot _configuration;
    protected readonly HttpClient _httpClient;

    public IntegrationTest()
    {
        _configuration = new ConfigurationBuilder()
            .SetBasePath(TestContext.CurrentContext.WorkDirectory)
            .AddJsonFile("appsettings.json", optional: true, reloadOnChange: true)
            .AddEnvironmentVariables()
            .Build();
        _httpClient = new HttpClient()
        {
            BaseAddress = new System.Uri(_configuration.GetSection("HttpClient")
                .GetValue<string>("Fleet:BaseUrl"))
        };
    }
}