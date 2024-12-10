namespace hospitalfinal.Models
{
    public interface IAuthenticationService
    {
        string GenerateJwtToken(string userId, string userName);
    }

}
