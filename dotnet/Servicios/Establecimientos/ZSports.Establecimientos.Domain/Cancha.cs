using ZSports.Establecimientos.Domain.Enums;

namespace ZSports.Establecimientos.Domain;

public class Cancha
{
    public Guid Id { get; private set; } = Guid.NewGuid();
    public int Numero { get; private set; } = 0;
    public TipoSuelo TipoSuelo { get; private set; } = TipoSuelo.SinDefinir;
    public Guid EstablecimientoId { get; private set; } = Guid.Empty;
    public virtual Establecimiento Establecimiento { get; private set; } = null!;

    public void SetNumero(int numero)
    {
        if (numero <= 0)
            throw new ArgumentException("El número de la cancha debe ser mayor que cero.", nameof(numero));
        
        this.Numero = numero;
    }

    public void SetTipoSuelo(TipoSuelo tipoSuelo)
    {
        if (!Enum.IsDefined(tipoSuelo))
            throw new ArgumentException("El tipo de suelo especificado no es válido.", nameof(tipoSuelo));
        
        this.TipoSuelo = tipoSuelo;
    }

    public void SetEstablecimiento(Guid establecimientoId)
    {
        if (establecimientoId == Guid.Empty)
            throw new ArgumentException("El ID del establecimiento no puede ser un GUID vacío.", nameof(establecimientoId));
        
        this.EstablecimientoId = establecimientoId;
    }
}
