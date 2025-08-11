namespace ZSports.Socios.Domain.Tests;

public class Tests
{
    private Socio _socio;

    [SetUp]
    public void Setup()
    {
        _socio = new Socio();
    }

    [Test]
    public void ValoresPorDefecto()
    {
        Assert.Multiple(() =>
        {
            Assert.That(_socio.Id, Is.Not.EqualTo(Guid.Empty), "Id should not be empty");
            Assert.That(_socio.Nombre, Is.EqualTo(string.Empty), "Nombre should be empty by default");
            Assert.That(_socio.Apellido, Is.EqualTo(string.Empty), "Apellido should be empty by default");
            Assert.That(_socio.Email, Is.EqualTo(string.Empty), "Email should be empty by default");
            Assert.That(_socio.Telefono, Is.EqualTo(string.Empty), "Telefono should be empty by default");
            Assert.That(_socio.UserId, Is.EqualTo(Guid.Empty), "UserId should be empty by default");
            Assert.That(_socio.User, Is.Null, "User should be null by default");
        });
    }

    [Test]
    public void SetNombre_ValorValido_AsignaNombre()
    {
        _socio.SetNombre("Juan");
        Assert.That(_socio.Nombre, Is.EqualTo("Juan"), "Nombre should be set correctly");
    }

    [Test]
    public void SetNombre_ValorInvalido_LanzaExcepcion()
    {
        Assert.Throws<ArgumentException>(() => _socio.SetNombre(string.Empty), "Should throw exception for empty name");
        Assert.Throws<ArgumentException>(() => _socio.SetNombre(null!), "Should throw exception for null name");
    }

    [Test]
    public void SetApellido_ValorValido_AsignaApellido()
    {
        _socio.SetApellido("Perez");
        Assert.That(_socio.Apellido, Is.EqualTo("Perez"), "Apellido should be set correctly");
    }

    [Test]
    public void SetApellido_ValorInvalido_LanzaExcepcion()
    {
        Assert.Throws<ArgumentException>(() => _socio.SetApellido(string.Empty), "Should throw exception for empty last name");
        Assert.Throws<ArgumentException>(() => _socio.SetApellido(null!), "Should throw exception for null last name");
    }

    [Test]
    public void SetEmail_ValorValido_AsignaEmail()
    {
        _socio.SetEmail("email@email.com");
        Assert.That(_socio.Email, Is.EqualTo("email@email.com"), "Email should be set correctly");
    }

    [Test]
    public void SetEmail_ValorInvalido_LanzaExcepcion()
    {
        Assert.Throws<ArgumentException>(() => _socio.SetEmail(string.Empty), "Should throw exception for empty email");
        Assert.Throws<ArgumentException>(() => _socio.SetEmail(null!), "Should throw exception for null email");
        Assert.Throws<ArgumentException>(() => _socio.SetEmail("invalid-email"), "Should throw exception for invalid email format");
    }

    [Test]
    public void SetTelefono_ValorValido_AsignaTelefono()
    {
        _socio.SetTelefono("1234567890");
        Assert.That(_socio.Telefono, Is.EqualTo("1234567890"), "Telefono should be set correctly");
    }

    [Test]
    public void SetTelefono_ValorInvalido_LanzaExcepcion()
    {
        Assert.Throws<ArgumentException>(() => _socio.SetTelefono(string.Empty), "Should throw exception for empty phone number");
        Assert.Throws<ArgumentException>(() => _socio.SetTelefono(null!), "Should throw exception for null phone number");
    }
}
