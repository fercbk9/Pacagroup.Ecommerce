using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using Pacagroup.Ecommerce.Application.DTO;
using Pacagroup.Ecommerce.Application.Interface;
using Pacagroup.Ecommerce.Services.WebApi.Helpers;
using Pacagroup.Ecommerce.Transversal.Common;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace Pacagroup.Ecommerce.Services.WebApi.Controllers
{   /// <summary>
    /// 
    /// </summary>
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class UsersController : Controller
    {
        #region Fields
        private readonly IUsersApplication _usersApplication;
        private readonly AppSettings _appSettings;
        #endregion
        #region Ctor
        /// <summary>
        /// Constructor of the class.
        /// </summary>
        /// <param name="usersApplication"></param>
        /// <param name="appSettings"></param>
        public UsersController(IUsersApplication usersApplication,IOptions<AppSettings> appSettings)
        {
            _usersApplication = usersApplication;
            _appSettings = appSettings.Value;
        }
        #endregion
        #region HTTP Methods
        /// <summary>
        /// Autenticacion para login.
        /// </summary>
        /// <param name="usersDTO"></param>
        /// <returns></returns>
        [AllowAnonymous]
        [HttpPost("Authenticate")]
        public IActionResult Authenticate([FromBody] UsersDTO usersDTO)
        {
            var response = _usersApplication.Authenticate(usersDTO.UserName, usersDTO.Password);
            if (response.IsSuccess)
            {
                if (response.Data != null)
                {
                    response.Data.Token = BuildToken(response);
                    return Ok(response);
                }
                else
                {
                    return NotFound(response.Message);
                }
            }
            else
            {
               return BadRequest(response.Message);
            }
        }
        #endregion
        #region Private Methods
        private string BuildToken(Response<UsersDTO> usersResp)
        {
            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes(_appSettings.Secret);
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new System.Security.Claims.ClaimsIdentity(new Claim[]
                {
                    new Claim(ClaimTypes.Name, usersResp.Data.UserId.ToString())
                }),
                Expires = DateTime.UtcNow.AddMinutes(1),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key),SecurityAlgorithms.HmacSha256Signature),
                Issuer = _appSettings.Issuer,
                Audience = _appSettings.Audience
            };
            var token = tokenHandler.CreateToken(tokenDescriptor);
            return tokenHandler.WriteToken(token);
        }
        #endregion
    }
}
