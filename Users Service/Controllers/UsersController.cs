using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;
using System.Net;
using Users_Service.Models;
using Users_Service.Services;

namespace Users_Service.Controllers
{
    [Route("api/users/[action]")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        private readonly Users _usersService;
        private readonly UserValidator _userValidator;

        public UsersController(Users usersService, UserValidator userValidator, SignInManager<User> signInManager)
        {
            _usersService = usersService;
            _userValidator = userValidator;
        }

        [HttpPost]
        [SwaggerResponse((int)HttpStatusCode.OK)]
        [SwaggerResponse((int)HttpStatusCode.BadRequest)]
        [SwaggerResponse((int)HttpStatusCode.Conflict)]
        public async Task<ActionResult<TokenResponse>> register([FromBody] RegisterRequest registerRequest)
        {
            try
            {
                await _userValidator.Validate(registerRequest);
            }
            catch (ArgumentException ex)
            {
                return new BadRequestObjectResult(new ErrorResponse(400, ex.Message));
            }
            return await _usersService.RegisterUser(registerRequest);
        }

        [HttpPost]
        [SwaggerResponse((int)HttpStatusCode.OK)]
        [SwaggerResponse((int)HttpStatusCode.Forbidden)]
        [SwaggerResponse((int)HttpStatusCode.BadRequest)]
        public async Task<ActionResult<TokenResponse>> login([FromBody] LoginRequest loginRequest)
        {
            return await _usersService.LoginUser(loginRequest);
        }

        [Authorize]
        [HttpPost]
        [SwaggerResponse((int)HttpStatusCode.OK)]
        [SwaggerResponse((int)HttpStatusCode.Forbidden)]
        [SwaggerResponse((int)HttpStatusCode.BadRequest)]
        public async Task<ActionResult<TokenResponse>> refresh()
        {
            var userId = User.Claims.ToList()[0].Value;
            var refreshToken = HttpContext.Request.Headers.Authorization.ToString().Substring(7);

            return await _usersService.Refresh(userId, refreshToken);
        }

        [Authorize]
        [HttpPost]
        [SwaggerResponse((int)HttpStatusCode.OK)]
        [SwaggerResponse((int)HttpStatusCode.Forbidden)]
        [SwaggerResponse((int)HttpStatusCode.Unauthorized)]
        public async Task<IActionResult> logout()
        {
            var userId = User.Claims.ToList()[0].Value;

            return await _usersService.LogoutUser(userId);
        }

        [Authorize(Roles = "Абитуриент")]
        [HttpGet]
        [SwaggerResponse((int)HttpStatusCode.OK)]
        [SwaggerResponse((int)HttpStatusCode.Forbidden)]
        [SwaggerResponse((int)HttpStatusCode.Unauthorized)]
        public async Task<ActionResult<UserProfileResponse>> profile()
        {
            var userId = User.Claims.ToList()[0].Value;

            return await _usersService.GetUserProfile(userId);
        }

        [Authorize]
        [HttpPatch]
        [SwaggerResponse((int)HttpStatusCode.OK)]
        [SwaggerResponse((int)HttpStatusCode.Forbidden)]
        [SwaggerResponse((int)HttpStatusCode.Unauthorized)]
        public async Task<IActionResult> profile([FromBody] EditUserRequest editRequest)
        {
            var userId = User.Claims.ToList()[0].Value;

            try
            {
                await _userValidator.Validate(editRequest);
            }
            catch (ArgumentException ex)
            {
                return new BadRequestObjectResult(new ErrorResponse(400, ex.Message));
            }
            return await _usersService.EditUser(userId, editRequest);
        }

        [Authorize]
        [HttpPut]
        [SwaggerResponse((int)HttpStatusCode.OK)]
        [SwaggerResponse((int)HttpStatusCode.BadRequest)]
        [SwaggerResponse((int)HttpStatusCode.Unauthorized)]
        [Route("/api/users/password")]
        public async Task<IActionResult> setNewPassword([FromBody] ChangePasswordRequest changePasswordRequest)
        {
            var userId = User.Claims.ToList()[0].Value;

            try
            {
                await _userValidator.Validate(changePasswordRequest);
            }
            catch (ArgumentException ex)
            {
                return new BadRequestObjectResult(new ErrorResponse(400, ex.Message));
            }
            return await _usersService.SetNewPassword(userId, changePasswordRequest);

        }
    }
}
