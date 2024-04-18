using Microsoft.AspNetCore.Mvc;
using Users_Service.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using EasyNetQ;
using Notification_Service.Models;
using Users_Service.DBContext;
using EasyNetQ.Topology;

namespace Users_Service.Services
{
    public class Users
    {
        private readonly AppDbContext _context;
        private readonly UserManager<User> _userManager;
        private readonly TokenGenerator _accessTokenGenerator;

        public Users(AppDbContext context,
            TokenGenerator accessTokenGenerator,
            UserManager<User> userManager
            )
        {
            _context = context;
            _accessTokenGenerator = accessTokenGenerator;
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

            var refreshToken = await _userManager.GenerateUserTokenAsync(user, "Abiturient.tsu", "RefreshToken");
            await _userManager.SetAuthenticationTokenAsync(user, "Abiturient.tsu", "RefreshToken", refreshToken);

            string accessToken = _accessTokenGenerator.GenerateAccessToken(user, (List<string>)await _userManager.GetRolesAsync(user));
            await _userManager.SetAuthenticationTokenAsync(user, "Abiturient.tsu", "AccessToken", accessToken);

            var rabbit = RabbitHutch.CreateBus("host=localhost");
            await rabbit.PubSub.PublishAsync(new TokenMessage(user.Id.ToString(), accessToken));

            Console.WriteLine("Send Mesage");

            return new OkObjectResult(new TokenResponse(refreshToken, accessToken));
        }

        public async Task<ActionResult<TokenResponse>> LoginUser(LoginRequest loginRequest)
        {
            User user = await _userManager.FindByEmailAsync(loginRequest.email);

            if (user == null)
            {
                return new NotFoundObjectResult(new ErrorResponse(404, "Пользователь с такой почтой не найден"));
            }
            else if (await _userManager.CheckPasswordAsync(user, loginRequest.password))
            {
                await _userManager.RemoveAuthenticationTokenAsync(user, "Abiturient.tsu", "RefreshToken");
                await _userManager.RemoveAuthenticationTokenAsync(user, "Abiturient.tsu", "AccessToken");

                var refreshToken = await _userManager.GenerateUserTokenAsync(user, "Abiturient.tsu", "RefreshToken");
                await _userManager.SetAuthenticationTokenAsync(user, "Abiturient.tsu", "RefreshToken", refreshToken);

                string accessToken = _accessTokenGenerator.GenerateAccessToken(user, (List<string>)await _userManager.GetRolesAsync(user));
                await _userManager.SetAuthenticationTokenAsync(user, "Abiturient.tsu", "AccessToken", accessToken);

                var rabbit = RabbitHutch.CreateBus("host=localhost");
                await rabbit.PubSub.PublishAsync(new TokenMessage(user.Id.ToString(), accessToken));

                return new OkObjectResult(new TokenResponse(refreshToken, accessToken));
            }
            else
            {
                return new UnauthorizedObjectResult(new ErrorResponse(401, "Неправильный пароль"));
            }
        }

        public async Task<IActionResult> LogoutUser(string userId, string accessToken)
        {
            User user = await _userManager.FindByIdAsync(userId);


            if (accessToken == await _userManager.GetAuthenticationTokenAsync(user, "Abiturient.tsu", "AccessToken"))
            {
                await _userManager.RemoveAuthenticationTokenAsync(user, "Abiturient.tsu", "RefreshToken");
                await _userManager.RemoveAuthenticationTokenAsync(user, "Abiturient.tsu", "AccessToken");

                var rabbit = RabbitHutch.CreateBus("host=localhost");
                await rabbit.PubSub.PublishAsync(user.Id.ToString());

                return new OkObjectResult("");
            }
            else return new UnauthorizedObjectResult(new ErrorResponse(401, "Неподходящий access token"));
        }

        public async Task<ActionResult<TokenResponse>> Refresh(string refreshToken)
        {
            User user = await _userManager.FindByIdAsync((await _context.UserTokens.Where(x => x.Name == "RefreshToken" && x.Value == refreshToken).Select(x => x.UserId).FirstOrDefaultAsync()).ToString());

            if (user!=null)
            {
                await _userManager.RemoveAuthenticationTokenAsync(user, "Abiturient.tsu", "RefreshToken");
                await _userManager.RemoveAuthenticationTokenAsync(user, "Abiturient.tsu", "AccessToken");

                var newRefreshToken = await _userManager.GenerateUserTokenAsync(user, "Abiturient.tsu", "RefreshToken");
                await _userManager.SetAuthenticationTokenAsync(user, "Abiturient.tsu", "RefreshToken", newRefreshToken);

                string accessToken = _accessTokenGenerator.GenerateAccessToken(user, (List<string>)await _userManager.GetRolesAsync(user));
                await _userManager.SetAuthenticationTokenAsync(user, "Abiturient.tsu", "AccessToken", accessToken);

                var rabbit = RabbitHutch.CreateBus("host=localhost");
                await rabbit.PubSub.PublishAsync(new TokenMessage(user.Id.ToString(), accessToken));

                return new OkObjectResult(new TokenResponse(newRefreshToken, accessToken));
            }
            else return new UnauthorizedObjectResult(new ErrorResponse(401, "Неподходящий refresh token"));
        }
        public async Task<ActionResult<UserProfileResponse>> GetUserProfile(string userId, string accessToken)
        {
            User user = await _userManager.FindByIdAsync(userId);
            if (accessToken == await _userManager.GetAuthenticationTokenAsync(user, "Abiturient.tsu", "AccessToken"))
            {
                return new OkObjectResult(new UserProfileResponse(user, (List<string>)await _userManager.GetRolesAsync(user)));
            }
            else return new UnauthorizedObjectResult("");
        }


        public async Task<IActionResult> EditUser(string userId, EditUserRequest editUserRequest, string accessToken)
        {
            User user = await _userManager.FindByIdAsync(userId);

            if (accessToken == await _userManager.GetAuthenticationTokenAsync(user, "Abiturient.tsu", "AccessToken"))
            {

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

                return new OkObjectResult("");
            }
            else return new UnauthorizedObjectResult("");
        }

        public async Task<IActionResult> SetRolesToUser(string userId, List<string> roles, string accessToken, string adminUserId)
        {
            User user = await _userManager.FindByIdAsync(userId);

            if (accessToken == await _userManager.GetAuthenticationTokenAsync(await _userManager.FindByIdAsync(adminUserId), "Abiturient.tsu", "AccessToken"))
            {
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
            else return new UnauthorizedObjectResult("");
        }

        public async Task<IActionResult> SetNewPassword(string userId, string accessToken, ChangePasswordRequest changePasswordRequest)
        {
            User user = await _userManager.FindByIdAsync(userId);

            if (accessToken == await _userManager.GetAuthenticationTokenAsync(user, "Abiturient.tsu", "AccessToken"))
            {
                if (await _userManager.CheckPasswordAsync(user, changePasswordRequest.password))
                {
                    await _userManager.ChangePasswordAsync(user, changePasswordRequest.password, changePasswordRequest.newPassword);
                    return new OkObjectResult("");
                }
                else return new BadRequestObjectResult(new ErrorResponse(400, "Неправильный пароль"));
            }
            else return new UnauthorizedObjectResult("");
        }
    }
}
