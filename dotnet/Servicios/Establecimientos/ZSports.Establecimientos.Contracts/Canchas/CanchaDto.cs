using ZSports.Establecimientos.Domain.Enums;

namespace ZSports.Establecimientos.Contracts.Canchas;

public record CanchaDto
{
    /// <summary>
    /// Id de la cancha.
    /// </summary>
    public Guid Id { get; set; } = Guid.Empty;

    /// <summary>
    /// Numero de la cancha dentro del establecimiento.
    /// </summary>
    public int Numero { get; set; } = 0;

    /// <summary>
    /// Tipo de superficie de la cancha.
    /// </summary>
    public TipoSuelo TipoSuelo { get; set; } = TipoSuelo.SinDefinir;

    /// <summary>
    /// Id del establecimiento al que pertenece la cancha.
    /// </summary>
    public Guid EstablecimientoId { get; set; } = Guid.Empty;
}
