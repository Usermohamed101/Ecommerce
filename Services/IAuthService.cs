using Ecommerce.Dtos.Auth;
using Ecommerce.Dtos.User;
using Ecommerce.Helper;
using Ecommerce.infrastruction;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Text;
using System.Security.Claims;
using System.Security.Cryptography;
using Microsoft.OpenApi.Models;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;
using Castle.Core.Smtp;
using System;
using Microsoft.AspNetCore.WebUtilities;
using Google.Apis.Auth;
using Ecommerce.Dtos.Image;
using Ecommerce.Dtos.Service;

namespace Ecommerce.Services
{
    public interface IAuthService
    {
        Task<AuthResultDto> RegisterAsync(CreateUser dto);
        Task<AuthResultDto> LoginAsync(LogInDto dto);
        Task LogoutAsync(string userId);
        Task<AuthResultDto> RefreshTokenAsync(string refreshToken);
        Task<bool> ResetPasswordAsync( ResetPassword dto);

        Task<ForgotPasswordResponse> ForgotPasswordAsync(ForgotPassword dto);

        Task<bool> ConfirmEmailAsync(string userId,string token);
        Task<string> AssignRoleAsync(AsignRoleDto dto);

        Task<bool> RevokeAsync(string refreshToken);

        Task<AuthResultDto> GoogleLogIn(GoogleLogInRequest dto);
    }



    public class AuthService(UserManager<User> userManager,RoleManager<IdentityRole> roleManager
        ,IOptions<jwtOption> _opt,IEmailSender emailSender,IConfiguration config,IUploadImageService imageServ) : IAuthService
    {
        public readonly jwtOption opt = _opt.Value;

        public async Task<string> AssignRoleAsync(AsignRoleDto dto)
        {


            var user = await userManager.FindByIdAsync(dto.UserId);
            if (user == null||await roleManager.RoleExistsAsync(dto.role))
                return "Invalid user or role";

          
            if (await userManager.IsInRoleAsync(user, dto.role)) return "user is assigned already to this role";

           var res= await userManager.AddToRoleAsync(user, dto.role);
            if (res.Succeeded) return "";

            return $"{res.Errors}";




        }

        public async Task<AuthResultDto> LoginAsync(LogInDto dto)
        {




            var user = await userManager.FindByEmailAsync(dto.Email);
            if (user == null ||user.EmailConfirmed|| !await userManager.CheckPasswordAsync(user, dto.Password)) return new AuthResultDto() { Msg = "Email or Password is not correct" };


            var jwtToken = await GenerateJwtToken(user);
            var activeRefreshToken = user.RefreshTokens.FirstOrDefault(r => r.IsActive);
            var authModel= new AuthResultDto()
            {

                IsAuthenticated = true,
                Token = new JwtSecurityTokenHandler().WriteToken(jwtToken),
                //    ExpiresAt = jwtToken.ValidTo,
                UserId = user.Id,
                Email = user.Email,

            };



            
            if (activeRefreshToken != null)
            {
                authModel.RefreshToken = activeRefreshToken.Token;
                authModel.RefreshTokenExpiration = activeRefreshToken.ExpiresOn;

            }
            else
            {
                var newRefreshToken = GenerateRefreshToken();
                authModel.RefreshToken = newRefreshToken.Token;
                authModel.RefreshTokenExpiration = newRefreshToken.ExpiresOn;

                user.RefreshTokens.Add(newRefreshToken);
                await userManager.UpdateAsync(user);

            }






            return authModel;

        }

        public Task LogoutAsync(string userId)
        {
            throw new NotImplementedException();
        }

        public async Task<AuthResultDto> RefreshTokenAsync(string refreshToken)
        {



            var user =await userManager.Users.FirstOrDefaultAsync(u => u.RefreshTokens.Any(r => r.Token == refreshToken));


            if (user == null) {

                return new AuthResultDto()
                {
                    IsAuthenticated = false,
                    Msg = "invalid token"
                };
            
            }
            var authModel = new AuthResultDto()
            {
                Email = user.Email,
                UserId = user.Id,

            };

            var activeRT = user.RefreshTokens.FirstOrDefault(r => r.Token==refreshToken);
            if (!activeRT.IsActive)
            {
                return new AuthResultDto()
                {
                    IsAuthenticated = false,
                    Msg = "invalid token"
                };

            }
            else
            {

                activeRT.RevokedAt = DateTime.UtcNow;
                var newRefreshToken = GenerateRefreshToken();
                var newJwt = await GenerateJwtToken(user);
                authModel.Email = user.Email;
                authModel.UserId = user.Id;
                authModel.IsAuthenticated = true;
                authModel.Token = new JwtSecurityTokenHandler().WriteToken(newJwt);
                authModel.RefreshToken = newRefreshToken.Token;
                authModel.RefreshTokenExpiration = newRefreshToken.ExpiresOn;
                user.RefreshTokens.Add(newRefreshToken);
                await userManager.UpdateAsync(user);

                return authModel;

            }


        }

        public async Task<AuthResultDto> RegisterAsync(CreateUser dto)
        {

            var user = await userManager.FindByEmailAsync(dto.Email);

            if(user!=null &&user.EmailConfirmed==false)
            {

                var tok = await userManager.GenerateEmailConfirmationTokenAsync(user);
                tok = WebEncoders.Base64UrlEncode(Encoding.UTF8.GetBytes(tok));
                return new AuthResultDto()
                {
                    Msg = tok,
                    Email = user.Email,
                 
                    IsAuthenticated = true,
                 
                    UserId = user.Id,
                   

                };
            }

            if (user != null) return new AuthResultDto() { Msg="Email is already exist"};

            var res = new User();
            res.Email = dto.Email;
            res.FName = dto.FName;
            res.LName = dto.LName;
            res.PhoneNumber = dto.PhoneNumber;
            res.UserName = dto.Email;

            if (dto.Image is not null)
            {



                var response = await imageServ.UploadImage(new UploadImageDto { Entity = "ProfilePics", Image = dto.Image });
                if (response.ImageUrl is null)
                    return new AuthResultDto { IsAuthenticated = false, Msg = response.Msg };


                res.ProfilePicPath = response.ImageUrl;

            }

            var result= await  userManager.CreateAsync(res, dto.Password);




            if (!result.Succeeded)
            {
                var errors = string.Empty;
                foreach (var e in result.Errors)
                    errors += $"{e},";

                return new AuthResultDto() { Msg = errors };


            }

            var token = await userManager.GenerateEmailConfirmationTokenAsync(user);

        


            await userManager.AddToRoleAsync(res, "User");

            var jwt =await  GenerateJwtToken(res);
            var refreshToken = GenerateRefreshToken();

            user.RefreshTokens.Add(refreshToken);
            await userManager.UpdateAsync(user);



            return new AuthResultDto()
            {
                Msg = token,
                Email = res.Email,
                //     ExpiresAt = jwt.ValidTo,
                IsAuthenticated = true,
                Token = new JwtSecurityTokenHandler().WriteToken(jwt),
                UserId = res.Id,
                RefreshToken = refreshToken.Token,
                RefreshTokenExpiration = refreshToken.ExpiresOn


            };

        }




        public async Task<bool> RevokeAsync(string refreshToken)
        {


            var user = await userManager.Users.FirstOrDefaultAsync(u => u.RefreshTokens.Any(r => r.Token == refreshToken));

            if (user == null) return false;

            var token = user.RefreshTokens.FirstOrDefault(r=>r.Token==refreshToken);

            if (!token.IsActive) return false;

            token.RevokedAt = DateTime.UtcNow;
            await userManager.UpdateAsync(user);
            return true;



        }
        public async Task<ForgotPasswordResponse> ForgotPasswordAsync(ForgotPassword dto)
        {

            var user = await userManager.FindByEmailAsync(dto.Email);
            if (user is null) return new ForgotPasswordResponse() { IsSucceded = false, Msg = "user  is not found" };

            var token = await userManager.GeneratePasswordResetTokenAsync(user);

            token = WebEncoders.Base64UrlEncode(Encoding.UTF8.GetBytes(token));

            return new ForgotPasswordResponse() { IsSucceded = true, Token = token, Email = dto.Email };



        }



        private async Task<JwtSecurityToken> GenerateJwtToken(User user)
        {


            var userClaims =await userManager.GetClaimsAsync(user);
            var userRoles = await userManager.GetRolesAsync(user);
            var roleClaims = new List<Claim>();

            foreach (var role in userRoles)
                roleClaims.Add(new Claim(ClaimTypes.Role, role));

            var claims = new[]
            {
                new Claim(JwtRegisteredClaimNames.Sub,user.Id),
                new Claim(JwtRegisteredClaimNames.Jti,Guid.NewGuid().ToString()),
                new Claim(JwtRegisteredClaimNames.Email,user.Email),
                new Claim(ClaimTypes.NameIdentifier,user.Id)

            }.Union(userClaims)
            .Union(roleClaims);


            var key = opt.key;
            var secretKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(key));
            var signingCreds = new SigningCredentials(secretKey, SecurityAlgorithms.HmacSha256);


            var jwtSecurityToken = new JwtSecurityToken
            (
                issuer: opt.issuer,
                audience: opt.audience,
                expires: DateTime.UtcNow.AddMinutes(opt.durationInMins),
                claims: claims,
                signingCredentials: signingCreds

            );



            return jwtSecurityToken;


        }


      


        private RefreshToken GenerateRefreshToken()
        {

            var randomBytes = new byte[64];

            using var rnd = RandomNumberGenerator.Create();

            rnd.GetBytes(randomBytes);

            var tk= Convert.ToBase64String(randomBytes);

            return new RefreshToken()
            {

                Token = tk,
                CreatedAt = DateTime.UtcNow,
                ExpiresOn = DateTime.UtcNow.AddDays(2)
            };


        }

        public async Task<bool> ConfirmEmailAsync(string Email,string token)
        {
            var user = await userManager.FindByEmailAsync(Email);
            if (user is null) return false;

            var tok = Encoding.UTF8.GetString(WebEncoders.Base64UrlDecode(token));

            var res = await userManager.ConfirmEmailAsync(user, tok);
            return res.Succeeded;
        }

        public async Task<bool> ResetPasswordAsync(ResetPassword dto)
        {
            var user = await userManager.FindByEmailAsync(dto.Email);
            if (user is null) return false;
            var tok = Encoding.UTF8.GetString(WebEncoders.Base64UrlDecode(dto.Token));

            var res = await userManager.ResetPasswordAsync(user, tok, dto.NewPassword);
            return res.Succeeded;
        }



        public async Task<AuthResultDto> GoogleLogIn(GoogleLogInRequest dto)
        {

            var settings = new GoogleJsonWebSignature.ValidationSettings()
            {
                Audience = new List<string>
              {
                  config["Authentication:Google:ClientId"]
              }
            };

            var payload = await GoogleJsonWebSignature.ValidateAsync(dto.Token, settings);
            if (payload == null) return new AuthResultDto { IsAuthenticated = false ,Msg="invalid token"};

            var user = await userManager.FindByEmailAsync(payload.Email);

            if(user==null)
            {
                user = new User()
                {
                    Email = payload.Email,
                    UserName = payload.Email,
                    FName = payload.Name,
                    LName = payload.FamilyName,



                };
                var res = await userManager.CreateAsync(user);
                if (!res.Succeeded) return new AuthResultDto { IsAuthenticated = false, Msg = "invalid payload" };
            }



            var jwtToken =await GenerateJwtToken(user);
            var activeRefreshTokens = user.RefreshTokens.FirstOrDefault(r => r.IsActive);
            if (activeRefreshTokens is null)
            {
                activeRefreshTokens = GenerateRefreshToken();

            }
            return new AuthResultDto
            {
                Email = user.Email,
                UserId = user.Id,
                Token = new JwtSecurityTokenHandler().WriteToken(jwtToken),
                RefreshTokenExpiration = activeRefreshTokens.ExpiresOn,
                IsAuthenticated=true


            };


        }
    }
}
