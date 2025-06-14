using Moq;
using NUnit.Framework;
using ZSports.Contracts;
using ZSports.Establecimientos.Contracts.Establecimientos.CrearEstablecimiento;
using ZSports.Establecimientos.Contracts.Establecimientos.ModificarEstablecimiento;
using ZSports.Establecimientos.Domain;

namespace ZSports.Establecimientos.Persistence.Test
{
    public class EstablecimientosServiceTests
    {
        private Mock<IUnitOfWork> _unitOfWorkMock;
        private Mock<IGenericRepository<Establecimiento, Guid>> _repoMock;
        private EstablecimientosService _service;

        [SetUp]
        public void Setup()
        {
            _unitOfWorkMock = new Mock<IUnitOfWork>();
            _repoMock = new Mock<IGenericRepository<Establecimiento, Guid>>();
            _unitOfWorkMock.Setup(u => u.GetRepository<Establecimiento, Guid>()).Returns(_repoMock.Object);
            _service = new EstablecimientosService(_unitOfWorkMock.Object);
        }

        [Test]
        public async Task AgregarAsync_CreaEstablecimientoYDevuelveDto()
        {
            // Arrange
            var request = new CrearEstablecimientoRequest
            {
                Nombre = "Test",
                Direccion = "Calle 123",
                Telefono = "123456",
                Email = "test@email.com"
            };
            _repoMock.Setup(r => r.AddAsync(It.IsAny<Establecimiento>(), It.IsAny<CancellationToken>()))
                .Returns(Task.CompletedTask);

            _unitOfWorkMock.Setup(u => u.SaveChangesAsync(It.IsAny<CancellationToken>()))
                .Returns(Task.CompletedTask);
            // Act
            var result = await _service.AgregarAsync(request, CancellationToken.None);

            // Assert
            Assert.Multiple(() =>
            {
                Assert.That(result, Is.Not.Null);
                Assert.That(result.Id, Is.Not.EqualTo(Guid.Empty));
                Assert.That(result.Nombre, Is.EqualTo(request.Nombre));
                Assert.That(result.Direccion, Is.EqualTo(request.Direccion));
                Assert.That(result.Telefono, Is.EqualTo(request.Telefono));
                Assert.That(result.Email, Is.EqualTo(request.Email));
            });
        }

        [Test]
        public void AgregarAsync_CuandoRepositorioFalla_LanzaExcepcion()
        {
            // Arrange
            var request = new CrearEstablecimientoRequest
            {
                Nombre = "Test",
                Direccion = "Calle 123",
                Telefono = "123456",
                Email = "test@email.com"
            };
            _repoMock.Setup(r => r.AddAsync(It.IsAny<Establecimiento>(), It.IsAny<CancellationToken>()))
                .ThrowsAsync(new InvalidOperationException("Error en el repositorio"));

            // Act & Assert
            Assert.ThrowsAsync<InvalidOperationException>(async () =>
                await _service.AgregarAsync(request, CancellationToken.None));
        }

        [Test]
        public async Task ObtenerPorIdAsync_CuandoExisteEstablecimiento_RetornaDto()
        {
            // Arrange
            var establecimiento = new Establecimiento();
            establecimiento.SetNombre("Test");
            establecimiento.SetDireccion("Calle 123");
            establecimiento.SetTelefono("123456");
            establecimiento.SetEmail("test@email.com");

            _repoMock.Setup(r => r.GetByIdAsync(establecimiento.Id, It.IsAny<CancellationToken>()))
                .ReturnsAsync(establecimiento);

            // Act
            var result = await _service.ObtenerPorIdAsync(establecimiento.Id, CancellationToken.None);

            // Assert
            Assert.Multiple(() =>
            {
                Assert.That(result, Is.Not.Null);
                Assert.That(result.Id, Is.EqualTo(establecimiento.Id));
                Assert.That(result.Nombre, Is.EqualTo("Test"));
                Assert.That(result.Direccion, Is.EqualTo("Calle 123"));
                Assert.That(result.Telefono, Is.EqualTo("123456"));
                Assert.That(result.Email, Is.EqualTo("test@email.com"));
            });
        }

        [Test]
        public void ObtenerPorIdAsync_CuandoRepositorioFalla_LanzaExcepcion()
        {
            // Arrange
            var id = Guid.NewGuid();
            _repoMock.Setup(r => r.GetByIdAsync(id, It.IsAny<CancellationToken>()))
                .ThrowsAsync(new InvalidOperationException("No encontrado"));

            // Act & Assert
            Assert.ThrowsAsync<InvalidOperationException>(async () =>
                await _service.ObtenerPorIdAsync(id, CancellationToken.None));
        }

        [Test]
        public async Task ObtenerPaginadoAsync_CuandoRepositorioRetornaLista_RetornaDtos()
        {
            // Arrange
            var pagination = new GetItemsPaginated { PageNumber = 1, PageSize = 2 };
            
            var establecimiento1 = new Establecimiento();
            establecimiento1.SetNombre("A");
            establecimiento1.SetDireccion("DirA");
            establecimiento1.SetTelefono("111");
            establecimiento1.SetEmail("a@email.com");

            var establecimiento2 = new Establecimiento();
            establecimiento2.SetNombre("B");
            establecimiento2.SetDireccion("DirB");
            establecimiento2.SetTelefono("222");
            establecimiento2.SetEmail("b@email.com");

            var establecimientos = new List<Establecimiento>
            {
                establecimiento1,
                establecimiento2
            };

            _repoMock.Setup(r => r.GetAllAsync(pagination, It.IsAny<CancellationToken>()))
                .ReturnsAsync(establecimientos);

            // Act
            var result = await _service.ObtenerPaginadoAsync(pagination, CancellationToken.None);
            var expectedItem1 = result.FirstOrDefault(e => e.Id.Equals(establecimiento1.Id));
            var expectedItem2 = result.FirstOrDefault(e => e.Id.Equals(establecimiento2.Id));

            // Assert
            Assert.That(result, Is.Not.Null);
            Assert.That(result.Count(), Is.EqualTo(2));
            Assert.Multiple(() =>
            {
                Assert.That(expectedItem1, Is.Not.Null);
                Assert.That(result, Contains.Item(expectedItem1));
            });
            Assert.Multiple(() =>
            {
                Assert.That(expectedItem2, Is.Not.Null);
                Assert.That(result, Contains.Item(expectedItem2));
            });
        }

        [Test]
        public void ObtenerPaginadoAsync_CuandoRepositorioFalla_LanzaExcepcion()
        {
            // Arrange
            var pagination = new GetItemsPaginated { PageNumber = 1, PageSize = 2 };
            _repoMock.Setup(r => r.GetAllAsync(pagination, It.IsAny<CancellationToken>()))
                .ThrowsAsync(new InvalidOperationException("Error de paginación"));

            // Act & Assert
            Assert.ThrowsAsync<InvalidOperationException>(async () =>
                await _service.ObtenerPaginadoAsync(pagination, CancellationToken.None));
        }

        [Test]
        public async Task ModificarAsync_CuandoExisteEstablecimiento_ModificaYRetornaDto()
        {
            // Arrange
            var establecimiento = new Establecimiento();
            establecimiento.SetNombre("ViejoNombre");
            establecimiento.SetDireccion("ViejaDireccion");
            establecimiento.SetTelefono("111111");
            establecimiento.SetEmail("viejo@email.com");

            var request = new ModificarEstablecimientoRequest
            {
                Id = establecimiento.Id,
                Nombre = "NuevoNombre",
                Direccion = "NuevaDireccion",
                Telefono = "999999",
                Email = "nuevo@email.com"
            };

            _repoMock.Setup(r => r.GetByIdAsync(establecimiento.Id, It.IsAny<CancellationToken>()))
                .ReturnsAsync(establecimiento);

            // Act
            var result = await _service.ModificarAsync(request, CancellationToken.None);

            // Assert
            Assert.Multiple(() =>
            {
                Assert.That(result, Is.Not.Null);
                Assert.That(result.Id, Is.EqualTo(establecimiento.Id));
                Assert.That(result.Nombre, Is.EqualTo(request.Nombre));
                Assert.That(result.Direccion, Is.EqualTo(request.Direccion));
                Assert.That(result.Telefono, Is.EqualTo(request.Telefono));
                Assert.That(result.Email, Is.EqualTo(request.Email));
            });
        }

        [Test]
        public void ModificarAsync_CuandoRepositorioFalla_LanzaExcepcion()
        {
            // Arrange
            var id = Guid.NewGuid();
            var request = new ModificarEstablecimientoRequest
            {
                Id = id,
                Nombre = "NuevoNombre",
                Direccion = "NuevaDireccion",
                Telefono = "999999",
                Email = "nuevo@email.com"
            };

            _repoMock.Setup(r => r.GetByIdAsync(id, It.IsAny<CancellationToken>()))
                .ThrowsAsync(new InvalidOperationException("No encontrado"));

            // Act & Assert
            Assert.ThrowsAsync<InvalidOperationException>(async () =>
                await _service.ModificarAsync(request, CancellationToken.None));
        }

        [Test]
        public void EliminarAsync_CuandoExisteEstablecimiento_EliminaCorrectamente()
        {
            // Arrange
            var establecimiento = new Establecimiento();
            establecimiento.SetNombre("A");
            establecimiento.SetDireccion("DirA");
            establecimiento.SetTelefono("111");
            establecimiento.SetEmail("a@email.com");

            _repoMock.Setup(r => r.GetByIdAsync(establecimiento.Id, It.IsAny<CancellationToken>()))
                .ReturnsAsync(establecimiento);

            // Act & Assert
            Assert.DoesNotThrowAsync(async () =>
                await _service.EliminarAsync(establecimiento.Id, CancellationToken.None));

            _repoMock.Verify(r => r.Delete(establecimiento), Times.Once);
        }

        [Test]
        public void EliminarAsync_CuandoRepositorioFalla_LanzaExcepcion()
        {
            // Arrange
            var id = Guid.NewGuid();
            _repoMock.Setup(r => r.GetByIdAsync(id, It.IsAny<CancellationToken>()))
                .ThrowsAsync(new InvalidOperationException("No encontrado"));

            // Act & Assert
            Assert.ThrowsAsync<InvalidOperationException>(async () =>
                await _service.EliminarAsync(id, CancellationToken.None));
        }
    }
}