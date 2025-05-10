using System.Net;
using Microsoft.AspNetCore.Mvc.Testing;

namespace ElsaWeb.Tests.Workflows;

[TestFixture]
public class HttpHelloWorldTests
{
    private WebApplicationFactory<Program> _factory = null!;
    private HttpClient _client = null!;

    [OneTimeSetUp]
    public void OneTimeSetup()
    {
        _factory = new WebApplicationFactory<Program>();
    }

    [SetUp]
    public void Setup()
    {
        _client = _factory.CreateClient();
    }

    [Test]
    public async Task ReturnsHelloWorld_When_HelloWorldIsCalled()
    {
        // Act
        var response = await _client.GetAsync("/workflows/hello-world");

        // Assert
        Assert.That(response.StatusCode, Is.EqualTo(HttpStatusCode.OK));
    }

    [TearDown]
    public void TearDown()
    {
        _client.Dispose();
    }

    [OneTimeTearDown]
    public void OneTimeTearDown()
    {
        _factory.Dispose();
    }
}
