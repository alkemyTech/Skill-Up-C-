using AlkemyWallet.Core.Interfaces;
using AlkemyWallet.Core.Models.DTO.UserLogin;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace AlkemyWallet.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class LoginController : ControllerBase
    {
        private readonly IAccountServiceJWT _iAccountsServiceJwt;

        public LoginController(IAccountServiceJWT iAccountsServiceJwt)
        {
            _iAccountsServiceJwt = iAccountsServiceJwt;
        }

        [HttpPost()]
        public async Task<IActionResult> AuthenticateAsync(AuthenticationRequestDTO request)
        {
            return Ok(await _iAccountsServiceJwt.AuthenticateAsync(new AuthenticationRequestDTO
            {
                Email = request.Email,
                Password = request.Password,
                Address = GenerateIPAddress()
            }));
        }

        private string GenerateIPAddress()
        {
            if (Request.Headers.ContainsKey("X-Forwared-For"))
            {
                return Request.Headers["X-Forwared-For"];
            }
            else
            {
                return HttpContext.Connection.RemoteIpAddress.MapToIPv4().ToString();
            }
        }
    }
}
