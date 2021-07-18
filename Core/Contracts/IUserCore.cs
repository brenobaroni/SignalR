using Core.Core;
using LinkGamer.Domain.Entities;
using LinkGamer.Models;

namespace Core.Contracts
{
    public interface IUserCore : IEntityCoreBase<User>
    {
        //User Get(string email, string password);
        User Get(string email);
        LinkGamerResult Login(UserModel user);
    }
}
