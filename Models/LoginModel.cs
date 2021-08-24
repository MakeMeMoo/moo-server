using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace moo_server.Core.Entities
{
    public class LoginModel
    {
        [Required]
        [JsonPropertyName("username")]
        public string UserName { get; set; }

        [Required]
        [JsonPropertyName("password")]
        public string Password { get; set; }
    }
}