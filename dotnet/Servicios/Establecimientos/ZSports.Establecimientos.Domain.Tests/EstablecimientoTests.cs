using ZSports.Establecimientos.Domain;
using ZSports.Establecimientos.Domain.Constants;

namespace ZSports.Establecimientos.Domain.Tests;

public class Tests
{
    private Establecimiento _establecimiento;

    [SetUp]
    public void Setup()
    {
        _establecimiento = new Establecimiento();
    }

    [Test]
    public void NuevoEstablecimiento_SettearNombreValido_DebeTerminarBienYSettearNombre()
    {
        // Arrange.
        var nombre = "Nombre Valido";

        // Act.
        _establecimiento.SetNombre(nombre);

        // Assert.
        Assert.That(_establecimiento.Nombre, Is.EqualTo(nombre));
    }

    [Test]
    public void NuevoEstablecimiento_SettearNombreInvalido_DebeLanzarException()
    {
        // Arrange.
        var primerNombreInvalido = string.Empty;
        var segundoNombreInvalido = new string('a', EstablecimientoConstants.MaxNombreLength + 1);

        // Act & Assert.
        Assert.Throws<ArgumentException>(() => _establecimiento.SetNombre(primerNombreInvalido), "El nombre no puede estar vacío.");
        Assert.Throws<ArgumentException>(() => _establecimiento.SetNombre(segundoNombreInvalido), $"El nombre no puede exceder los {EstablecimientoConstants.MaxNombreLength} caracteres.");
    }

    [Test]
    public void NuevoEstablecimiento_SettearDireccionValida_DebeTerminarBienYSettearDireccion()
    {
        // Arrange.
        var direccion = "123 Calle Principal, Ciudad, País";

        // Act.
        _establecimiento.SetDireccion(direccion);

        // Assert.
        Assert.That(_establecimiento.Direccion, Is.EqualTo(direccion));
    }

    [Test]
    public void NuevoEstablecimiento_SettearDireccionInvalida_DebeLanzarException()
    {
        // Arrange.
        var primerDireccionInvalida = string.Empty;
        var segundaDireccionInvalida = new string('a', EstablecimientoConstants.MaxDireccionLength + 1);
        // Act & Assert.
        Assert.Throws<ArgumentException>(() => _establecimiento.SetDireccion(primerDireccionInvalida), "La dirección no puede estar vacía.");
        Assert.Throws<ArgumentException>(() => _establecimiento.SetDireccion(segundaDireccionInvalida), $"La dirección no puede exceder los {EstablecimientoConstants.MaxDireccionLength} caracteres.");
    }

    [Test]
    public void NuevoEstablecimiento_SettearTelefonoValido_DebeTerminarBienYSettearTelefono()
    {
        // Arrange.
        var telefono = "+123 456 7890";
        // Act.
        _establecimiento.SetTelefono(telefono);
        // Assert.
        Assert.That(_establecimiento.Telefono, Is.EqualTo(telefono));
    }

    [Test]
    public void NuevoEstablecimiento_SettearTelefonoInvalido_DebeLanzarException()
    {
        // Arrange.
        var primerTelefonoInvalido = string.Empty;
        var segundoTelefonoInvalido = new string('1', EstablecimientoConstants.MaxTelefonoLength + 1);
        var tercerTelefonoInvalido = "123-456?7890";

        // Act & Assert.
        Assert.Throws<ArgumentException>(() => _establecimiento.SetTelefono(primerTelefonoInvalido), "El teléfono no puede estar vacío.");
        Assert.Throws<ArgumentException>(() => _establecimiento.SetTelefono(segundoTelefonoInvalido), $"El teléfono no puede exceder los {EstablecimientoConstants.MaxTelefonoLength} caracteres.");
        Assert.Throws<ArgumentException>(() => _establecimiento.SetTelefono(tercerTelefonoInvalido), "El formato del teléfono es inválido.");
    }

    [Test]
    public void NuevoEstablecimiento_SettearEmailValido_DebeTerminarBienYSettearEmail()
    {
        // Arrange.
        var email = "email@email.com";

        // Act.
        _establecimiento.SetEmail(email);

        // Assert.
        Assert.That(_establecimiento.Email, Is.EqualTo(email));
    }

    [Test]
    public void NuevoEstablecimiento_SettearEmailInvalido_DebeLanzarException()
    {
        // Arrange.
        var primerEmailInvalido = string.Empty;
        var segundoEmailInvalido = new string('a', EstablecimientoConstants.MaxEmailLength + 1);
        var tercerEmailInvalido = "email@com"; // Falta el dominio
        var cuartoEmailInvalido = "@email.com"; // Falta el usuario
        var quintoEmailInvalido = "email@email"; // Falta el TLD
        var sextoEmailInvalido = ".com"; // Solo el TLD

        // Act & Assert.
        Assert.Throws<ArgumentException>(() => _establecimiento.SetEmail(primerEmailInvalido), "El email no puede estar vacío.");
        Assert.Throws<ArgumentException>(() => _establecimiento.SetEmail(segundoEmailInvalido), $"El email no puede exceder los {EstablecimientoConstants.MaxEmailLength} caracteres.");
        Assert.Throws<ArgumentException>(() => _establecimiento.SetEmail(tercerEmailInvalido), "El formato del email es inválido.");
        Assert.Throws<ArgumentException>(() => _establecimiento.SetEmail(cuartoEmailInvalido), "El formato del email es inválido.");
        Assert.Throws<ArgumentException>(() => _establecimiento.SetEmail(quintoEmailInvalido), "El formato del email es inválido.");
        Assert.Throws<ArgumentException>(() => _establecimiento.SetEmail(sextoEmailInvalido), "El formato del email es inválido.");
    }
}
