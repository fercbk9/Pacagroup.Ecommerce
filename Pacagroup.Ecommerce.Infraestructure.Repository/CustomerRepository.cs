using Dapper;
using Pacagroup.Ecommerce.Domain.Entity;
using Pacagroup.Ecommerce.Infraestructure.Interface;
using Pacagroup.Ecommerce.Transversal.Common;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Pacagroup.Ecommerce.Infraestructure.Repository
{
    public class CustomerRepository : ICustomersRepository
    {
        #region Fields
        private readonly IConnectionFactory _connectionFactory;
        #endregion
        #region Ctor
        public CustomerRepository(IConnectionFactory connectionFactory)
        {
            _connectionFactory = connectionFactory;
        }
        #endregion
        #region Sync Methods
        public bool Insert(Customers customer)
        {
            using (var connection = _connectionFactory.GetConnection)
            {
                var procedureName = "CustomersInsert";
                var parameters = new DynamicParameters();
                parameters.AddDynamicParams(customer);
                var result = connection.Execute(procedureName, parameters, commandType: System.Data.CommandType.StoredProcedure);
                return result > 0;
            }
        }
        public bool Update(Customers customer)
        {
            using (var connection = _connectionFactory.GetConnection)
            {
                var procedureName = "CustomersUpdate";
                var parameters = new DynamicParameters();
                parameters.AddDynamicParams(customer);
                var result = connection.Execute(procedureName, parameters, commandType: System.Data.CommandType.StoredProcedure);
                return result > 0;
            }
        }
        public bool Delete(string customerId)
        {
            using (var connection = _connectionFactory.GetConnection)
            {
                var procedureName = "CustomersDelete";
                var parameters = new DynamicParameters();
                parameters.Add("customerID",customerId);
                var result = connection.Execute(procedureName, parameters, commandType: System.Data.CommandType.StoredProcedure);
                return result > 0;
            }
        }
        public Customers Get(string customerId)
        {
            using (var connection = _connectionFactory.GetConnection)
            {
                var procedureName = "CustomersGetByID";
                var parameters = new DynamicParameters();
                parameters.Add("customerID", customerId);
                var result = connection.QuerySingle<Customers>(procedureName, parameters, commandType: System.Data.CommandType.StoredProcedure);
                return result;
            }
        }
        public IEnumerable<Customers> GetAll()
        {
            using (var connection = _connectionFactory.GetConnection)
            {
                var procedureName = "CustomersList";
                var result = connection.Query<Customers>(procedureName, commandType: System.Data.CommandType.StoredProcedure);
                return result;
            }
        }
        #endregion
        #region Async Methods
        public async Task<bool> InsertAsync(Customers customer)
        {
            using (var connection = _connectionFactory.GetConnection)
            {
                var procedureName = "CustomersInsert";
                var parameters = new DynamicParameters();
                parameters.AddDynamicParams(customer);
                var result = await connection.ExecuteAsync(procedureName, parameters, commandType: System.Data.CommandType.StoredProcedure);
                return result > 0;
            }
        }
        public async Task<bool> UpdateAsync(Customers customer)
        {
            using (var connection = _connectionFactory.GetConnection)
            {
                var procedureName = "CustomersUpdate";
                var parameters = new DynamicParameters();
                parameters.AddDynamicParams(customer);
                var result = await connection.ExecuteAsync(procedureName, parameters, commandType: System.Data.CommandType.StoredProcedure);
                return result > 0;
            }
        }
        public async Task<bool> DeleteAsync(string customerId)
        {
            using (var connection = _connectionFactory.GetConnection)
            {
                var procedureName = "CustomersDelete";
                var parameters = new DynamicParameters();
                parameters.Add("customerID", customerId);
                var result = await connection.ExecuteAsync(procedureName, parameters, commandType: System.Data.CommandType.StoredProcedure);
                return result > 0;
            }
        }
        public async Task<Customers> GetAsync(string customerId)
        {
            using (var connection = _connectionFactory.GetConnection)
            {
                var procedureName = "CustomersGetByID";
                var parameters = new DynamicParameters();
                parameters.Add("customerID", customerId);
                var result = await connection.QuerySingleAsync<Customers>(procedureName, parameters, commandType: System.Data.CommandType.StoredProcedure);
                return result;
            }
        }
        public async Task<IEnumerable<Customers>> GetAllAsync()
        {
            using (var connection = _connectionFactory.GetConnection)
            {
                var procedureName = "CustomersList";
                var result = await connection.QueryAsync<Customers>(procedureName, commandType: System.Data.CommandType.StoredProcedure);
                return result;
            }
        }
        #endregion
    }
}
