namespace GroepsWerk.Mail
{
    public interface UserLogin
    {
        string LoginNaam { get; set; }
        string Wachtwoord { get; set; }

        string Email { get; set; }
    }
}