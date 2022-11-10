﻿using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using AlkemyWallet.Core.Helper;
using AlkemyWallet.Core.Interfaces;
using AlkemyWallet.Core.Models.DTO;
using AlkemyWallet.Core.Models.DTO.UserLogin;
using AlkemyWallet.Entities.JWT;
using AlkemyWallet.Repositories.Interfaces;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using Microsoft.AspNetCore.Mvc;
using AlkemyWallet.Entities;

namespace AlkemyWallet.Core.Services;

public class AccountServiceJWT : IAccountServiceJWT
{
    private readonly IUserRepository _iUserRepository;
    private readonly JWTSettings _jWTSettings;
    private readonly IUnitOfWork _unitOfWork;
    private readonly UserManager<ApplicationUser> _userManager;

    public AccountServiceJWT(IUserRepository iUserRepository, UserManager<ApplicationUser> userManager,
        IOptions<JWTSettings> jWTSettings, IUnitOfWork unitOfWork)
    {
        _iUserRepository = iUserRepository;
        _userManager = userManager;
        _jWTSettings = jWTSettings.Value;
        _unitOfWork = unitOfWork;
    }

    public async Task<Response<AuthenticationResponseDTO>> AuthenticateAsync(AuthenticationRequestDTO request)
    {
        AuthenticationResponseDTO responseSinAutenticar = new();
        var user = await _iUserRepository.GetUserByEmail(request.Email, request.Password);

        if (user is null)
            return new Response<AuthenticationResponseDTO>(responseSinAutenticar, false,
                "El email o la contraseña no coinciden con lo registrado en la base de datos");

        ApplicationUser userIdentity = new()
        {
            Email = user.Email,
            Id = user.Id.ToString(),
            UserName = user.First_name,
            RolId = user.Rol_id
        };

        var jwtSecurityToken = await GenerateJWTToken(userIdentity);
        AuthenticationResponseDTO response = new();
        response.Id = user.Id.ToString();
        response.JWToken = new JwtSecurityTokenHandler().WriteToken(jwtSecurityToken);
        response.Email = user.Email;
        response.Name = user.First_name;

        List<string> rolesList = new();
        rolesList.Add(user.Rol_id.ToString());
        response.Roles = rolesList.ToList();
        response.IsVerified = true;

        return new Response<AuthenticationResponseDTO>(response, $"Usuario autenticado {user.First_name}");
    }

    public async Task<Response<AuthenticatedUserDTO>> AuthenticatedUserAsync(List<Claim> userdataTokenList)
    {
        Task<Response<AuthenticatedUserDTO>> task = Task.Run(() =>
        {
            AuthenticatedUserDTO authenticatedUserDTO = new()
            {
                Id = int.Parse(userdataTokenList[3].Value),
                Email = userdataTokenList[2].Value,
                Name = userdataTokenList[0].Value,
                Rol = userdataTokenList[5].Value
            };

            return new Response<AuthenticatedUserDTO>(authenticatedUserDTO, "Datos de usuario loggueado");
        });
        return await task;
    }

    private async Task<JwtSecurityToken> GenerateJWTToken(ApplicationUser user)
    {
        IList<Claim> userClaims = await _userManager.GetClaimsAsync(user);
        var rol = await _unitOfWork.RoleRepository.GetById(user.RolId);
        List<string> roles = new() { rol.Name };

        List<Claim> roleClaims = new();

        roleClaims.Add(new Claim("roles", roles[0]));

        var ipAddress = ApiHelper.GetIpAddress();

        var claims = new[]
            {
                new Claim(JwtRegisteredClaimNames.Sub, user.UserName),
                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
                new Claim(JwtRegisteredClaimNames.Email, user.Email),
                new Claim("uid", user.Id),
                new Claim("ip", ipAddress)
            }
            .Union(userClaims)
            .Union(roleClaims);

        var symmetricSecurityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_jWTSettings.Key));
        var signingCredentials = new SigningCredentials(symmetricSecurityKey, SecurityAlgorithms.HmacSha256);

        var jwtSecurityToken = new JwtSecurityToken(
            _jWTSettings.Issuer,
            _jWTSettings.Audience,
            claims,
            expires: DateTime.Now.AddMinutes(_jWTSettings.DurationInMinutes),
            signingCredentials: signingCredentials
        );

        return jwtSecurityToken;
    }
}