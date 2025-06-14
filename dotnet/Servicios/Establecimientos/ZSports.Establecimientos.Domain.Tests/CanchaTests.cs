using ZSports.Establecimientos.Domain;
using ZSports.Establecimientos.Domain.Enums;

namespace ZSports.Establecimientos.Domain.Tests;

public class CanchaTests
{
    private Cancha _cancha;

    [SetUp]
    public void Setup()
    {
        _cancha = new Cancha();
    }

    [Test]
    public void Cancha_Instancia_ValoresPorDefecto()
    {
        Assert.That(_cancha.Id, Is.Not.EqualTo(Guid.Empty));
        Assert.That(_cancha.Numero, Is.EqualTo(0));
        Assert.That(_cancha.TipoSuelo, Is.EqualTo(TipoSuelo.SinDefinir));
        Assert.That(_cancha.EstablecimientoId, Is.EqualTo(Guid.Empty));
        Assert.That(_cancha.Establecimiento, Is.Null);
    }

    [Test]
    public void SetNumero_ValorValido_AsignaNumero()
    {
        _cancha.SetNumero(5);
        Assert.That(_cancha.Numero, Is.EqualTo(5));
    }

    [Test]
    public void SetNumero_ValorInvalido_LanzaExcepcion()
    {
        Assert.Throws<ArgumentException>(() => _cancha.SetNumero(0));
        Assert.Throws<ArgumentException>(() => _cancha.SetNumero(-1));
    }

    [Test]
    public void SetTipoSuelo_ValorValido_AsignaTipoSuelo()
    {
        _cancha.SetTipoSuelo(TipoSuelo.Cemento);
        Assert.That(_cancha.TipoSuelo, Is.EqualTo(TipoSuelo.Cemento));
    }

    [Test]
    public void SetTipoSuelo_ValorInvalido_LanzaExcepcion()
    {
        Assert.Throws<ArgumentException>(() => _cancha.SetTipoSuelo((TipoSuelo)999));
        Assert.Throws<ArgumentException>(() => _cancha.SetTipoSuelo((TipoSuelo)(-1)));
    }

    [Test]
    public void SetEstablecimiento_ValorValido_AsignaEstablecimientoId()
    {
        var id = Guid.NewGuid();
        _cancha.SetEstablecimiento(id);
        Assert.That(_cancha.EstablecimientoId, Is.EqualTo(id));
    }

    [Test]
    public void SetEstablecimiento_ValorInvalido_LanzaExcepcion()
    {
        Assert.Throws<ArgumentException>(() => _cancha.SetEstablecimiento(Guid.Empty));
    }
}
