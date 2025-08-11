using ZSports.Domain;

namespace ZSports.Socios.Domain;

public class Socio
{
    public Guid Id { get; private set; } = Guid.NewGuid();
    public string Nombre { get; private set; } = string.Empty;
    public string Apellido { get; private set; } = string.Empty;
    public string Email { get; private set; } = string.Empty;
    public string Telefono { get; private set; } = string.Empty;

    public Guid UserId { get; private set; } = Guid.Empty;
    public virtual User User { get; private set; } = null!;

    public void SetNombre(string nombre)
    {
        if (string.IsNullOrWhiteSpace(nombre))
            throw new ArgumentException("El nombre no puede estar vacío.", nameof(nombre));
        
        this.Nombre = nombre;
    }

    public void SetApellido(string apellido)
    {
        if (string.IsNullOrWhiteSpace(apellido))
            throw new ArgumentException("El apellido no puede estar vacío.", nameof(apellido));
        
        this.Apellido = apellido;
    }

    public void SetEmail(string email)
    {
        if (string.IsNullOrWhiteSpace(email) || !email.Contains("@"))
            throw new ArgumentException("El email no es válido.", nameof(email));
        
        this.Email = email;
    }

    public void SetTelefono(string telefono)
    {
        if (string.IsNullOrWhiteSpace(telefono))
            throw new ArgumentException("El teléfono no puede estar vacío.", nameof(telefono));
        
        this.Telefono = telefono;
    }

    public void SetUser(Guid userId)
    {
        if (userId == Guid.Empty)
            throw new ArgumentException("El ID del usuario no puede ser un GUID vacío.", nameof(userId));
        
        this.UserId = userId;
    }
}
