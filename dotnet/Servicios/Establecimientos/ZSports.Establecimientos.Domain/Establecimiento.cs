using ZSports.Establecimientos.Domain.Constants;

namespace ZSports.Establecimientos.Domain;

public class Establecimiento
{
    public Guid Id { get; set; }
    public string Nombre { get; private set; } = string.Empty;
    public string Direccion { get; private set; } = string.Empty;
    public string Telefono { get; private set; } = string.Empty;
    public string Email { get; private set; } = string.Empty;

    public void SetNombre(string nombre)
    {
        if (string.IsNullOrEmpty(nombre))
            throw new ArgumentException("El nombre no puede estar vacío.", nameof(nombre));

        if (nombre.Length > EstablecimientoConstants.MaxNombreLength)
            throw new ArgumentException($"El nombre no puede exceder los {EstablecimientoConstants.MaxNombreLength} caracteres.", nameof(nombre));

        this.Nombre = nombre;
    }

    public void SetDireccion(string direccion)
    {
        if (string.IsNullOrEmpty(direccion))
            throw new ArgumentException("La dirección no puede estar vacía.", nameof(direccion));
        
        if (direccion.Length > EstablecimientoConstants.MaxDireccionLength)
            throw new ArgumentException($"La dirección no puede exceder los {EstablecimientoConstants.MaxDireccionLength} caracteres.", nameof(direccion));
        
        this.Direccion = direccion;
    }

    public void SetTelefono(string telefono)
    {
        if (string.IsNullOrEmpty(telefono))
            throw new ArgumentException("El teléfono no puede estar vacío.", nameof(telefono));
        
        if (telefono.Length > EstablecimientoConstants.MaxTelefonoLength)
            throw new ArgumentException($"El teléfono no puede exceder los {EstablecimientoConstants.MaxTelefonoLength} caracteres.", nameof(telefono));
        
        if (!System.Text.RegularExpressions.Regex.IsMatch(telefono, EstablecimientoConstants.TelefonoRegex))
            throw new ArgumentException("El formato del teléfono es inválido.", nameof(telefono));
        
        this.Telefono = telefono;
    }

    public void SetEmail(string email)
    {
        if (string.IsNullOrEmpty(email))
            throw new ArgumentException("El email no puede estar vacío.", nameof(email));
        
        if (email.Length > EstablecimientoConstants.MaxEmailLength)
            throw new ArgumentException($"El email no puede exceder los {EstablecimientoConstants.MaxEmailLength} caracteres.", nameof(email));
        
        if (!System.Text.RegularExpressions.Regex.IsMatch(email, EstablecimientoConstants.EmailRegex))
            throw new ArgumentException("El formato del email es inválido.", nameof(email));
        
        this.Email = email;
    }
}
