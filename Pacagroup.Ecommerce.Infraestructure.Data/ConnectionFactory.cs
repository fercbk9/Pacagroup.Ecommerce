using System;
using System.Data;
using System.Data.SqlClient;
using Microsoft.Extensions.Configuration;
using Pacagroup.Ecommerce.Transversal.Common;


namespace Pacagroup.Ecommerce.Infraestructure.Data
{
    public class ConnectionFactory : IConnectionFactory
    {
        #region Fields
        private readonly IConfiguration _configuration;
        #endregion
        #region Ctor
        public ConnectionFactory(IConfiguration configuration)
        {
            _configuration = configuration;
        }
        #endregion
        #region Properties
        public IDbConnection GetConnection
        {
            get
            {
                var sqlConnection = new SqlConnection();
                if (sqlConnection == null) return null;
                sqlConnection.ConnectionString = _configuration.GetConnectionString("NorthwindConnection");
                sqlConnection.Open();
                return sqlConnection;
            }
        }
        #endregion

    }
}
