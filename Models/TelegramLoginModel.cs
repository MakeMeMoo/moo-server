using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Text.Json.Serialization;

namespace moo_server.Core.Entities
{
    public class TelegramLoginModel
    {
        [Required]
        [JsonPropertyName("id")]
        public long Id { get; set; }

        [Required]
        [JsonPropertyName("first_name")]
        public string FirstName { get; set; }

        [Required]
        [JsonPropertyName("last_name")]
        public string LastName { get; set; }

        [Required]
        [JsonPropertyName("username")]
        public string Username { get; set; }

        [Required]
        [JsonPropertyName("photo_url")]
        public string PhotoUrl { get; set; }

        [Required]
        [JsonPropertyName("auth_date")]
        public long AuthDate { get; set; }

        [Required]
        [JsonPropertyName("hash")]
        public string Hash { get; set; }

        public bool CheckAuth()
        {
            var dataCheckString = $"auth_date={AuthDate}\nfirst_name={FirstName}\nid={Id}\nlast_name={LastName}\nphoto_url={PhotoUrl}\nusername={Username}";
            var secretKey = Sha256Hash("1806520485:AAGRvj7kCuuP8SXXEX-3XyDkqhO23_M7o98");
            var myHash = HashHmac(secretKey, Encoding.UTF8.GetBytes(dataCheckString));
            var myHashStr = string.Join("", myHash.Select(i => i.ToString("x2")).ToArray());
            return myHashStr == Hash;
        }

        private byte[] Sha256Hash(string value)
        {
            using (var hasher = SHA256.Create())
            {
                return hasher.ComputeHash(Encoding.UTF8.GetBytes(value));
            }
        }

        private static byte[] HashHmac(byte[] key, byte[] message)
        {
            var hash = new HMACSHA256(key);
            return hash.ComputeHash(message);
        }
    }
}