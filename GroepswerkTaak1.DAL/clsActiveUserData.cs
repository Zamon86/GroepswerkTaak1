using GroepswerkTaak1.Model;

namespace GroepswerkTaak1.DAL;

public class clsActiveUserData
{
	public static clsUserM ActiveUser { get; set; } = new();
	public static clsRollenM ActiveUserRole { get; set; } = new()
	{
		RolNaam = string.Empty
	};


}