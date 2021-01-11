using Dapper;
using Pacagroup.Ecommerce.Domain.Entity;
using Pacagroup.Ecommerce.Infraestructure.Interface;
using Pacagroup.Ecommerce.Transversal.Common;

namespace Pacagroup.Ecommerce.Infraestructure.Repository
{
    public class UsersRepository : IUsersRepository
    {
        #region Fields
        private readonly IConnectionFactory _connectionFactory;
        #endregion
        #region Ctor
        public UsersRepository(IConnectionFactory connectionFactory)
        {
            _connectionFactory = connectionFactory;
        }
        #endregion
        public Users Authenticate(string username,string password)
        {
            using (var connection = _connectionFactory.GetConnection)
            {
                var query = "UsersGetByUserAndPassword";
                var parameters = new DynamicParameters();
                parameters.Add("UserName", username);
                parameters.Add("Password", password);
                return connection.QuerySingle<Users>(query, parameters, commandType: System.Data.CommandType.StoredProcedure); 
            }
        }
    }
}
