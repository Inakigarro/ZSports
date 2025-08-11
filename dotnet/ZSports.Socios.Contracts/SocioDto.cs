namespace ZSports.Socios.Contracts;

public class SocioDto
{
    /// <summary>
    /// Identificador único del socio.
    /// </summary>
    public Guid Id { get; set; }

    /// <summary>
    /// Nombre del socio.
    /// </summary>
    public string Nombre { get; set; } = string.Empty;

    /// <summary>
    /// Apellido del socio.
    /// </summary>
    public string Apellido { get; set; } = string.Empty;

    /// <summary>
    /// Correo electrónico del socio.
    /// </summary>
    public string Email { get; set; } = string.Empty;

    /// <summary>
    /// Número de teléfono del socio.
    /// </summary>
    public string Telefono { get; set; } = string.Empty;
}
