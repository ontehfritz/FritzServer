using System.Text.Json.Serialization;

namespace Www;

public class Config
{
    [JsonPropertyName("domain")]
    public string Domain { get; set; }
    [JsonPropertyName("port")]
    public int Port { get; set; }
    [JsonPropertyName("rootDir")]
    public string RootDir { get; set; }
    [JsonPropertyName("index")]
    public string Index { get; set; }
}