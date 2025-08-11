using Microsoft.Extensions.Logging;
using ZSports.Contracts;
using ZSports.Socios.Contracts;

namespace ZSports.Socios.Persistence;

public class SociosService(ILogger<SociosService> logger, IUnitOfWork unitOfWork) : ISociosService
{

}
