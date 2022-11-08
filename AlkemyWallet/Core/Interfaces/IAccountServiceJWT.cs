using AlkemyWallet.Core.Helper;
using AlkemyWallet.Core.Models.DTO.UserLogin;

namespace AlkemyWallet.Core.Interfaces
{
    public interface IAccountServiceJWT
    {
        Task<Response<AuthenticationResponseDTO>> AuthenticateAsync(AuthenticationRequestDTO request);
    }
}
