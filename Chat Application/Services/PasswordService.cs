using Chat_Application.DTOs;
using System.Security.Cryptography;

namespace Chat_Application.Services
{
    public interface IPasswordService
    {
        void CreatePasswordHash(string password, out byte[] passwordHash, out byte[] passwordSalt);
        ServiceResponse<bool> VerifyPasswordHash(string password, byte[] passwordHash, byte[] passwordSalt);
    }
    public class PasswordService : IPasswordService
    {
        public void CreatePasswordHash(string password, out byte[] passwordHash, out byte[] passwordSalt)
        {
            using (var hmac = new HMACSHA512())
            {
                passwordSalt = hmac.Key;
                passwordHash = hmac.ComputeHash(System.Text.Encoding.UTF8.GetBytes(password));
            }
        }

        public ServiceResponse<bool> VerifyPasswordHash(string password, byte[] passwordHash, byte[] passwordSalt)
        {
            ServiceResponse<bool> response = new ServiceResponse<bool>();
            using (var hmac = new HMACSHA512(passwordSalt))
            {
                var computedHash = hmac.ComputeHash(System.Text.Encoding.UTF8.GetBytes(password));
                if(computedHash.SequenceEqual(passwordHash))
                {
                    response.Data = true;
                }else
                {
                    Console.WriteLine("wrong");
                    response.Error = "Wrong Password";
                }
                return response;
            }
        }
    }
}
