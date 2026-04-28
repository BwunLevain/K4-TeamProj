using Microsoft.AspNetCore.Mvc;
using OllamaSharp;
using ProxyAPI.DTOs;

[ApiController]
[Route("api/[controller]")]
public class AiController : ControllerBase
{
    private readonly IOllamaApiClient _ollama;
    private readonly IHttpClientFactory _httpClientFactory;

    public AiController(IOllamaApiClient ollama, IHttpClientFactory httpClientFactory)
    {
        _ollama = ollama;
        _httpClientFactory = httpClientFactory;
    }

    [HttpPost("ask")]
    public async Task<IActionResult> AskAi([FromBody] AiRequest request)
    {
        _ollama.SelectedModel = "gemma3:4b";
        var fullResponse = new System.Text.StringBuilder();

        await foreach (var stream in _ollama.GenerateAsync(request.Prompt))
        {
            fullResponse.Append(stream.Response);
        }

        return Ok(new { GeneratedMessage = fullResponse.ToString() });
    }
}