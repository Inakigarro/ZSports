namespace ZSports.Establecimientos.Contracts.Establecimientos.CrearEstablecimiento;

/// <summary>
/// Dto utilizado para crear un nuevo establecimiento.
/// </summary>
public record CrearEstablecimientoRequest
{
    /// <summary>
    /// Nombre del establecimiento.
    /// </summary>
    public string Nombre { get; set; } = string.Empty;

    /// <summary>
    /// Dirección del establecimiento.
    /// </summary>
    public string Direccion { get; set; } = string.Empty;

    /// <summary>
    /// Telefono de contacto del establecimiento.
    /// </summary>
    public string Telefono { get; set; } = string.Empty;

    /// <summary>
    /// Email de contacto del establecimiento.
    /// </summary>
    public string Email { get; set; } = string.Empty;
}
