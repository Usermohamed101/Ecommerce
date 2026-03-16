using Azure.Core;
using Castle.Core.Smtp;
using Ecommerce.Dtos.Auth;
using Ecommerce.Dtos.User;
using Ecommerce.Helper;
using Ecommerce.infrastruction;
using Ecommerce.Services;
using Google.Apis.Auth;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.RateLimiting;
using Microsoft.Extensions.Options;

namespace Ecommerce.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [EnableRateLimiting("loginWindow")]
    public class AuthController(IAuthService auth,IEmailSender emailSender,IConfiguration _configuration,IOptions<EmailOptions> emailOptions) : ControllerBase
    {

        EmailOptions _emailOption = emailOptions.Value;

        [HttpPost("register")]
        public async Task<IActionResult> RegisterAsync(CreateUser user)
        {

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var res = await auth.RegisterAsync(user);
            //if(res.IsAuthenticated&&!string.IsNullOrEmpty(res.RefreshToken))
            // {
            //     SetRefreshTokenInCookies(res.RefreshToken, res.RefreshTokenExpiration);
            //     return Ok(res);
            // }
            if (res.IsAuthenticated)
            {



                var link = Url.Action(

                    nameof(ConfirmEmailAsync),
                    "Auth",
                    new { Email = res.Email, token = res.Msg },
                    Request.Scheme
                    );
             emailSender.Send(_emailOption.SenderEmail, user.Email,"confirm Email", link);
                return Ok("confirm your email");

            }

            return  BadRequest(res);

        }


        [HttpPost("gettoken")]
        public async Task<IActionResult> GetToken (LogInDto user)
        {

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var res = await auth.LoginAsync(user);
            if (res.IsAuthenticated &&!string.IsNullOrEmpty(res.RefreshToken))
            {
                SetRefreshTokenInCookies(res.RefreshToken, res.RefreshTokenExpiration);
                return Ok(res);
            }
            return BadRequest(res);

        }

        [HttpPost("assignRole")]
        public async Task<IActionResult> AssignRole(AsignRoleDto dto)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);
            var res = await auth.AssignRoleAsync(dto);
            return string.IsNullOrEmpty(res) ? Ok(dto) : BadRequest(res);


        }

        [HttpGet("refreshToken")]
        public async Task<IActionResult> RefreshToken()
        {
            var refreshToken = Request.Cookies["refreshToken"];

            var res = await auth.RefreshTokenAsync(refreshToken);

            if (!res.IsAuthenticated)
            {
                return BadRequest(res);
            }
            SetRefreshTokenInCookies(res.RefreshToken, res.RefreshTokenExpiration);
            return Ok(res);

        }



        [HttpGet("revoke-token")]
        public async  Task<IActionResult> RevokeToken(RevokeToken tokenDto)
        {
            var token = tokenDto.Token??Request.Cookies["refreshToken"] ;
            if (string.IsNullOrEmpty(token)) return BadRequest("invalid token");

           var res= await auth.RevokeAsync(token);
            if (!res) return BadRequest("invalid token");

            return Ok();

        }



        [HttpPost("confirm-email")]
        public async Task<IActionResult> ConfirmEmailAsync(string userId,string token)
        {
            var res=await auth.ConfirmEmailAsync(userId, token);
            return res ? Ok("your email is confirmed") : BadRequest("email is not confirmed");
        }



        [HttpPost("forget-password")]
        public async Task<IActionResult> ForgetPassword(ForgotPassword dto)
        {
            var res = await auth.ForgotPasswordAsync(dto);
            if (res.IsSucceded)
            {
                var link = Url.Action(
                    nameof(ResetPassword),
                    "Auth",
                    new { Email = res.Email, Token = res.Token, }
                   , Request.Scheme

                    );


                emailSender.Send(_emailOption.SenderEmail, dto.Email, "forgot password", link);

                return Ok();



            }
            return BadRequest();
        }

        [HttpPost("reset-password")]
        public async Task<IActionResult> ResetPassword(ResetPassword dto)
        {


            var res =await auth.ResetPasswordAsync(dto);

            return res ? Ok() : BadRequest();

        }




        [HttpPost("google-login")]
        public async Task<IActionResult> GoogleLOgIn(GoogleLogInRequest req)
        {



            var res = await auth.GoogleLogIn(req);
            return res.IsAuthenticated ? Ok(res) : BadRequest(res.Msg);



           
        }






        private void SetRefreshTokenInCookies(string rt,DateTime expire)
            
        {

            var cookieOptions = new CookieOptions() {
                Expires = expire,
                HttpOnly=true

            };
            Response.Cookies.Append("refreshToken", rt, cookieOptions);


        }



    }
}
