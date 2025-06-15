using Microsoft.Extensions.Logging;
using Moq;
using ZSports.Contracts;
using ZSports.Establecimientos.Contracts.Canchas.CrearCancha;
using ZSports.Establecimientos.Domain;
using ZSports.Establecimientos.Domain.Enums;

namespace ZSports.Establecimientos.Persistence.Test;

public class CanchasServiceTests
{
    private Mock<ILogger<CanchasService>> _loggerMock;
    private Mock<IUnitOfWork> _unitOfWorkMock;
    private Mock<IGenericRepository<Cancha, Guid>> _repoMock;
    private CanchasService _canchasService;
    public CanchasServiceTests()
    {
        _loggerMock = new Mock<ILogger<CanchasService>>();
        _unitOfWorkMock = new Mock<IUnitOfWork>();
        _repoMock = new Mock<IGenericRepository<Cancha, Guid>>();
        _canchasService = new CanchasService(_loggerMock.Object, _unitOfWorkMock.Object);
    }

    [SetUp]
    public void Setup()
    {
        _loggerMock = new Mock<ILogger<CanchasService>>();
        _repoMock = new Mock<IGenericRepository<Cancha, Guid>>();
        _unitOfWorkMock = new Mock<IUnitOfWork>();
        _unitOfWorkMock.Setup(uow => uow.GetRepository<Cancha, Guid>()).Returns(_repoMock.Object);
        _canchasService = new CanchasService(_loggerMock.Object, _unitOfWorkMock.Object);
    }

    [Test]
    public async Task AgregarAsync_CanchaValida_CreaCanchaYRetornaDto()
    {
        // Arrange
        var request = new CrearCanchaRequest
        {
            Numero = 1,
            TipoSuelo = TipoSuelo.Cesped,
            EstablecimientoId = Guid.NewGuid()
        };
        
        _repoMock.Setup(repo => repo.AddAsync(It.IsAny<Cancha>(), It.IsAny<CancellationToken>()))
                 .Returns(Task.CompletedTask);
        
        // Act
        var result = await _canchasService.AgregarAsync(request, CancellationToken.None);

        // Assert
        Assert.Multiple(() =>
        {
            Assert.That(result, Is.Not.Null);
            Assert.That(result.Id, Is.Not.EqualTo(Guid.Empty));
            Assert.That(result.Numero, Is.EqualTo(request.Numero));
            Assert.That(result.TipoSuelo, Is.EqualTo(request.TipoSuelo));
            Assert.That(result.EstablecimientoId, Is.EqualTo(request.EstablecimientoId));
        });
        
        _repoMock.Verify(repo => repo.AddAsync(It.IsAny<Cancha>(), It.IsAny<CancellationToken>()), Times.Once);
        _unitOfWorkMock.Verify(uow => uow.SaveChangesAsync(It.IsAny<CancellationToken>()), Times.Once);
    }

    [Test]
    public void AgregarAsync_CanchaInvalida_LanzaExcepcion()
    {
        // Arrange
        var request = new CrearCanchaRequest
        {
            Numero = 0, // Número inválido
            TipoSuelo = TipoSuelo.Cesped,
            EstablecimientoId = Guid.NewGuid()
        };
        // Act & Assert
        Assert.ThrowsAsync<ArgumentException>(async () => await _canchasService.AgregarAsync(request, CancellationToken.None));
        
        _repoMock.Verify(repo => repo.AddAsync(It.IsAny<Cancha>(), It.IsAny<CancellationToken>()), Times.Never);
        _unitOfWorkMock.Verify(uow => uow.SaveChangesAsync(It.IsAny<CancellationToken>()), Times.Never);
    }

    [Test]
    public void AgregarAsync_EstablecimientoIdVacio_LanzaExcepcion()
    {
        // Arrange
        var request = new CrearCanchaRequest
        {
            Numero = 1,
            TipoSuelo = TipoSuelo.Cesped,
            EstablecimientoId = Guid.Empty // ID de establecimiento vacío
        };
        
        // Act & Assert
        Assert.ThrowsAsync<ArgumentException>(async () => await _canchasService.AgregarAsync(request, CancellationToken.None));
        
        _repoMock.Verify(repo => repo.AddAsync(It.IsAny<Cancha>(), It.IsAny<CancellationToken>()), Times.Never);
        _unitOfWorkMock.Verify(uow => uow.SaveChangesAsync(It.IsAny<CancellationToken>()), Times.Never);
    }

    [Test]
    public void AgregarAsync_TipoSueloSinDefinir_LanzaExcepcion()
    {
        // Arrange
        var request = new CrearCanchaRequest
        {
            Numero = 1,
            TipoSuelo = TipoSuelo.SinDefinir, // Tipo de suelo inválido
            EstablecimientoId = Guid.NewGuid()
        };
        
        // Act & Assert
        Assert.ThrowsAsync<ArgumentException>(async () => await _canchasService.AgregarAsync(request, CancellationToken.None));
        
        _repoMock.Verify(repo => repo.AddAsync(It.IsAny<Cancha>(), It.IsAny<CancellationToken>()), Times.Never);
        _unitOfWorkMock.Verify(uow => uow.SaveChangesAsync(It.IsAny<CancellationToken>()), Times.Never);
    }
}
