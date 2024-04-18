using System.Text.RegularExpressions;
using Users_Service.Models;

namespace Users_Service.Services
{
    public class UserValidator
    {
        public UserValidator(){}

        public async Task<bool> Validate(RegisterRequest registerRequest)
        {
            await IsValidEmail(registerRequest.email);

            await IsValidFullName(registerRequest.fullName);

            await IsValidPhoneNumber(registerRequest.phone);

            await IsValidPassword(registerRequest.password);

            return true;
        }

        public async Task<bool> Validate(EditUserRequest editUserRequest)
        {
            if (editUserRequest.phone!=null) await IsValidPhoneNumber(editUserRequest.phone);

            if (editUserRequest.fullName != null) await IsValidFullName(editUserRequest.fullName);

            if (editUserRequest.email != null) await IsValidEmail(editUserRequest.email);

            if (editUserRequest.birthDate != null) await IsValidBirthDate(editUserRequest.birthDate);

            if (editUserRequest.gender != null) await IsValidGender(editUserRequest.gender);

            return true;
        }

        public async Task<bool> Validate(ChangePasswordRequest changePasswordRequest)
        {
            await IsValidPassword(changePasswordRequest.newPassword);

            return true;
        }

        private async Task<bool> IsValidPhoneNumber(string phone)
        {

            if (!Regex.IsMatch(phone, @"^\+7 \d{3} \d{3}-\d{2}-\d{2}$"))
            {
                throw new ArgumentException("Неправильный формат номера телефона, должен быть: +7 999 999-99-99");
            }
            return true;

        }
        private async Task<bool> IsValidPassword(string password)
        {
            if ( password.Length < 8)
            {
                throw new ArgumentException("Пароль не может быть меньше 8 символов");
            }
            return true;

        }
        private async Task<bool> IsValidFullName(string fullName)
        {
            if (!Regex.IsMatch(fullName, "^[А-Яа-я]+\\s[А-Яа-я]+\\s[А-Яа-я]+$"))
            {
                throw new ArgumentException("Неправильный формат полного имени, должно быть: Фамилия Имя Отчество");
            }    
            return true;
        }

        private async Task<bool> IsValidGender(string gender)
        {
            if (!Regex.IsMatch(gender, "^(Мужской|Женский)$"))
            {
                throw new ArgumentException("Неправильный формат гендера, должно быть: Мужской или Женский");
            }
            return true;
        }

        private async Task<bool> IsValidBirthDate(string birthDate)
        {
            if (!Regex.IsMatch(birthDate, "^[0-9]{2}\\.[0-9]{2}\\.[0-9]{4}$"))
            {
                throw new ArgumentException("Неправильный формат даты рождения, должно быть: дд.мм.гггг");
            }
            return true;
        }

        private async Task<bool> IsValidEmail(string email)
        {
            if (!Regex.IsMatch(email, @"^[a-zA-Z0-9_.+-]+@[a-zA-Z0-9-]+\.[a-zA-Z0-9-.]+$"))
            {
                throw new ArgumentException("Неправильный формат электронной почты");
            }
            return true;
        }
    }
}
