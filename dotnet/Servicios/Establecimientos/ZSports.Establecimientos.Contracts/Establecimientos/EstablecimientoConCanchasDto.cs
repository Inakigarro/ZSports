using ZSports.Establecimientos.Contracts.Canchas;

namespace ZSports.Establecimientos.Contracts.Establecimientos;

public record EstablecimientoConCanchasDto: EstablecimientoDto
{
    public IEnumerable<CanchaDto> Canchas { get; set; } = [];
}
