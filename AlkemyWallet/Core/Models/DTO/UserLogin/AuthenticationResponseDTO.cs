using System.Text.Json.Serialization;

namespace AlkemyWallet.Core.Models.DTO.UserLogin
{
    public class AuthenticationResponseDTO
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public List<string> Roles { get; set; }
        public bool IsVerified { get; set; }
        public string JWToken { get; set; }
        [JsonIgnore]
        public string RefreshToken { get; set; }
    }
}
