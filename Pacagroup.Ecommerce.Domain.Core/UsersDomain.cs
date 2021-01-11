using Pacagroup.Ecommerce.Domain.Entity;
using Pacagroup.Ecommerce.Domain.Interface;
using Pacagroup.Ecommerce.Infraestructure.Interface;

namespace Pacagroup.Ecommerce.Domain.Core
{
    public class UsersDomain : IUsersDomain
    {
        #region Fields
        private readonly IUsersRepository _usersRepository;
        #endregion
        #region Ctor
        public UsersDomain(IUsersRepository usersRepository)
        {
            _usersRepository = usersRepository;
        }
        #endregion
        public Users Authenticate(string username, string password)
        {
            return _usersRepository.Authenticate(username, password);
        }
    }
}
