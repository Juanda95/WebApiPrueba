using Application.DTOs.Users;
using Application.Enums;
using Application.Exceptions;
using Application.Interfaces;
using Application.Wrappers;
using Domain.Settings;
using Identity.Helpers;
using Identity.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace Identity.Services
{
    public class AccountService : IAccountService
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly JWTSettings _jWTSettings;
        private RoleManager<IdentityRole> _roleManager;
        private SignInManager<ApplicationUser> _signInManager;

        public AccountService(UserManager<ApplicationUser> userManager, IOptions<JWTSettings> jWTSettings, RoleManager<IdentityRole> roleManager, SignInManager<ApplicationUser> signInManager)
        {
            _userManager = userManager;
            _jWTSettings = jWTSettings.Value;
            _roleManager = roleManager;
            _signInManager = signInManager;
        }

        public async Task<Response<AuthenticationResponse>> AuthenticateAsync(AuthenticationRequest request, string ipAddress)
        {
            var usuario =await _userManager.FindByEmailAsync(request.Email);
            if (usuario == null)
            {
                throw new ApiException($"No hay una cuenta registrada con el email {request.Email}.");
            }
            var result = await _signInManager.PasswordSignInAsync(usuario.UserName, request.Password, false, lockoutOnFailure: false);
            if (!result.Succeeded) 
            {
                throw new ApiException($"Las credenciales del usuario no son validas  {request.Email}.");
            }
            JwtSecurityToken jwtSecurityToken = await GenerateJWTToken(usuario);

            AuthenticationResponse response = new AuthenticationResponse();
            response.Id = usuario.Id;
            response.JWToken = new JwtSecurityTokenHandler().WriteToken(jwtSecurityToken);
            response.UserName= usuario.UserName;

            var rolesList = await _userManager.GetRolesAsync(usuario).ConfigureAwait(false);
            response.Roles = rolesList.ToList();
            response.IsVerified = usuario.EmailConfirmed;

            var refreshToken = GenerateRefreshtoken(ipAddress);

            response.RefreshToken = refreshToken.Token;
            return new Response<AuthenticationResponse>(response, $"usuario Autenticado {usuario.UserName}");

        }

        public async Task<Response<string>> RegisterAsync(RegisterRequest request, string origin)
        {
            var usuarioConElMismoUserName = await _userManager.FindByNameAsync(request.UserName);
            if (usuarioConElMismoUserName != null)
            {
                throw new ApiException($"El usuario {request.UserName} ya se encuentra registrado.");
            }
            var usuario = new ApplicationUser
            {
                Email = request.Email,
                Nombre= request.Nombre,
                Apellido= request.Apellido,
                UserName = request.UserName,
                EmailConfirmed = true,
                PhoneNumberConfirmed = true,
            };

            var usuarioConElMismoCorreo = await _userManager.FindByEmailAsync(request.Email);
            if(usuarioConElMismoCorreo != null)
            {
                throw new ApiException($"El email {request.Email} ya se encuentra registrado.");
            }
            else
            {
                var result = await _userManager.CreateAsync(usuario, request.Password);
                if (result.Succeeded) 
                {
                    await _userManager.AddToRoleAsync(usuario, Roles.Basic.ToString());
                    return new Response<string>(usuario.Id, messages: $"Usuario registrado exitosamente. {request.UserName}" );
                }
                else
                {
                    throw new ApiException($"{result.Errors}");
                }
            }
        }

        private async Task<JwtSecurityToken> GenerateJWTToken(ApplicationUser usuario)
        {
            var userclaims = await _userManager.GetClaimsAsync(usuario);
            var roles = await _userManager.GetRolesAsync(usuario);

            var roleClaims = new List<Claim>();

            for (int i = 0; i < roles.Count; i++)
            {
                roleClaims.Add(new Claim("roles", roles[i]));
            }

            string ipaddress = IpHelper.GetIpAddress();
            var claims = new[]
            {
                new Claim(JwtRegisteredClaimNames.Sub,usuario.UserName),
                new Claim(JwtRegisteredClaimNames.Jti,Guid.NewGuid().ToString()),
                new Claim(JwtRegisteredClaimNames.Email,usuario.Email),
                new Claim("uid",usuario.Id),
                new Claim("ip",ipaddress)
            }
            .Union(userclaims)
            .Union(roleClaims);

            var symetricSecuritykey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_jWTSettings.key ?? "123456"));
            var signingCredentials = new SigningCredentials(symetricSecuritykey, SecurityAlgorithms.HmacSha256);
            var jwtSecuritytoken = new JwtSecurityToken(
                issuer: _jWTSettings.Issuer,
                audience:_jWTSettings.Audience,
                claims:claims,
                expires: DateTime.Now.AddMinutes(_jWTSettings.DurationInMinutes),
                signingCredentials: signingCredentials
                
                );
            return jwtSecuritytoken;
        }
        private RefreshToken GenerateRefreshtoken(string ipAddress)
        {
            return new RefreshToken
            {
                Token = RandomTokenString(),
                Expires = DateTime.Now.AddDays(7),
                Created = DateTime.Now,
                CreatedByIp = ipAddress,
            };

        }

        private string RandomTokenString()
        {
            using var rngCryptoServiceProvider = new RNGCryptoServiceProvider();
            var ramdomBytes = new byte[40];
            rngCryptoServiceProvider.GetBytes(ramdomBytes);
            return BitConverter.ToString(ramdomBytes).Replace("-", "");
        }
    }
}
