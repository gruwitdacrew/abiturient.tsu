using Microsoft.AspNetCore.Mvc;
using Users_Service.Models;
using Microsoft.AspNetCore.Identity;
using EasyNetQ;
using Notification_Service.Models;
using Users_Service.DBContext;
using EasyNetQ.Topology;

namespace Users_Service.Services
{
    public class Users
    {
        private readonly UserManager<User> _userManager;
        private readonly TokenGenerator _tokenGenerator;

        public Users(
            TokenGenerator tokenGenerator,
            UserManager<User> userManager
            )
        {
            _tokenGenerator = tokenGenerator;
            _userManager = userManager;
        }

        public async Task<ActionResult<TokenResponse>> RegisterUser(RegisterRequest registerRequest)
        {
            if (await _userManager.FindByEmailAsync(registerRequest.email) != null)
            {
                return new ConflictObjectResult(new ErrorResponse(409, "Пользователь с такой почтой уже существует"));
            }
            User user = new User(registerRequest);

            var result = await _userManager.CreateAsync(user, registerRequest.password);
            if (!result.Succeeded)
            {
                var errorMessage = "Регистрациция провалена из-за:";
                foreach (var error in result.Errors)
                {
                    errorMessage += " # " + error.Description;
                }
                return new BadRequestObjectResult(new ErrorResponse(400, errorMessage));
            }

            await _userManager.AddToRoleAsync(user, "ABITURIENT");

            var refreshToken = await _tokenGenerator.GenerateRefreshToken(user);
            await _userManager.SetAuthenticationTokenAsync(user, "Abiturient.tsu", "RefreshToken", refreshToken);

            string accessToken = await _tokenGenerator.GenerateAccessToken(user, (List<string>)await _userManager.GetRolesAsync(user));

            var rabbit = RabbitHutch.CreateBus("host=localhost");
            await rabbit.PubSub.PublishAsync(user);

            return new OkObjectResult(new TokenResponse(refreshToken, accessToken));
        }

        public async Task<IActionResult> LoginUser(LoginRequest loginRequest)
        {
            User user = await _userManager.FindByEmailAsync(loginRequest.email);

            if (user == null)
            {
                return new NotFoundObjectResult(new ErrorResponse(404, "Пользователь с такой почтой не найден"));
            }
            else if (await _userManager.CheckPasswordAsync(user, loginRequest.password))
            {
                await _userManager.RemoveAuthenticationTokenAsync(user, "Abiturient.tsu", "RefreshToken");

                var refreshToken = await _tokenGenerator.GenerateRefreshToken(user);
                await _userManager.SetAuthenticationTokenAsync(user, "Abiturient.tsu", "RefreshToken", refreshToken);

                string accessToken = await _tokenGenerator.GenerateAccessToken(user, (List<string>)await _userManager.GetRolesAsync(user));

                return new OkObjectResult(new TokenResponse(refreshToken, accessToken));
            }
            else
            {
                return new UnauthorizedObjectResult(new ErrorResponse(401, "Неправильный пароль"));
            }
        }

        public async Task<IActionResult> LogoutUser(string userId)
        {
            User user = await _userManager.FindByIdAsync(userId);

            await _userManager.RemoveAuthenticationTokenAsync(user, "Abiturient.tsu", "RefreshToken");

            return new OkObjectResult("");
        }

        public async Task<ActionResult<TokenResponse>> Refresh(string userId, string refreshToken)
        {
            User user = await _userManager.FindByIdAsync(userId);

            if (refreshToken == await _userManager.GetAuthenticationTokenAsync(user, "Abiturient.tsu", "RefreshToken"))
            {
                await _userManager.RemoveAuthenticationTokenAsync(user, "Abiturient.tsu", "RefreshToken");

                var newRefreshToken = await _tokenGenerator.GenerateRefreshToken(user);
                await _userManager.SetAuthenticationTokenAsync(user, "Abiturient.tsu", "RefreshToken", newRefreshToken);

                string accessToken = await _tokenGenerator.GenerateAccessToken(user, (List<string>)await _userManager.GetRolesAsync(user));

                return new OkObjectResult(new TokenResponse(newRefreshToken, accessToken));
            }
            else return new UnauthorizedObjectResult(new ErrorResponse(401, "Неподходящий refresh token"));
        }
        public async Task<ActionResult<UserProfileResponse>> GetUserProfile(string userId)
        {
            User user = await _userManager.FindByIdAsync(userId);

            return new OkObjectResult(new UserProfileResponse(user, (List<string>)await _userManager.GetRolesAsync(user)));
        }


        public async Task<IActionResult> EditUser(string userId, EditUserRequest editUserRequest)
        {
            User user = await _userManager.FindByIdAsync(userId);

            if (editUserRequest.gender != null)
            {
                user.gender = editUserRequest.gender;
            }
            if (editUserRequest.email != null)
            {
                user.Email = editUserRequest.email;
            }
            if (editUserRequest.nationality != null)
            {
                user.nationality = editUserRequest.nationality;
            }

            if (editUserRequest.birthDate != null)
            {
                user.birthDate = editUserRequest.birthDate;
            }

            if (editUserRequest.phone != null)
            {
                user.PhoneNumber = editUserRequest.phone;
            }

            if (editUserRequest.fullName != null)
            {
                user.fullName = editUserRequest.fullName;
            }

            await _userManager.UpdateAsync(user);

            var rabbit = RabbitHutch.CreateBus("host=localhost");
            await rabbit.PubSub.PublishAsync(user);

            return new OkObjectResult("");
        }

        public async Task<IActionResult> SetRolesToUser(string userId, List<string> roles, string adminUserId)
        {
            User user = await _userManager.FindByIdAsync(userId);

            string message = "Вам выдали новые роли: ";
            foreach (string role in roles)
            {
                await _userManager.AddToRoleAsync(user, role); //"ABITURIENT"
                message += role + ", ";
            }

            Notification notification = new Notification(user.Email, message.Substring(0, message.Length - 2), "Новая роль - новые обязанности");
            var rabbit = RabbitHutch.CreateBus("host=localhost");
            await rabbit.PubSub.PublishAsync(notification);

            return new OkObjectResult("");
        }

        public async Task<IActionResult> SetNewPassword(string userId, ChangePasswordRequest changePasswordRequest)
        {
            User user = await _userManager.FindByIdAsync(userId);

            if (await _userManager.CheckPasswordAsync(user, changePasswordRequest.password))
            {
                await _userManager.ChangePasswordAsync(user, changePasswordRequest.password, changePasswordRequest.newPassword);
                return new OkObjectResult("");
            }
            else return new BadRequestObjectResult(new ErrorResponse(400, "Неправильный пароль"));
        }
    }
}
