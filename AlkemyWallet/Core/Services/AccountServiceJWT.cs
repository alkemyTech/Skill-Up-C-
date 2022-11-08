using AlkemyWallet.Core.Helper;
using AlkemyWallet.Core.Interfaces;
using AlkemyWallet.Core.Models.DTO.UserLogin;
using AlkemyWallet.Entities.JWT;
using AlkemyWallet.Entities;
using AlkemyWallet.Repositories.Interfaces;
using Microsoft.AspNetCore.Identity;
using AlkemyWallet.Core.Helper.ExceptionGenerics;
using System.IdentityModel.Tokens.Jwt;
using Microsoft.IdentityModel.Tokens;
using System.Security.Claims;
using System.Text;
using System.Security.Cryptography;
using Microsoft.Extensions.Options;

namespace AlkemyWallet.Core.Services
{
    public class AccountServiceJWT : IAccountServiceJWT
    {
        private readonly IUserRepository _iUserRepository;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly SignInManager<ApplicationUser> _signInManager;
        private readonly JWTSettings _jWTSettings;

        public AccountServiceJWT(IUserRepository iUserRepository, UserManager<ApplicationUser> userManager, RoleManager<IdentityRole> roleManager, SignInManager<ApplicationUser> signInManager, IOptions<JWTSettings> jWTSettings)
        {
            _iUserRepository = iUserRepository;
            _userManager = userManager;
            _roleManager = roleManager;
            _signInManager = signInManager;
            _jWTSettings = jWTSettings.Value;
        }

        public async Task<Response<AuthenticationResponseDTO>> AuthenticateAsync(AuthenticationRequestDTO request)
        {
            AuthenticationResponseDTO responseSinAutenticar = new();
            var user = _iUserRepository.GetAll().Result.ToList().Where(u => u.Email.Equals(request.Email)).FirstOrDefault();

            if (user is null)
            {
                return new Response<AuthenticationResponseDTO>(responseSinAutenticar, $"No hay registrada una cuenta con el email {request.Email}");
            }

            var userIdentity = new ApplicationUser()
            {
                Email = user.Email,
                Id = user.Id.ToString(),
                UserName = user.First_name
            };

            if (user.Rol_id.Equals(1) || user.Rol_id.Equals(2))
            {
                var result = _iUserRepository.GetAll().Result.ToList().Where(u => u.Password.Equals(request.Password)).FirstOrDefault();
                if (result is null)
                {
                    throw new ApiException($"Las credenciales no son válidas {request.Email}");
                }

                JwtSecurityToken jwtSecurityToken = await GenerateJWTToken(userIdentity);
                AuthenticationResponseDTO response = new();
                response.Id = user.Id.ToString();
                response.JWToken = new JwtSecurityTokenHandler().WriteToken(jwtSecurityToken);
                response.Email = user.Email;
                response.Name = user.First_name;

                List<string> rolesList = new();
                rolesList.Add(user.Rol_id.ToString());
                response.Roles = rolesList.ToList();
                response.IsVerified = true;

                RefreshTokenDTO refreshToken = GenerateRefreshToken(request.Address);

                response.RefreshToken = refreshToken.Token;

                return new Response<AuthenticationResponseDTO>(response, $"Usuario autenticado {user.First_name}");
            }
            else
            {
                return new Response<AuthenticationResponseDTO>(responseSinAutenticar, $"El usuario no se puede autenticar {user.First_name}");
            }

        }

        private async Task<JwtSecurityToken> GenerateJWTToken(ApplicationUser user)
        {
            IList<Claim> userClaims = await _userManager.GetClaimsAsync(user);
            IList<string> roles = await _userManager.GetRolesAsync(user);

            List<Claim> roleClaims = new();

            for (int i = 0; i < roles.Count; i++)
            {
                roleClaims.Add(new Claim("roles", roles[i]));
            }

            string ipAddress = ApiHelper.GetIpAddress();

            var claims = new[]
            {
                new Claim(JwtRegisteredClaimNames.Sub, user.UserName),
                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
                new Claim(JwtRegisteredClaimNames.Email, user.Email),
                new Claim("uid", user.Id.ToString()),
                new Claim("ip", ipAddress)
            }
            .Union(userClaims)
            .Union(roleClaims);

            SymmetricSecurityKey symmetricSecurityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_jWTSettings.Key));
            SigningCredentials signingCredentials = new SigningCredentials(symmetricSecurityKey, SecurityAlgorithms.HmacSha256);

            JwtSecurityToken jwtSecurityToken = new JwtSecurityToken(
                    issuer: _jWTSettings.Issuer,
                    audience: _jWTSettings.Audience,
                    claims: claims,
                    expires: DateTime.Now.AddMinutes(_jWTSettings.DurationInMinutes),
                    signingCredentials: signingCredentials
                );

            return jwtSecurityToken;
        }

        private RefreshTokenDTO GenerateRefreshToken(string ipAddress)
        {
            return new RefreshTokenDTO
            {
                Token = RamdomTokenString(),
                Expires = DateTime.Now.AddDays(7),
                Created = DateTime.Now,
                CreatedByIp = ipAddress
            };
        }

        private string RamdomTokenString()
        {
            using RNGCryptoServiceProvider rngCryptoServiceProvider = new();
            Byte[] ramdomBytes = new Byte[40];
            rngCryptoServiceProvider.GetBytes(ramdomBytes);

            return BitConverter.ToString(ramdomBytes).Replace("-", "");
        }
    }
}
