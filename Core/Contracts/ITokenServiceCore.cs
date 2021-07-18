using LinkGamer.Domain.Entities;

namespace Core.Contracts
{
    public interface ITokenServiceCore
    {
        string GenerateToken(User user);
    }
}
