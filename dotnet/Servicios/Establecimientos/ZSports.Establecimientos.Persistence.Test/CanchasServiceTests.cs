using Microsoft.Extensions.Logging;
using MockQueryable;
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

    [Test]
    public async Task ObtenerPorEstablecimientoAsync_ConRequestValida_RetornaCanchasCorrectamente()
    {
        // Arrange
        var establecimientoId = Guid.NewGuid();
        var paginationInfo = new GetItemsPaginated { PageNumber = 1, PageSize = 10 };
        var cancha1 = new Cancha();
        cancha1.SetNumero(1);
        cancha1.SetTipoSuelo(TipoSuelo.Cesped);
        cancha1.SetEstablecimiento(establecimientoId);

        var cancha2 = new Cancha();
        cancha2.SetNumero(2);
        cancha2.SetTipoSuelo(TipoSuelo.PolvoLadrillo);
        cancha2.SetEstablecimiento(establecimientoId);

        var canchas = new List<Cancha>
        {
            cancha1, cancha2
        };
        
        _repoMock.Setup(repo => repo.GetAsQueryable())
                 .Returns(canchas.AsQueryable().BuildMock());
        
        // Act
        var result = await _canchasService.ObtenerPorEstablecimientoAsync(establecimientoId, paginationInfo, CancellationToken.None);
        // Assert
        Assert.Multiple(() =>
        {
            Assert.That(result, Is.Not.Null);
            Assert.That(result.Count(), Is.EqualTo(2));
            Assert.That(result.First().EstablecimientoId, Is.EqualTo(establecimientoId));
        });
        
        _repoMock.Verify(repo => repo.GetAsQueryable(), Times.Once);
    }

    [Test]
    public async Task ObtenerPorEstablecimientoAsync_SinCanchas_RetornaListaVacia()
    {
        // Arrange
        var establecimientoId = Guid.NewGuid();
        var paginationInfo = new GetItemsPaginated { PageNumber = 1, PageSize = 10 };
        
        _repoMock.Setup(repo => repo.GetAsQueryable())
                 .Returns(Enumerable.Empty<Cancha>().AsQueryable().BuildMock());
        
        // Act
        var result = await _canchasService.ObtenerPorEstablecimientoAsync(establecimientoId, paginationInfo, CancellationToken.None);
        
        // Assert
        Assert.Multiple(() =>
        {
            Assert.That(result, Is.Not.Null);
            Assert.That(result.Count(), Is.EqualTo(0));
        });
        
        _repoMock.Verify(repo => repo.GetAsQueryable(), Times.Once);
    }

    [Test]
    public void ObtenerPorEstablecimientoAsync_ConEstablecimientoIdVacio_LanzaException()
    {
        // Arrange
        var paginationInfo = new GetItemsPaginated { PageNumber = 1, PageSize = 10 };
        
        // Act & Assert
        Assert.ThrowsAsync<ArgumentException>(async () => 
            await _canchasService.ObtenerPorEstablecimientoAsync(Guid.Empty, paginationInfo, CancellationToken.None));
        
        _repoMock.Verify(repo => repo.GetAsQueryable(), Times.Never);
    }

    [Test]
    public async Task ObtenerPorId_ConIdValido_ObtieneCanchaCorrespondiente()
    {
        // Arrange.
        var numero = 1;
        var tipoSuelo = TipoSuelo.Parquet;
        var establecimientoId = Guid.NewGuid();

        var cancha = new Cancha();
        cancha.SetNumero(numero);
        cancha.SetTipoSuelo(tipoSuelo);
        cancha.SetEstablecimiento(establecimientoId);

        _repoMock.Setup(r => r.GetByIdAsync(It.IsAny<Guid>(), It.IsAny<CancellationToken>())).ReturnsAsync(cancha);

        // Act.
        var result = await _canchasService.ObtenerPorIdAsync(cancha.Id, CancellationToken.None);

        // Assert.
        Assert.Multiple(() =>
        {
            Assert.That(result, Is.Not.Null);
            Assert.That(result.Id, Is.EqualTo(cancha.Id));
            Assert.That(result.Numero, Is.EqualTo(numero));
            Assert.That(result.TipoSuelo, Is.EqualTo(tipoSuelo));
            Assert.That(result.EstablecimientoId, Is.EqualTo(establecimientoId));
        });
    }

    [Test]
    public void ObtenerPorId_ConIdInvalido_LanzaExcepcion()
    {
        // Arrange.
        var invalidId = Guid.Empty;
        // Act & Assert.
        Assert.ThrowsAsync<KeyNotFoundException>(async () => 
            await _canchasService.ObtenerPorIdAsync(invalidId, CancellationToken.None));
        
        _repoMock.Verify(r => r.GetByIdAsync(It.IsAny<Guid>(), It.IsAny<CancellationToken>()), Times.Once);
    }
}
