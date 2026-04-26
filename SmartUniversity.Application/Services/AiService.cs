using SmartUniversity.Application.DTOs.AI;
using System.Text;
using System.Text.Json;

public class AiService
{
    private readonly HttpClient _http;

    public AiService(HttpClient http)
    {
        _http = http;
    }

    public async Task<List<AiResultDto>> AnalyzeStudents(List<StudentAiDto> students)
    {
        var json = JsonSerializer.Serialize(students, new JsonSerializerOptions
        {
            PropertyNamingPolicy = JsonNamingPolicy.CamelCase
        });

        var content = new StringContent(json, Encoding.UTF8, "application/json");

        var response = await _http.PostAsync(
            "http://127.0.0.1:8000/student-analysis",
            content
        );

        response.EnsureSuccessStatusCode();

        var result = await response.Content.ReadAsStringAsync();

        return JsonSerializer.Deserialize<List<AiResultDto>>(
            result,
            new JsonSerializerOptions { PropertyNameCaseInsensitive = true }
        );
    }
}