using System.Text.Json.Serialization;

namespace moo_server.Core.Entities
{
    public class RefreshTokenModel
    {
        [JsonPropertyName("refreshToken")]
        public string RefreshToken { get; set; }
    }
}