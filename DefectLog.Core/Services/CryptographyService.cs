namespace DefectLog.Core.Services
{
    public interface ICryptographyService
    {
        string GetSalt();
        string HashPassword(string password, string salt);
    }

    public class CryptographyService : ICryptographyService
    {
        public string GetSalt()
        {
            return BCrypt.Net.BCrypt.GenerateSalt();
        }

        public string HashPassword(string password, string salt)
        {
            return BCrypt.Net.BCrypt.HashPassword(password, salt);
        }
    }
}