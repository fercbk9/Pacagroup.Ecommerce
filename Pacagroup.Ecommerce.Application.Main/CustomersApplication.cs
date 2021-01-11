using System;
using AutoMapper;
using Pacagroup.Ecommerce.Application.DTO;
using Pacagroup.Ecommerce.Application.Interface;
using Pacagroup.Ecommerce.Domain.Interface;
using Pacagroup.Ecommerce.Domain.Entity;
using Pacagroup.Ecommerce.Transversal.Common;
using System.Threading.Tasks;
using System.Collections.Generic;

namespace Pacagroup.Ecommerce.Application.Main
{
    public class CustomersApplication : ICustomersApplication
    {
        #region Fields
        private readonly ICustomersDomain _customersDomain;
        private readonly IMapper _mapper;
        private readonly IAppLogger<CustomersApplication> _appLogger;
        #endregion
        #region Ctor
        public CustomersApplication(ICustomersDomain customersDomain, IMapper mapper, IAppLogger<CustomersApplication> appLogger)
        {
            _customersDomain = customersDomain;
            _mapper = mapper;
            _appLogger = appLogger;
        }
        #endregion
        #region Sync
        public Response<bool> Insert(CustomersDTO customer)
        {
            var response = new Response<bool>();
            try
            {
                var customerAux = _mapper.Map<Customers>(customer);
                response.Data = _customersDomain.Insert(customerAux);
                if (response.Data)
                {
                    response.IsSuccess = true;
                    response.Message = "Registro con exito!";
                    _appLogger.LogInformation("Exito!");
                }
            }
            catch (Exception ex)
            {
                response.Message = ex.Message;
            }
            return response;
        }
        public Response<bool> Update(CustomersDTO customer) 
        {
            var response = new Response<bool>();
            try
            {
                var customerAux = _mapper.Map<Customers>(customer);
                response.Data = _customersDomain.Update(customerAux);
                if (response.Data)
                {
                    response.IsSuccess = true;
                    response.Message = "Update con exito!";
                }
            }
            catch (Exception ex)
            {
                response.Message = ex.Message;
            }
            return response;
        }
        public Response<bool> Delete(string customerId)
        {
            var response = new Response<bool>();
            try
            {
                response.Data = _customersDomain.Delete(customerId);
                if (response.Data)
                {
                    response.IsSuccess = true;
                    response.Message = "Registro con exito!";
                }
            }
            catch (Exception ex)
            {
                response.Message = ex.Message;
            }
            return response;
        }
        public Response<CustomersDTO> Get(string customerId)
        {
            var response = new Response<CustomersDTO>();
            try
            {
                response.Data = _mapper.Map<CustomersDTO>(_customersDomain.Get(customerId));
                if (response.Data != null)
                {
                    response.IsSuccess = true;
                    response.Message = "Query con exito!";
                }
            }
            catch (Exception ex)
            {
                response.Message = ex.Message;
            }
            return response;
        }
        public Response<IEnumerable<CustomersDTO>> GetAll()
        {
            var response = new Response<IEnumerable<CustomersDTO>>();
            try
            {
                response.Data = _mapper.Map<IEnumerable<CustomersDTO>>(_customersDomain.GetAll());
                if (response.Data != null)
                {
                    response.IsSuccess = true;
                    response.Message = "Query con exito!";
                }
            }
            catch (Exception ex)
            {
                response.Message = ex.Message;
            }
            return response;
        }
        #endregion

        #region Async
        public async Task<Response<bool>> InsertAsync(CustomersDTO customer)
        {
            var response = new Response<bool>();
            try
            {
                var customerAux = _mapper.Map<Customers>(customer);
                response.Data = await _customersDomain.InsertAsync(customerAux);
                if (response.Data)
                {
                    response.IsSuccess = true;
                    response.Message = "Registro con exito!";
                }
            }
            catch (Exception ex)
            {
                response.Message = ex.Message;
            }
            return response;
        }
        public async Task<Response<bool>> UpdateAsync(CustomersDTO customer)
        {
            var response = new Response<bool>();
            try
            {
                var customerAux = _mapper.Map<Customers>(customer);
                response.Data = await _customersDomain.UpdateAsync(customerAux);
                if (response.Data)
                {
                    response.IsSuccess = true;
                    response.Message = "Update con exito!";
                }
            }
            catch (Exception ex)
            {
                response.Message = ex.Message;
            }
            return response;
        }
        public async Task<Response<bool>> DeleteAsync(string customerId)
        {
            var response = new Response<bool>();
            try
            {
                response.Data = await _customersDomain.DeleteAsync(customerId);
                if (response.Data)
                {
                    response.IsSuccess = true;
                    response.Message = "Delete con exito!";
                }
            }
            catch (Exception ex)
            {
                response.Message = ex.Message;
            }
            return response;
        }
        public async Task<Response<CustomersDTO>> GetAsync(string customerId)
        {
            var response = new Response<CustomersDTO>();
            try
            {
                response.Data = _mapper.Map<CustomersDTO>(await _customersDomain.GetAsync(customerId));
                if (response.Data != null)
                {
                    response.IsSuccess = true;
                    response.Message = "Query con exito!";
                }
            }
            catch (Exception ex)
            {
                response.Message = ex.Message;
            }
            return response;
        }
        public async Task<Response<IEnumerable<CustomersDTO>>> GetAllAsync()
        {
            var response = new Response<IEnumerable<CustomersDTO>>();
            try
            {
                response.Data = _mapper.Map<IEnumerable<CustomersDTO>>(await _customersDomain.GetAllAsync());
                if (response.Data != null)
                {
                    response.IsSuccess = true;
                    response.Message = "Query con exito!";
                    _appLogger.LogInformation("Exito!");
                }
            }
            catch (Exception ex)
            {
                response.Message = ex.Message;
                _appLogger.LogError("Error! " + ex.Message);
            }
            return response;
        }
        #endregion
    }
}
