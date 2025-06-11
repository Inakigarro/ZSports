namespace ZSports.Establecimientos.Domain.Constants;

public class EstablecimientoConstants
{
    public const int MaxNombreLength = 100;
    public const int MaxDireccionLength = 200;
    public const int MaxTelefonoLength = 15;
    public const int MaxEmailLength = 100;
    public const string TelefonoRegex = @"^\+?[0-9\s-]+$"; // Optional '+' followed by digits, spaces, or hyphens
    public const string EmailRegex = @"^[^@\s]+@[^@\s]+\.[^@\s]+$"; // Basic email validation
}
