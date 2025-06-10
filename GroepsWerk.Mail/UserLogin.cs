namespace GroepsWerk.Mail
{
    public interface I_UserLogin
    {
        string LoginNaam { get; set; }
        string Wachtwoord { get; set; }

        string Email { get; set; }
    }
}