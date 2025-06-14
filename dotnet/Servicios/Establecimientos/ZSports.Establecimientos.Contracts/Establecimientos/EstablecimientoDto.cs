namespace ZSports.Establecimientos.Contracts.Establecimientos;

public record EstablecimientoDto
{
    ///<summary>
    /// Id del establecimiento.
    /// </summary>
    public Guid Id { get; set; } = Guid.Empty;

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
